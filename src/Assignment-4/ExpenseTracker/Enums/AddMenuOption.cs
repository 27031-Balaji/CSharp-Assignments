namespace ExpenseTracker.Enums
{
    /// <summary>
    /// Represents the choices available in the add menu.
    /// </summary>
    internal enum AddMenuOption
    {
        /// <summary>
        /// Indicates an invalid menu selection.
        /// </summary>
        Invalid,

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