namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents the income record with an ID, date, amount, and source.
    /// </summary>
    internal class Income : FinancialRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Income"/> class.
        /// </summary>
        /// <param name="id">The unique ID of the income record.</param>
        /// <param name="date">The date in which the record is taken.</param>
        /// <param name="amount">The amount associated with the record.</param>
        /// <param name="source">The source of income.</param>
        public Income(string id, DateOnly date, decimal amount, string source)
            : base(id, date, amount)
        {
            this.Source = source;
        }

        /// <summary>
        /// Gets or sets the source of the income.
        /// </summary>
        /// <value>The source of the income.</value>
        public string Source { get; set; }
    }
}
