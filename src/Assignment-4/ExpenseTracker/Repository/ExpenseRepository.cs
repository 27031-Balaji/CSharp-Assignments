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

        /// <summary>
        /// Gets or sets the net balance of the financial records, calculated as the sum of income minus the sum of expenses.
        /// </summary>
        /// <value>The net balance after the user's income and expenses.</value>
        public decimal Balance { get; set; }

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

        public bool RecordIdExists(string recordId)
        {
            return this._records.Any(record => record.Id == recordId);
        }
    }
}
