namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents the expense record with an ID, date, amount, and category.
    /// </summary>
    internal class Expense : FinancialRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Expense"/> class.
        /// </summary>
        /// <param name="id">The unique ID of the expense record.</param>
        /// <param name="date">The date of the record.</param>
        /// <param name="amount">The amount associated with the record.</param>
        /// <param name="category">The category of the expense.</param>
        public Expense(string id, DateOnly date, decimal amount, string category)
            : base(id, date, amount)
        {
            this.Category = category;
        }

        /// <summary>
        /// Gets or sets the category of the expense.
        /// </summary>
        /// <value>The category of the expense.</value>
        public string Category { get; set; }
    }
}
