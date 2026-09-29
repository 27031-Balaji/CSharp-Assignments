using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    /// <summary>
    /// Defines repository operations for storing and querying <see cref="FinancialRecord"/> instances.
    /// </summary>
    internal interface IFinanceRepository
    {
        /// <summary>
        /// Loads the records from the file into memory.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        void LoadRecords(Guid userId);

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
        /// <returns>True if delete operation is successful, else false.</returns>
        bool DeleteRecord(FinancialRecord record);

        /// <summary>
        /// Updates the data of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <returns>True if edit operation is successful, else false.</returns>
        bool UpdateRecord(FinancialRecord record);

        /// <summary>
        /// Deletes the <see cref="FinancialRecord"/> mapped with the user's id.
        /// </summary>
        void DeleteCurrentUserRecords();

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