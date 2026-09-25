using ExpenseTracker.Enums;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents an income record that derives from <see cref="FinancialRecord"/>.
    /// </summary>
    internal class Income : FinancialRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Income"/> class.
        /// </summary>
        /// <param name="id">The unique ID of the <see cref="Income"/> record.</param>
        /// <param name="date">The date in which the <see cref="Income"/> record is taken.</param>
        /// <param name="amount">The amount associated with the <see cref="Income"/> record.</param>
        /// <param name="description">The description of the <see cref="Income"/> record.</param>
        /// <param name="source">The <see cref="IncomeSource"/> of income.</param>
        public Income(string id, DateOnly date, decimal amount, string? description, IncomeSource source)
            : base(id, date, amount, description)
        {
            this.Source = source;
        }

        /// <summary>
        /// Gets or sets the source of the <see cref="Income"/>.
        /// </summary>
        /// <value>The source of the <see cref="Income"/> as an <see cref="IncomeSource"/> value.</value>
        public IncomeSource Source { get; set; }

        /// <summary>
        /// Gets the type of the <see cref="FinancialRecord"/>.
        /// </summary>
        /// <value>
        /// The type of the <see cref="FinancialRecord"/>.
        /// </value>
        public override RecordType Type => RecordType.Income;

        /// <summary>
        /// Gets the classification of the <see cref="Income"/> based on its <see cref="IncomeSource"/>.
        /// </summary>
        /// <value>
        /// The classification string derived from the <see cref="IncomeSource"/> of this <see cref="Income"/>.
        /// </value>
        public override string Classification => this.Source.ToString();
    }
}