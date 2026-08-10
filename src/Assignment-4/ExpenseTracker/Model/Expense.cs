using ExpenseTracker.Enums;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents an expense record that derives from <see cref="FinancialRecord"/>.
    /// </summary>
    internal class Expense : FinancialRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Expense"/> class.
        /// </summary>
        /// <param name="id">The unique ID of the <see cref="Expense"/> record.</param>
        /// <param name="date">The date of the <see cref="Expense"/> record.</param>
        /// <param name="amount">The amount associated with the <see cref="Expense"/> record.</param>
        /// <param name="description">The description of the <see cref="Expense"/> record.</param>
        /// <param name="category">The <see cref="ExpenseCategory"/> of the <see cref="Expense"/>.</param>
        public Expense(string id, DateOnly date, decimal amount, string? description, ExpenseCategory category)
            : base(id, date, amount, description)
        {
            this.Category = category;
        }

        /// <summary>
        /// Gets or sets the category of the <see cref="Expense"/>.
        /// </summary>
        /// <value>The category of the <see cref="Expense"/> as an <see cref="ExpenseCategory"/> value.</value>
        public ExpenseCategory Category { get; set; }

        /// <summary>
        /// Gets the type of the <see cref="FinancialRecord"/>.
        /// </summary>
        /// <value>The type name of the record (for <see cref="Expense"/> this is "Expense").</value>
        public override string Type => "Expense";

        /// <summary>
        /// Gets the classification of the <see cref="Expense"/> based on its <see cref="ExpenseCategory"/>.
        /// </summary>
        /// <value>
        /// The classification string derived from the <see cref="ExpenseCategory"/> of this <see cref="Expense"/>.
        /// </value>
        public override string Classification => this.Category.ToString();

        /// <summary>
        /// Creates a copy of the current <see cref="Expense"/>.
        /// </summary>
        /// <returns>
        /// A cloned instance of the current <see cref="FinancialRecord"/> (specifically an <see cref="Expense"/>).
        /// </returns>
        public override FinancialRecord Clone()
        {
            return new Expense(this.Id, this.Date, this.Amount, this.Description, this.Category);
        }
    }
}