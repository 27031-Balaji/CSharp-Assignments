using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    /// <summary>
    /// Handles the in-memory storage and management of financial records, including income and expenses.
    /// </summary>
    internal class ExpenseRepository
    {
        private readonly List<FinancialRecord> _records;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseRepository"/> class.
        /// </summary>
        public ExpenseRepository()
        {
            this._records = new List<FinancialRecord>();
        }

        public int RecordCount { get => this._records.Count; }

        public void AddRecord(FinancialRecord record)
        {
            this._records.Add(record);
        }

        public List<FinancialRecord> GetAllRecords()
        {
            List<FinancialRecord> duplicateRecords = new List<FinancialRecord>();

            foreach (FinancialRecord record in this._records)
            {
                duplicateRecords.Add(record.Clone());
            }

            return duplicateRecords;
        }

        public List<FinancialRecord> GetAllIncomeRecords()
        {
            List<FinancialRecord> duplicateIncomeRecords = new List<FinancialRecord>();
            foreach (FinancialRecord record in this._records)
            {
                if (record is Income)
                {
                    duplicateIncomeRecords.Add(record.Clone());
                }
            }

            return duplicateIncomeRecords;
        }

        public List<FinancialRecord> GetAllExpenseRecords()
        {
            List<FinancialRecord> duplicateExpenseRecords = new List<FinancialRecord>();
            foreach (FinancialRecord record in this._records)
            {
                if (record is Expense)
                {
                    duplicateExpenseRecords.Add(record.Clone());
                }
            }

            return duplicateExpenseRecords;
        }

        public List<FinancialRecord> GetByDate(DateOnly date)
        {
            List<FinancialRecord> dateRecords = new List<FinancialRecord>();
            foreach (FinancialRecord record in this._records)
            {
                if (record.Date == date)
                {
                    dateRecords.Add(record.Clone());
                }
            }

            return dateRecords;
        }

        public List<FinancialRecord> GetByAmount(decimal amount)
        {
            List<FinancialRecord> amountRecords = new List<FinancialRecord>();
            foreach (FinancialRecord record in this._records)
            {
                if (record.Amount == amount)
                {
                    amountRecords.Add(record.Clone());
                }
            }

            return amountRecords;
        }

        public List<FinancialRecord> GetBySource(IncomeSource source)
        {
            List<FinancialRecord> sourceRecords = new List<FinancialRecord>();
            foreach (FinancialRecord record in this._records)
            {
                if (record is Income income && income.Source == source)
                {
                    sourceRecords.Add(record.Clone());
                }
            }

            return sourceRecords;
        }

        public List<FinancialRecord> GetByCategory(ExpenseCategory category)
        {
            List<FinancialRecord> categoryRecords = new List<FinancialRecord>();
            foreach (FinancialRecord record in this._records)
            {
                if (record is Expense expense && expense.Category == category)
                {
                    categoryRecords.Add(record.Clone());
                }
            }

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

        public bool RecordIdExists(string recordId)
        {
            return this._records.Any(record => record.Id == recordId);
        }
    }
}
