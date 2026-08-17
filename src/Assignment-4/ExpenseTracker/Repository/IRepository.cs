using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    /// <summary>
    /// Defines repository operations for storing and querying <see cref="FinancialRecord"/> instances.
    /// </summary>
    internal interface IRepository
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
        /// Retrieves all stored records with the specific filter.
        /// </summary>
        /// <param name="filter">The filter to search for.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing the filtered records.</returns>
        public IEnumerable<FinancialRecord> GetRecords(Func<FinancialRecord, bool>? filter = null);

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