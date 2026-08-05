using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    /// <summary>
    /// Handles the in-memory storage and management of financial records, including income and expenses.
    /// </summary>
    internal class InMemoryExpenseRepository
    {
        private readonly List<FinancialRecord> _records;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryExpenseRepository"/> class.
        /// </summary>
        public InMemoryExpenseRepository()
        {
            this._records = new List<FinancialRecord>();
        }

        /// <summary>
        /// Gets or sets the net balance of the financial records, calculated as the sum of income minus the sum of expenses.
        /// </summary>
        /// <value>The net balance after the user's income and expenses.</value>
        public decimal Balance { get; set; }
    }
}
