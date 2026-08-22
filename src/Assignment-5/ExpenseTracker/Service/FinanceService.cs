using ExpenseTracker.ConstantLiteral;
using ExpenseTracker.Enums;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="FinanceService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IFinanceRepository"/> used to save and query records.</param>
        public FinanceService(IFinanceRepository repository)
        {
            this.financeRepository = repository;
        }

        /// <summary>
        /// Determines whether the specified user has any associated records.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>True if the user has no records, otherwise false.</returns>
        public bool IsRecordListEmpty(Guid userId)
        {
            return !this.financeRepository.GetRecords(record => record.UserId == userId).Any();
        }

        /// <summary>
        /// Creates and stores a new <see cref="Income"/> record.
        /// </summary>
        /// <param name="userId">The user ID associated with the record.</param>
        /// <param name="date">The date value for the new record.</param>
        /// <param name="amount">The amount for the income.</param>
        /// <param name="source">The <see cref="IncomeSource"/> of the income.</param>
        /// <param name="description">An optional description for the record.</param>
        public void AddIncome(Guid userId, DateOnly date, decimal amount, IncomeSource source, string? description)
        {
            string recordId = this.GenerateUniqueId();
            Income income = new Income(recordId, userId, date, amount, description, RecordType.Income, source);
            this.financeRepository.AddRecord(income);
        }

        /// <summary>
        /// Creates and stores a new <see cref="Expense"/> record.
        /// </summary>
        /// <param name="userId">The user ID associated with the record.</param>
        /// <param name="date">The date value for the new record.</param>
        /// <param name="amount">The amount for the expense.</param>
        /// <param name="category">The <see cref="ExpenseCategory"/> of the expense.</param>
        /// <param name="description">An optional description for the record.</param>
        public void AddExpense(Guid userId, DateOnly date, decimal amount, ExpenseCategory category, string? description)
        {
            string recordId = this.GenerateUniqueId();
            Expense expense = new Expense(recordId, userId, date, amount, description, RecordType.Expense, category);
            this.financeRepository.AddRecord(expense);
        }

        /// <summary>
        /// Retrieves all stored records sorted by date (most recent first).
        /// </summary>
        /// <param name="userId">The user ID of the record.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> sorted by date.</returns>
        public IEnumerable<FinancialRecord> GetAllRecords(Guid userId)
        {
            IEnumerable<FinancialRecord> records = this.financeRepository
                                        .GetRecords(record => record.UserId == userId);

            return this.SortRecordsByDate(records);
        }

        /// <summary>
        /// Retrieves all stored income records sorted by date (most recent first).
        /// </summary>
        /// <param name="userId">The user ID of the record.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing incomes sorted by date.</returns>
        public IEnumerable<FinancialRecord> GetIncomeRecords(Guid userId)
        {
            IEnumerable<FinancialRecord> incomeRecords = this.financeRepository
                                        .GetRecords(record => record is Income
                                                              && record.UserId == userId);

            return this.SortRecordsByDate(incomeRecords);
        }

        /// <summary>
        /// Retrieves all stored expense records sorted by date (most recent first).
        /// </summary>
        /// <param name="userId">The user ID of the record.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing expenses sorted by date.</returns>
        public IEnumerable<FinancialRecord> GetExpenseRecords(Guid userId)
        {
            IEnumerable<FinancialRecord> expenseRecords = this.financeRepository
                                        .GetRecords(record => record is Expense
                                                              && record.UserId == userId);

            return this.SortRecordsByDate(expenseRecords);
        }

        /// <summary>
        /// Retrieves records that match the specified date, sorted by date.
        /// </summary>
        /// <param name="userId">The user ID of the record.</param>
        /// <param name="date">The date value to search for.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> that occur on the specified date, sorted by date.</returns>
        public IEnumerable<FinancialRecord> SearchByDate(Guid userId, DateOnly date)
        {
            IEnumerable<FinancialRecord> dateRecords = this.financeRepository
                                        .GetRecords(record => record.UserId == userId
                                                              && record.Date == date);

            return this.SortRecordsByDate(dateRecords);
        }

        /// <summary>
        /// Retrieves records that match the specified amount, sorted by date.
        /// </summary>
        /// <param name="userId">The user ID of the record.</param>
        /// <param name="amount">The amount to search for.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> with the specified amount, sorted by date.</returns>
        public IEnumerable<FinancialRecord> SearchByAmount(Guid userId, decimal amount)
        {
            IEnumerable<FinancialRecord> amountRecords = this.financeRepository
                                        .GetRecords(record => record.UserId == userId
                                                              && record.Amount == amount);

            return this.SortRecordsByDate(amountRecords);
        }

        /// <summary>
        /// Retrieves income records that match the specified <see cref="IncomeSource"/>, sorted by date.
        /// </summary>
        /// <param name="userId">The user ID of the record.</param>
        /// <param name="source">The <see cref="IncomeSource"/> to filter by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing incomes with the specified source, sorted by date.</returns>
        public IEnumerable<FinancialRecord> SearchBySource(Guid userId, IncomeSource source)
        {
            IEnumerable<FinancialRecord> sourceRecords = this.financeRepository
                                        .GetRecords(record => record.UserId == userId
                                                              && record is Income income
                                                              && income.Source == source);
            return this.SortRecordsByDate(sourceRecords);
        }

        /// <summary>
        /// Retrieves expense records that match the specified <see cref="ExpenseCategory"/>, sorted by date.
        /// </summary>
        /// <param name="userId">The user ID of the record.</param>
        /// <param name="category">The <see cref="ExpenseCategory"/> to filter by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing expenses in the specified category, sorted by date.</returns>
        public IEnumerable<FinancialRecord> SearchByCategory(Guid userId, ExpenseCategory category)
        {
            IEnumerable<FinancialRecord> categoryRecords = this.financeRepository
                                        .GetRecords(record => record.UserId == userId
                                                              && record is Expense expense
                                                              && expense.Category == category);
            return this.SortRecordsByDate(categoryRecords);
        }

        /// <summary>
        /// Retrieves a record by its identifier.
        /// </summary>
        /// <param name="userId">The user ID of the record.</param>
        /// <param name="recordId">The identifier of the record to retrieve.</param>
        /// <returns>The matching <see cref="FinancialRecord"/> if found.</returns>
        public FinancialRecord GetRecordById(Guid userId, string recordId)
        {
            FinancialRecord record = this.financeRepository.GetById(recordId) !;

            if (record.UserId != userId) // Check if the user enters a record ID that is present in another user's record.
            {
                throw new UnauthorizedAccessException();
            }

            return record;
        }

        /// <summary>
        /// Deletes the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="userId">The user ID of the record.</param>
        /// <param name="recordId">The record ID to be deleted.</param>
        public void DeleteRecord(Guid userId, string recordId)
        {
            FinancialRecord recordToDelete = this.GetRecordById(userId, recordId);
            this.financeRepository.DeleteRecord(recordToDelete);
        }

        /// <summary>
        /// Deletes the records that are associated with the user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        public void DeleteRecordsByUserId(Guid userId)
        {
            this.financeRepository.DeleteRecordsByUserId(userId);
        }

        /// <summary>
        /// Updates the date of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="date">The new date value.</param>
        public void EditRecordDate(FinancialRecord record, DateOnly date)
        {
            FinancialRecord originalRecord = this.GetRecordById(record.UserId, record.Id);
            originalRecord.Date = date;
            this.financeRepository.UpdateRecord(originalRecord);
        }

        /// <summary>
        /// Updates the amount of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="amount">The new amount value.</param>
        public void EditRecordAmount(FinancialRecord record, decimal amount)
        {
            FinancialRecord originalRecord = this.GetRecordById(record.UserId, record.Id);
            originalRecord.Amount = amount;
            this.financeRepository.UpdateRecord(originalRecord);
        }

        /// <summary>
        /// Updates the source of the specified <see cref="Income"/>.
        /// </summary>
        /// <param name="record">The <see cref="Income"/> to update.</param>
        /// <param name="source">The new <see cref="IncomeSource"/>.</param>
        public void EditRecordSource(Income record, IncomeSource source)
        {
            Income originalRecord = (Income)this.GetRecordById(record.UserId, record.Id);
            originalRecord.Source = source;
            this.financeRepository.UpdateRecord(originalRecord);
        }

        /// <summary>
        /// Updates the category of the specified <see cref="Expense"/>.
        /// </summary>
        /// <param name="record">The <see cref="Expense"/> to update.</param>
        /// <param name="category">The new <see cref="ExpenseCategory"/>.</param>
        public void EditRecordCategory(Expense record, ExpenseCategory category)
        {
            Expense originalRecord = (Expense)this.GetRecordById(record.UserId, record.Id);
            originalRecord.Category = category;
            this.financeRepository.UpdateRecord(originalRecord);
        }

        /// <summary>
        /// Updates the description of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="description">The new description value; may be empty.</param>
        public void EditRecordDescription(FinancialRecord record, string? description)
        {
            FinancialRecord originalRecord = this.GetRecordById(record.UserId, record.Id);
            originalRecord.Description = description;
            this.financeRepository.UpdateRecord(originalRecord);
        }

        /// <summary>
        /// Produces a monthly summary for the specified month and year.
        /// </summary>
        /// <param name="userId">The user ID associated with the summary.</param>
        /// <param name="month">The month to summarize.</param>
        /// <param name="year">The year to summarize.</param>
        /// <returns>
        /// A collection of the following: Total income for the month,
        /// Total expenses for the month,
        /// The net balance for the month,
        /// The savings rate for the month,
        /// The <see cref="Expense"/> with the highest amount for the month (if any).
        /// </returns>
        public (
            decimal NetIncome,
            decimal NetExpense,
            decimal NetBalance,
            decimal SavingsRate,
            Expense? HighestExpense)
        GetMonthlySummary(Guid userId, int month, int year)
        {
            IEnumerable<FinancialRecord> records = this.financeRepository
                                            .GetRecords(record => record.UserId == userId
                                                                  && record.Date.Month == month
                                                                  && record.Date.Year == year);
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