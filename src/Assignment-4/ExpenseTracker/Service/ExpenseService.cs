using ExpenseTracker.Enums;
using ExpenseTracker.Model;
using ExpenseTracker.Repository;

namespace ExpenseTracker.Service
{
    /// <summary>
    /// Coordinates application-level operations for creating, retrieving and modifying <see cref="FinancialRecord"/> instances.
    /// </summary>
    internal class ExpenseService
    {
        private readonly IExpenseRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IExpenseRepository"/> used to save and query records.</param>
        public ExpenseService(IExpenseRepository repository)
        {
            this.repository = repository;
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
            this.repository.AddRecord(income);
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
            this.repository.AddRecord(expense);
        }

        /// <summary>
        /// Retrieves all stored records sorted by date (most recent first).
        /// </summary>
        /// <returns>A list of <see cref="FinancialRecord"/> sorted by date.</returns>
        public IEnumerable<FinancialRecord> GetAllRecords()
        {
            IEnumerable<FinancialRecord> records = this.repository.GetAllRecords();
            return this.SortRecordsByDate(records);
        }

        /// <summary>
        /// Retrieves all stored income records sorted by date (most recent first).
        /// </summary>
        /// <returns>A list of <see cref="FinancialRecord"/> representing incomes sorted by date.</returns>
        public IEnumerable<FinancialRecord> GetIncomeRecords()
        {
            IEnumerable<FinancialRecord> incomeRecords = this.repository.GetAllIncomeRecords();
            return this.SortRecordsByDate(incomeRecords);
        }

        /// <summary>
        /// Retrieves all stored expense records sorted by date (most recent first).
        /// </summary>
        /// <returns>A list of <see cref="FinancialRecord"/> representing expenses sorted by date.</returns>
        public IEnumerable<FinancialRecord> GetExpenseRecords()
        {
            IEnumerable<FinancialRecord> expenseRecords = this.repository.GetAllExpenseRecords();
            return this.SortRecordsByDate(expenseRecords);
        }

        /// <summary>
        /// Retrieves records that match the specified date, sorted by date.
        /// </summary>
        /// <param name="date">The date value to search for.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> that occur on the specified date, sorted by date.</returns>
        public IEnumerable<FinancialRecord> SearchByDate(DateOnly date)
        {
            IEnumerable<FinancialRecord> dateRecords = this.repository.GetByDate(date);
            return this.SortRecordsByDate(dateRecords);
        }

        /// <summary>
        /// Retrieves records that match the specified amount, sorted by date.
        /// </summary>
        /// <param name="amount">The amount to search for.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> with the specified amount, sorted by date.</returns>
        public IEnumerable<FinancialRecord> SearchByAmount(decimal amount)
        {
            IEnumerable<FinancialRecord> amountRecords = this.repository.GetByAmount(amount);
            return this.SortRecordsByDate(amountRecords);
        }

        /// <summary>
        /// Retrieves income records that match the specified <see cref="IncomeSource"/>, sorted by date.
        /// </summary>
        /// <param name="source">The <see cref="IncomeSource"/> to filter by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing incomes with the specified source, sorted by date.</returns>
        public IEnumerable<FinancialRecord> SearchBySource(IncomeSource source)
        {
            IEnumerable<FinancialRecord> sourceRecords = this.repository.GetBySource(source);
            return this.SortRecordsByDate(sourceRecords);
        }

        /// <summary>
        /// Retrieves expense records that match the specified <see cref="ExpenseCategory"/>, sorted by date.
        /// </summary>
        /// <param name="category">The <see cref="ExpenseCategory"/> to filter by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing expenses in the specified category, sorted by date.</returns>
        public IEnumerable<FinancialRecord> SearchByCategory(ExpenseCategory category)
        {
            IEnumerable<FinancialRecord> categoryRecords = this.repository.GetByCategory(category);
            return this.SortRecordsByDate(categoryRecords);
        }

        /// <summary>
        /// Retrieves a record by its identifier.
        /// </summary>
        /// <param name="recordId">The identifier of the record to retrieve.</param>
        /// <returns>The matching <see cref="FinancialRecord"/> if found.</returns>
        public FinancialRecord? GetRecordById(string recordId)
        {
            return this.repository.GetById(recordId);
        }

        /// <summary>
        /// Deletes the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to delete.</param>
        public void DeleteRecord(FinancialRecord record)
        {
            this.repository.DeleteRecord(record);
        }

        /// <summary>
        /// Updates the date of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="date">The new date value.</param>
        public void EditRecordDate(FinancialRecord record, DateOnly date)
        {
            this.repository.UpdateRecordDate(record, date);
        }

        /// <summary>
        /// Updates the amount of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="amount">The new amount value.</param>
        public void EditRecordAmount(FinancialRecord record, decimal amount)
        {
            this.repository.UpdateRecordAmount(record, amount);
        }

        /// <summary>
        /// Updates the source of the specified <see cref="Income"/>.
        /// </summary>
        /// <param name="record">The <see cref="Income"/> to update.</param>
        /// <param name="source">The new <see cref="IncomeSource"/>.</param>
        public void EditRecordSource(Income record, IncomeSource source)
        {
            this.repository.UpdateRecordSource(record, source);
        }

        /// <summary>
        /// Updates the category of the specified <see cref="Expense"/>.
        /// </summary>
        /// <param name="record">The <see cref="Expense"/> to update.</param>
        /// <param name="category">The new <see cref="ExpenseCategory"/>.</param>
        public void EditRecordCategory(Expense record, ExpenseCategory category)
        {
            this.repository.UpdateRecordCategory(record, category);
        }

        /// <summary>
        /// Updates the description of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="description">The new description value; may be empty.</param>
        public void EditRecordDescription(FinancialRecord record, string? description)
        {
            this.repository.UpdateRecordDescription(record, description);
        }

        /// <summary>
        /// Determines whether the repository contains no records.
        /// </summary>
        /// <returns>True if the record list is empty, otherwise False.</returns>
        public bool IsRecordListEmpty()
        {
            return this.repository.RecordCount == 0;
        }

        /// <summary>
        /// Produces a monthly summary for the specified month and year.
        /// </summary>
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
        GetMonthlySummary(int month, int year)
        {
            IEnumerable<FinancialRecord> records = this.repository.GetByMonthAndYear(month, year);
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
                recordId = Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper();
            }
            while (this.repository.RecordIdExists(recordId));

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