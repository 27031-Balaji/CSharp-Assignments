namespace ExpenseTracker.Enums
{
    /// <summary>
    /// Represents the choices available in the add menu.
    /// </summary>
    public enum AddMenuOption
    {
        /// <summary>
        /// Selects the option to add a new <see cref="Model.Income"/> record.
        /// </summary>
        AddIncome,

        /// <summary>
        /// Selects the option to add a new <see cref="Model.Expense"/> record.
        /// </summary>
        AddExpense,

        /// <summary>
        /// Returns to the main menu.
        /// </summary>
        BackToMainMenu,
    }
}