using ExpenseTracker.ConstantLiteral;
using ExpenseTracker.Enums;
using ExpenseTracker.Helper;
using ExpenseTracker.Model;
using ExpenseTracker.Repository;

namespace ExpenseTracker.Service
{
    /// <summary>
    /// Coordinates application-level operations for creating, retrieving and modifying <see cref="FinancialRecord"/> instances.
    /// </summary>
    internal class FinanceService
    {
        private readonly IFinanceRepository financeRepository;
        private readonly FinanceHelper financeHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinanceService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IFinanceRepository"/> used to save and query records.</param>
        /// <param name="financeHelper">The <see cref="FinanceHelper"/> used to validate input.</param>
        public FinanceService(IFinanceRepository repository, FinanceHelper financeHelper)
        {
            this.financeRepository = repository;
            this.financeHelper = financeHelper;
        }

        /// <summary>
        /// Determines whether the specified user has any associated records.
        /// </summary>
        /// <returns>True if the user has no records, otherwise false.</returns>
        public bool HasRecords()
        {
            return this.financeRepository.GetRecords().Any();
        }

        /// <summary>
        /// Creates and stores a new <see cref="Income"/> record.
        /// </summary>
        /// <param name="date">The date value for the new record.</param>
        /// <param name="amount">The amount for the income.</param>
        /// <param name="source">The <see cref="IncomeSource"/> of the income.</param>
        /// <param name="description">An optional description for the record.</param>
        public void AddIncome(DateOnly date, decimal amount, IncomeSource source, string? description)
        {
            string recordId = this.GenerateUniqueId();
            Income income = new Income(recordId, date, amount, description, source);
            this.financeRepository.AddRecord(income);
        }

        /// <summary>
        /// Creates and stores a new <see cref="Expense"/> record.
        /// </summary>
        /// <param name="date">The date value for the new record.</param>
        /// <param name="amount">The amount for the expense.</param>
        /// <param name="category">The <see cref="ExpenseCategory"/> of the expense.</param>
        /// <param name="description">An optional description for the record.</param>
        public void AddExpense(DateOnly date, decimal amount, ExpenseCategory category, string? description)
        {
            string recordId = this.GenerateUniqueId();
            Expense expense = new Expense(recordId, date, amount, description, category);
            this.financeRepository.AddRecord(expense);
        }

        /// <summary>
        /// Retrieves all the records with the specified date range.
        /// </summary>
        /// <typeparam name="T">The type of the record to be retrieved.</typeparam>
        /// <param name="startDate">The starting date of the range.</param>
        /// <param name="endDate">The ending date of the range.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> within the date range.</returns>
        public IEnumerable<FinancialRecord> GetByDateRange<T>(DateOnly? startDate, DateOnly? endDate)
            where T : FinancialRecord
        {
            IEnumerable<FinancialRecord> records = this.financeRepository
                                                    .GetRecords(record =>
                                                        record is T &&
                                                        (!startDate.HasValue || record.Date >= startDate.Value) &&
                                                        (!endDate.HasValue || record.Date <= endDate.Value));

            return this.SortRecordsByDate(records);
        }

        /// <summary>
        /// Retrieves records that match the specified date, sorted by date.
        /// </summary>
        /// <param name="date">The date value to search for.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> that occur on the specified date, sorted by date.</returns>
        public IEnumerable<FinancialRecord> SearchByDate(DateOnly date)
        {
            IEnumerable<FinancialRecord> dateRecords = this.financeRepository
                                        .GetRecords(record => record.Date == date);

            return this.SortRecordsByDate(dateRecords);
        }

        /// <summary>
        /// Retrieves records that match the specified amount, sorted by date.
        /// </summary>
        /// <param name="amount">The amount to search for.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> with the specified amount, sorted by date.</returns>
        public IEnumerable<FinancialRecord> SearchByAmount(decimal amount)
        {
            IEnumerable<FinancialRecord> amountRecords = this.financeRepository
                                        .GetRecords(record => record.Amount == amount);

            return this.SortRecordsByDate(amountRecords);
        }

