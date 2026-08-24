using ExpenseTracker.Enums;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents a financial record with an ID, date, and amount.
    /// </summary>
    internal abstract class FinancialRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialRecord"/> class.
        /// </summary>
        /// <param name="id">The unique ID of the <see cref="FinancialRecord"/>.</param>
        /// <param name="date">The date in which the <see cref="FinancialRecord"/> is taken.</param>
        /// <param name="amount">The amount associated with the <see cref="FinancialRecord"/>.</param>
        /// <param name="description">The description of the <see cref="FinancialRecord"/>.</param>
        protected FinancialRecord(string id, DateOnly date, decimal amount, string? description)
        {
            this.Id = id;
            this.Date = date;
            this.Amount = amount;
            this.Description = description;
        }

        /// <summary>
        /// Gets the unique ID of the <see cref="FinancialRecord"/>.
        /// </summary>
        /// <value>The unique ID of the <see cref="FinancialRecord"/>.</value>
        public string Id { get; }

        /// <summary>
        /// Gets or sets the date of the <see cref="FinancialRecord"/>.
        /// </summary>
        /// <value>The date of the <see cref="FinancialRecord"/>.</value>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets the amount of the <see cref="FinancialRecord"/>.
        /// </summary>
        /// <value>The amount of the <see cref="FinancialRecord"/>.</value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the description of the <see cref="FinancialRecord"/>.
        /// </summary>
        /// <value>The description of the <see cref="FinancialRecord"/>.</value>
        public string? Description { get; set; }

        /// <summary>
        /// Gets the type of the <see cref="FinancialRecord"/>.
        /// </summary>
        /// <value>
        /// The type of the <see cref="FinancialRecord"/>.
        /// </value>
        public abstract RecordType Type { get; }

        /// <summary>
        /// Gets the classification of the <see cref="FinancialRecord"/>.
        /// </summary>
        /// <value>
        /// The classification of the <see cref="FinancialRecord"/>.
        /// </value>
        public abstract string Classification { get; }
    }
}