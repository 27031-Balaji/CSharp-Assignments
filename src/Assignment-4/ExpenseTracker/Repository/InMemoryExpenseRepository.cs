using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    /// <summary>
    /// Handles the in-memory storage and management of financial records, including income and expenses.
    /// </summary>
    internal class InMemoryExpenseRepository : IExpenseRepository
    {
        private readonly List<FinancialRecord> _records;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryExpenseRepository"/> class.
        /// </summary>
        public InMemoryExpenseRepository()
        {
            this._records = new List<FinancialRecord>();
        }

        public int RecordCount { get => this._records.Count; }

        public void AddRecord(FinancialRecord record)
        {
            this._records.Add(record);
        }

        public IEnumerable<FinancialRecord> GetAllRecords()
        {
            List<FinancialRecord> duplicateRecords = this._records
                                                        .Select(record => record.Clone())
                                                        .ToList();

            return duplicateRecords;
        }

        public IEnumerable<FinancialRecord> GetAllIncomeRecords()
        {
            List<FinancialRecord> duplicateIncomeRecords = this._records
                                                            .Where(record => record is Income)
                                                            .Select(record => record.Clone())
                                                            .ToList();

            return duplicateIncomeRecords;
        }

        public IEnumerable<FinancialRecord> GetAllExpenseRecords()
        {
            List<FinancialRecord> duplicateExpenseRecords = this._records
                                                            .Where(record => record is Expense)
                                                            .Select(record => record.Clone())
                                                            .ToList();

            return duplicateExpenseRecords;
        }

        public IEnumerable<FinancialRecord> GetByDate(DateOnly date)
        {
            List<FinancialRecord> dateRecords = this._records
                                                .Where(record => record.Date == date)
                                                .Select(record => record.Clone())
                                                .ToList();

            return dateRecords;
        }

        public IEnumerable<FinancialRecord> GetByMonthAndYear(int month, int year)
        {
            List<FinancialRecord> monthAndYearRecords = this._records
                                                        .Where(record => record.Date.Month == month && record.Date.Year == year)
                                                        .Select(record => record.Clone())
                                                        .ToList();

            return monthAndYearRecords;
        }

        public IEnumerable<FinancialRecord> GetByAmount(decimal amount)
        {
            List<FinancialRecord> amountRecords = this._records
                                                    .Where(record => record.Amount == amount)
                                                    .Select(record => record.Clone())
                                                    .ToList();

            return amountRecords;
        }

        public IEnumerable<FinancialRecord> GetBySource(IncomeSource source)
        {
            List<FinancialRecord> sourceRecords = this._records
                                                    .Where(record => record is Income income && income.Source == source)
                                                    .Select(record => record.Clone())
                                                    .ToList();

            return sourceRecords;
        }

        public IEnumerable<FinancialRecord> GetByCategory(ExpenseCategory category)
        {
            List<FinancialRecord> categoryRecords = this._records
                                                    .Where(record => record is Expense expense && expense.Category == category)
                                                    .Select(record => record.Clone())
                                                    .ToList();

            return categoryRecords;
        }

        public FinancialRecord? GetById(string recordId)
        {
            return this._records.Find(record => record.Id.Equals(recordId, StringComparison.OrdinalIgnoreCase));
        }

        public void DeleteRecord(FinancialRecord record)
        {
            this._records.Remove(record);
        }

        public void UpdateRecordDate(FinancialRecord record, DateOnly date)
        {
            record.Date = date;
        }

        public void UpdateRecordAmount(FinancialRecord record, decimal amount)
        {
            record.Amount = amount;
        }

        public void UpdateRecordSource(Income record, IncomeSource source)
        {
            record.Source = source;
        }

        public void UpdateRecordCategory(Expense record, ExpenseCategory category)
        {
            record.Category = category;
        }

        public void UpdateRecordDescription(FinancialRecord record, string? description)
        {
            record.Description = description;
        }

        public bool RecordIdExists(string recordId)
        {
            return this._records.Any(record => record.Id == recordId);
        }
    }
}