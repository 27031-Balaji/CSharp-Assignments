namespace ExpenseTracker.Enums
{
    /// <summary>
    /// Represents available categories for an <see cref="Model.Expense"/>.
    /// </summary>
    public enum ExpenseCategory
    {
        /// <summary>
        /// Housing-related expenses such as rent or mortgage.
        /// </summary>
        Housing,

        /// <summary>
        /// Utility bills like Internet, etc.,
        /// </summary>
        Utilities,

        /// <summary>
        /// Grocery and supermarket purchases.
        /// </summary>
        Groceries,

        /// <summary>
        /// Dining out and restaurant expenses.
        /// </summary>
        Dining,

        /// <summary>
        /// Transportation costs like petrol, etc.,
        /// </summary>
        Transportation,

        /// <summary>
        /// Medical and healthcare expenses.
        /// </summary>
        Healthcare,

        /// <summary>
        /// Education-related expenses like books, tuition, etc.,
        /// </summary>
        Education,

        /// <summary>
        /// Purchases of goods such as clothing or electronics.
        /// </summary>
        Shopping,

        /// <summary>
        /// Entertainment and leisure spending.
        /// </summary>
        Entertainment,

        /// <summary>
        /// Insurance related payments.
        /// </summary>
        Insurance,

        /// <summary>
        /// Loan repayments like student loan, etc.,
        /// </summary>
        LoanRepayment,

        /// <summary>
        /// Charitable donations and contributions.
        /// </summary>
        Charity,

        /// <summary>
        /// Travel and accommodation expenses.
        /// </summary>
        Travel,

        /// <summary>
        /// Any other expense category not listed above.
        /// </summary>
        Other,
    }
}