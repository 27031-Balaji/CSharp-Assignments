using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    /// <summary>
    /// Defines repository operations for storing and querying <see cref="FinancialRecord"/> instances.
    /// </summary>
    internal interface IExpenseRepository
    {
        /// <summary>
        /// Gets the total number of stored <see cref="FinancialRecord"/> instances.
        /// </summary>
        /// <value>The number of records in the repository.</value>
        int RecordCount { get; }

        /// <summary>
        /// Adds a <see cref="FinancialRecord"/> to the repository.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to add.</param>
        void AddRecord(FinancialRecord record);

        /// <summary>
        /// Retrieves all stored <see cref="FinancialRecord"/> instances.
        /// </summary>
        /// <returns>
        /// A list of <see cref="FinancialRecord"/> containing all records.
        /// </returns>
        IEnumerable<FinancialRecord> GetAllRecords();

        /// <summary>
        /// Retrieves all stored income records.
        /// </summary>
        /// <returns>
        /// A list of <see cref="FinancialRecord"/> representing incomes.
        /// </returns>
        IEnumerable<FinancialRecord> GetAllIncomeRecords();

        /// <summary>
        /// Retrieves all stored expense records.
        /// </summary>
        /// <returns>
        /// A list of <see cref="FinancialRecord"/> representing expenses.
        /// </returns>
        IEnumerable<FinancialRecord> GetAllExpenseRecords();

        /// <summary>
        /// Retrieves records that match the specified date.
        /// </summary>
        /// <param name="date">The date to filter records by.</param>
        /// <returns>
        /// A list of <see cref="FinancialRecord"/> that occur on the specified date.
        /// </returns>
        IEnumerable<FinancialRecord> GetByDate(DateOnly date);

        /// <summary>
        /// Retrieves records that match the specified month and year.
        /// </summary>
        /// <param name="month">The month to filter by.</param>
        /// <param name="year">The year to filter by.</param>
        /// <returns>
        /// A list of <see cref="FinancialRecord"/> that occur within the specified month and year.
        /// </returns>
        IEnumerable<FinancialRecord> GetByMonthAndYear(int month, int year);

        /// <summary>
        /// Retrieves records that match the specified amount.
        /// </summary>
        /// <param name="amount">The amount to filter records by.</param>
        /// <returns>
        /// A list of <see cref="FinancialRecord"/> with the specified amount.
        /// </returns>
        IEnumerable<FinancialRecord> GetByAmount(decimal amount);

        /// <summary>
        /// Retrieves income records that match the specified <see cref="IncomeSource"/>.
        /// </summary>
        /// <param name="source">The <see cref="IncomeSource"/> to filter by.</param>
        /// <returns>
        /// A list of <see cref="FinancialRecord"/> representing incomes with the specified source.
        /// </returns>
        IEnumerable<FinancialRecord> GetBySource(IncomeSource source);

        /// <summary>
        /// Retrieves expense records that match the specified <see cref="ExpenseCategory"/>.
        /// </summary>
        /// <param name="category">The <see cref="ExpenseCategory"/> to filter by.</param>
        /// <returns>
        /// A list of <see cref="FinancialRecord"/> representing expenses in the specified category.
        /// </returns>
        IEnumerable<FinancialRecord> GetByCategory(ExpenseCategory category);

        /// <summary>
        /// Retrieves a <see cref="FinancialRecord"/> by its identifier.
        /// </summary>
        /// <param name="recordId">The identifier of the <see cref="FinancialRecord"/>.</param>
        /// <returns>
        /// The matching <see cref="FinancialRecord"/> if found.
        /// </returns>
        FinancialRecord? GetById(string recordId);

        /// <summary>
        /// Deletes the specified <see cref="FinancialRecord"/> from the repository.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to delete.</param>
        void DeleteRecord(FinancialRecord record);

        /// <summary>
        /// Updates the date of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="date">The new date.</param>
        void UpdateRecordDate(FinancialRecord record, DateOnly date);

        /// <summary>
        /// Updates the amount of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="amount">The new amount value.</param>
        void UpdateRecordAmount(FinancialRecord record, decimal amount);

        /// <summary>
        /// Updates the source of the specified <see cref="Income"/>.
        /// </summary>
        /// <param name="record">The <see cref="Income"/> record to update.</param>
        /// <param name="source">The new <see cref="IncomeSource"/>.</param>
        void UpdateRecordSource(Income record, IncomeSource source);

        /// <summary>
        /// Updates the category of the specified <see cref="Expense"/>.
        /// </summary>
        /// <param name="record">The <see cref="Expense"/> record to update.</param>
        /// <param name="category">The new <see cref="ExpenseCategory"/>.</param>
        void UpdateRecordCategory(Expense record, ExpenseCategory category);

        /// <summary>
        /// Updates the description of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="description">The new description value.</param>
        void UpdateRecordDescription(FinancialRecord record, string? description);

        /// <summary>
        /// Determines whether a record identifier already exists in the repository.
        /// </summary>
        /// <param name="recordId">The record identifier to check.</param>
        /// <returns>
        /// True if the record exists, otherwise false.
        /// </returns>
        bool RecordIdExists(string recordId);
    }
}