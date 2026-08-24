namespace ExpenseTracker.Enums
{
    /// <summary>
    /// Represents the choices available in the view menu.
    /// </summary>
    internal enum ViewMenuOption
    {
        /// <summary>
        /// Indicates an invalid menu selection.
        /// </summary>
        Invalid,

        /// <summary>
        /// Selects the option to view all the records.
        /// </summary>
        ViewAll,

        /// <summary>
        /// Selects the option to view all the income records.
        /// </summary>
        ViewIncomes,

        /// <summary>
        /// Selects the option to view all the expense records.
        /// </summary>
        ViewExpenses,

        /// <summary>
        /// Returns to the main menu.
        /// </summary>
        BackToMainMenu,
    }
}