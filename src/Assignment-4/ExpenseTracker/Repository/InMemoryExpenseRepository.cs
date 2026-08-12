using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    /// <summary>
    /// Handles in-memory storage and management of <see cref="FinancialRecord"/> instances.
    /// </summary>
    internal class InMemoryExpenseRepository : IExpenseRepository
    {
        private readonly List<FinancialRecord> records;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryExpenseRepository"/> class.
        /// </summary>
        public InMemoryExpenseRepository()
        {
            this.records = new List<FinancialRecord>();
        }

        /// <summary>
        /// Gets the total number of stored <see cref="FinancialRecord"/> instances.
        /// </summary>
        /// <value>The number of records currently stored.</value>
        public int RecordCount { get => this.records.Count; }

        /// <summary>
        /// Adds a <see cref="FinancialRecord"/> to the repository.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to add.</param>
        public void AddRecord(FinancialRecord record)
        {
            this.records.Add(record);
        }

        /// <summary>
        /// Retrieves all stored <see cref="FinancialRecord"/> instances as a cloned list.
        /// </summary>
        /// <returns>A list of <see cref="FinancialRecord"/> containing cloned records.</returns>
        public IEnumerable<FinancialRecord> GetAllRecords()
        {
            IEnumerable<FinancialRecord> duplicateRecords = this.records
                                                        .Select(this.CloneRecord);

            return duplicateRecords;
        }

        /// <summary>
        /// Retrieves all stored income records.
        /// </summary>
        /// <returns>A list of <see cref="FinancialRecord"/> representing income records.</returns>
        public IEnumerable<FinancialRecord> GetAllIncomeRecords()
        {
            IEnumerable<FinancialRecord> duplicateIncomeRecords = this.records
                                                    .Where(record => record is Income)
                                                    .Select(this.CloneRecord);

            return duplicateIncomeRecords;
        }

        /// <summary>
        /// Retrieves all stored expense records.
        /// </summary>
        /// <returns>A list of <see cref="FinancialRecord"/> representing expense records.</returns>
        public IEnumerable<FinancialRecord> GetAllExpenseRecords()
        {
            IEnumerable<FinancialRecord> duplicateExpenseRecords = this.records
                                                            .Where(record => record is Expense)
                                                            .Select(this.CloneRecord);

            return duplicateExpenseRecords;
        }

        /// <summary>
        /// Retrieves records that match the specified date.
        /// </summary>
        /// <param name="date">The date value to filter records by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> that occur on the specified date.</returns>
        public IEnumerable<FinancialRecord> GetByDate(DateOnly date)
        {
            IEnumerable<FinancialRecord> dateRecords = this.records
                                                .Where(record => record.Date == date)
                                                .Select(this.CloneRecord);

            return dateRecords;
        }

        /// <summary>
        /// Retrieves records that match the specified month and year.
        /// </summary>
        /// <param name="month">The month to filter by.</param>
        /// <param name="year">The year to filter by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> that occur within the specified month and year.</returns>
        public IEnumerable<FinancialRecord> GetByMonthAndYear(int month, int year)
        {
            IEnumerable<FinancialRecord> monthAndYearRecords = this.records
                                                .Where(record => record.Date.Month == month && record.Date.Year == year)
                                                .Select(this.CloneRecord);

            return monthAndYearRecords;
        }

        /// <summary>
        /// Retrieves records that match the specified amount.
        /// </summary>
        /// <param name="amount">The amount to filter records by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> with the specified amount.</returns>
        public IEnumerable<FinancialRecord> GetByAmount(decimal amount)
        {
            IEnumerable<FinancialRecord> amountRecords = this.records
                                                    .Where(record => record.Amount == amount)
                                                    .Select(this.CloneRecord);

            return amountRecords;
        }

        /// <summary>
        /// Retrieves income records that match the specified <see cref="IncomeSource"/>.
        /// </summary>
        /// <param name="source">The <see cref="IncomeSource"/> to filter by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing incomes with the specified source.</returns>
        public IEnumerable<FinancialRecord> GetBySource(IncomeSource source)
        {
            IEnumerable<FinancialRecord> sourceRecords = this.records
                                                    .Where(record => record is Income income && income.Source == source)
                                                    .Select(this.CloneRecord);

            return sourceRecords;
        }

        /// <summary>
        /// Retrieves expense records that match the specified <see cref="ExpenseCategory"/>.
        /// </summary>
        /// <param name="category">The <see cref="ExpenseCategory"/> to filter by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing expenses in the specified category.</returns>
        public IEnumerable<FinancialRecord> GetByCategory(ExpenseCategory category)
        {
            IEnumerable<FinancialRecord> categoryRecords = this.records
                                                    .Where(record => record is Expense expense && expense.Category == category)
                                                    .Select(this.CloneRecord);

            return categoryRecords;
        }

        /// <summary>
        /// Retrieves a <see cref="FinancialRecord"/> by its identifier.
        /// </summary>
        /// <param name="recordId">The identifier of the <see cref="FinancialRecord"/>.</param>
        /// <returns>The matching <see cref="FinancialRecord"/> if found.</returns>
        public FinancialRecord? GetById(string recordId)
        {
            FinancialRecord? record = this.records.Find(record => record.Id.Equals(recordId, StringComparison.OrdinalIgnoreCase));
            return record is null ? null : this.CloneRecord(record);
        }

        /// <summary>
        /// Deletes the specified <see cref="FinancialRecord"/> from the repository.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to delete.</param>
        public void DeleteRecord(FinancialRecord record)
        {
            FinancialRecord originalRecord = this.FindOriginalRecord(record.Id);
            this.records.Remove(originalRecord);
        }

        /// <summary>
        /// Updates the date of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="date">The new date.</param>
        public void UpdateRecordDate(FinancialRecord record, DateOnly date)
        {
            FinancialRecord originalRecord = this.FindOriginalRecord(record.Id);
            originalRecord.Date = date;
        }

        /// <summary>
        /// Updates the amount of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="amount">The new amount value.</param>
        public void UpdateRecordAmount(FinancialRecord record, decimal amount)
        {
            FinancialRecord originalRecord = this.FindOriginalRecord(record.Id);
            originalRecord.Amount = amount;
        }

        /// <summary>
        /// Updates the source of the specified <see cref="Income"/>.
        /// </summary>
        /// <param name="record">The <see cref="Income"/> to update.</param>
        /// <param name="source">The new <see cref="IncomeSource"/>.</param>
        public void UpdateRecordSource(Income record, IncomeSource source)
        {
            Income originalRecord = (Income)this.FindOriginalRecord(record.Id);
            originalRecord.Source = source;
        }

        /// <summary>
        /// Updates the category of the specified <see cref="Expense"/>.
        /// </summary>
        /// <param name="record">The <see cref="Expense"/> to update.</param>
        /// <param name="category">The new <see cref="ExpenseCategory"/>.</param>
        public void UpdateRecordCategory(Expense record, ExpenseCategory category)
        {
            Expense originalRecord = (Expense)this.FindOriginalRecord(record.Id);
            originalRecord.Category = category;
        }

        /// <summary>
        /// Updates the description of the specified <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="description">The new description value.</param>
        public void UpdateRecordDescription(FinancialRecord record, string? description)
        {
            FinancialRecord originalRecord = this.FindOriginalRecord(record.Id);
            originalRecord.Description = description;
        }

        /// <summary>
        /// Determines whether a record identifier already exists in the repository.
        /// </summary>
        /// <param name="recordId">The record identifier to check.</param>
        /// <returns>True if the record exists, otherwise false.</returns>
        public bool RecordIdExists(string recordId)
        {
            return this.records.Any(record => record.Id == recordId);
        }

        /// <summary>
        /// Retrieves the original record stored in the repository.
        /// </summary>
        /// <param name="recordId">The identifier of the record.</param>
        /// <returns>The original stored record.</returns>
        private FinancialRecord FindOriginalRecord(string recordId)
        {
            return this.records.First(record =>
                record.Id.Equals(recordId, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Creates a copy of the specified financial record.
        /// </summary>
        /// <param name="record">The financial record to copy.</param>
        /// <returns>A new instance of the same type as the provided record.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the record type is unknown.</exception>
        private FinancialRecord CloneRecord(FinancialRecord record)
        {
            return record switch
            {
                Income income => new Income(income.Id, income.Date, income.Amount, income.Description, income.Source),
                Expense expense => new Expense(expense.Id, expense.Date, expense.Amount, expense.Description, expense.Category),
                _ => throw new InvalidOperationException("Unknown record type.")
            };
        }
    }
}