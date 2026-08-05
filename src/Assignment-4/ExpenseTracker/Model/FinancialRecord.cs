namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents a financial record with an ID, date, and amount.
    /// </summary>
    internal class FinancialRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialRecord"/> class.
        /// </summary>
        /// <param name="id">The unique ID of the record.</param>
        /// <param name="date">The date in which the record is taken.</param>
        /// <param name="amount">The amount associated with the record.</param>
        protected FinancialRecord(string id, DateOnly date, decimal amount)
        {
            this.Id = id;
            this.Date = date;
            this.Amount = amount;
        }

        /// <summary>
        /// Gets the unique ID of the financial record.
        /// </summary>
        /// <value>The unique ID of the record.</value>
        public string Id { get; }

        /// <summary>
        /// Gets or sets the date of the financial record.
        /// </summary>
        /// <value>The date of the record.</value>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets the amount of the financial record.
        /// </summary>
        /// <value>The amount of the record.</value>
        public decimal Amount { get; set; }
    }
}