        /// <summary>
        /// Retrieves income records that match the specified <see cref="IncomeSource"/>, sorted by date.
        /// </summary>
        /// <param name="source">The <see cref="IncomeSource"/> to filter by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing incomes with the specified source, sorted by date.</returns>
        public IEnumerable<FinancialRecord> SearchBySource(IncomeSource source)
        {
            IEnumerable<FinancialRecord> sourceRecords = this.financeRepository
                                        .GetRecords(record => record is Income income && income.Source == source);
            return this.SortRecordsByDate(sourceRecords);
        }

        /// <summary>
        /// Retrieves expense records that match the specified <see cref="ExpenseCategory"/>, sorted by date.
        /// </summary>
        /// <param name="category">The <see cref="ExpenseCategory"/> to filter by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing expenses in the specified category, sorted by date.</returns>
        public IEnumerable<FinancialRecord> SearchByCategory(ExpenseCategory category)
        {
            IEnumerable<FinancialRecord> categoryRecords = this.financeRepository
                                        .GetRecords(record => record is Expense expense && expense.Category == category);
            return this.SortRecordsByDate(categoryRecords);
        }

        /// <summary>
        /// Searches the records according to the specific input term given by the user.
        /// </summary>
        /// <param name="searchTerm">The search input given by the user.</param>
        /// <returns>The records according to the search input.</returns>
        public IEnumerable<FinancialRecord> Search(string searchTerm)
        {
            SearchType searchType = this.financeHelper.ReturnSearchType(searchTerm);

            switch (searchType)
            {
                case SearchType.Date:
                    this.financeHelper.IsValidDate(searchTerm, out DateOnly date);
                    return this.SearchByDate(date);

                case SearchType.Amount:
                    this.financeHelper.IsValidAmount(searchTerm, out decimal amount);
                    return this.SearchByAmount(amount);

                case SearchType.Source:
                    this.financeHelper.IsValidSource(searchTerm, out IncomeSource source);
                    return this.SearchBySource(source);

                case SearchType.Category:
                    this.financeHelper.IsValidCategory(searchTerm, out ExpenseCategory category);
                    return this.SearchByCategory(category);

                default:
                    // Checks for the case where the source and category of expense are the same (Other).
                    if (this.financeHelper.IsValidSource(searchTerm, out IncomeSource sameSource)
                        && this.financeHelper.IsValidCategory(searchTerm, out ExpenseCategory sameCategory))
                    {
                        return this.SearchBySource(sameSource)
                            .Concat(this.SearchByCategory(sameCategory))
                            .ToList();
                    }

                    return Enumerable.Empty<FinancialRecord>();
            }
        }

        /// <summary>
        /// Retrieves a record by its identifier.
        /// </summary>
        /// <param name="recordId">The identifier of the record to retrieve.</param>
        /// <returns>The matching <see cref="FinancialRecord"/> if found.</returns>
        public FinancialRecord? GetRecordById(string recordId)
        {
            return this.financeRepository.GetById(recordId);
        }

        /// <summary>
        /// Determines whether a given record identifier is present in the provided collection.
        /// </summary>
        /// <param name="recordId">The identifier to search for.</param>
        /// <param name="records">The collection of <see cref="FinancialRecord"/> to search.</param>
        /// <returns>True if the identifier is present in the collection, otherwise false.</returns>
        public bool IsDisplayedRecord(string recordId, IEnumerable<FinancialRecord> records)
        {
            return records.Any(record => record.Id.Equals(recordId, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Deletes the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to delete.</param>
        /// <returns>True if the delete operation is successful, else false.</returns>
        public bool DeleteRecord(FinancialRecord record)
        {
            return this.financeRepository.DeleteRecord(record);
        }

        /// <summary>
        /// Deletes the records that are associated with the user.
        /// </summary>
        public void DeleteCurrentUserRecords()
        {
            this.financeRepository.DeleteCurrentUserRecords();
        }

        /// <summary>
        /// Updates the date of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="date">The new date value.</param>
        /// <returns>True if the edit operation is successful, else false.</returns>
        public bool EditRecordDate(FinancialRecord record, DateOnly date)
        {
            record.Date = date;
            return this.financeRepository.UpdateRecord(record);
        }

        /// <summary>
        /// Updates the amount of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="amount">The new amount value.</param>
        /// <returns>True if the edit operation is successful, else false.</returns>
        public bool EditRecordAmount(FinancialRecord record, decimal amount)
        {
            record.Amount = amount;
            return this.financeRepository.UpdateRecord(record);
        }

        /// <summary>
        /// Updates the source of the specified <see cref="Income"/>.
        /// </summary>
        /// <param name="record">The <see cref="Income"/> to update.</param>
        /// <param name="source">The new <see cref="IncomeSource"/>.</param>
        /// <returns>True if the edit operation is successful, else false.</returns>
        public bool EditRecordSource(Income record, IncomeSource source)
        {
            record.Source = source;
            return this.financeRepository.UpdateRecord(record);
        }

        /// <summary>
        /// Updates the category of the specified <see cref="Expense"/>.
        /// </summary>
        /// <param name="record">The <see cref="Expense"/> to update.</param>
        /// <param name="category">The new <see cref="ExpenseCategory"/>.</param>
        /// <returns>True if the edit operation is successful, else false.</returns>
        public bool EditRecordCategory(Expense record, ExpenseCategory category)
        {
            record.Category = category;
            return this.financeRepository.UpdateRecord(record);
        }

        /// <summary>
        /// Updates the description of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="description">The new description value; may be empty.</param>
        /// <returns>True if the edit operation is successful, else false.</returns>
        public bool EditRecordDescription(FinancialRecord record, string? description)
        {
            record.Description = description;
            return this.financeRepository.UpdateRecord(record);
        }

        /// <summary>
        /// Produces a monthly summary for the specified month and year.
        /// </summary>
        /// <param name="startDate">The starting date to summarize.</param>
        /// <param name="endDate">The ending date to summarize.</param>
        /// <returns>
        /// A collection of the following: Total income for the given range,
        /// Total expenses for the given range,
        /// The net balance for the given range,
        /// The savings rate for the given range,
        /// The <see cref="Expense"/> with the highest amount for the given range (if any).
        /// </returns>
        public (
            decimal NetIncome,
            decimal NetExpense,
            decimal NetBalance,
            decimal SavingsRate,
            Expense? HighestExpense)
        GetFinancialSummary(DateOnly? startDate, DateOnly? endDate)
        {
            IEnumerable<FinancialRecord> records = this.GetByDateRange<FinancialRecord>(startDate, endDate);
            IEnumerable<Income> incomes = records.OfType<Income>();
            IEnumerable<Expense> expenses = records.OfType<Expense>();

            decimal totalIncome = incomes.Sum(income => income.Amount);
            decimal totalExpense = expenses.Sum(expense => expense.Amount);
            Expense? highestExpense = expenses.MaxBy(expense => expense.Amount);

            decimal netBalance = totalIncome - totalExpense;
            decimal savingsRate = totalIncome == 0 ? 0 : (netBalance / totalIncome) * 100;
            return (totalIncome, totalExpense, netBalance, savingsRate, highestExpense);
        }

        /// <summary>
        /// Loads the records in the repository for CRUD operations.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        public void LoadRecords(Guid userId)
        {
            this.financeRepository.LoadRecords(userId);
        }

        /// <summary>
        /// Generates a short unique identifier for a record.
        /// </summary>
        /// <returns>A unique uppercase identifier string used for new records.</returns>
        private string GenerateUniqueId()
        {
            string recordId;
            do
            {
                recordId = Guid.NewGuid().ToString("N").Substring(0, Constant.MaxLengthOfId).ToUpper();
            }
            while (this.financeRepository.RecordIdExists(recordId));

            return recordId;
        }

        /// <summary>
        /// Returns the provided records ordered by date in descending order.
        /// </summary>
        /// <param name="records">A collection of <see cref="FinancialRecord"/> to sort.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> ordered by date (most recent first).</returns>
        private IEnumerable<FinancialRecord> SortRecordsByDate(IEnumerable<FinancialRecord> records)
        {
            return records.OrderByDescending(record => record.Date).ToList();
        }
    }
}