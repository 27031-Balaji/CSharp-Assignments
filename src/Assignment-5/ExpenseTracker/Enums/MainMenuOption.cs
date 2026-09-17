namespace ExpenseTracker.Enums
{
    /// <summary>
    /// Represents the choices available in the main menu.
    /// </summary>
    internal enum MainMenuOption
    {
        /// <summary>
        /// Selects the option to add a new record.
        /// </summary>
        AddRecord,

        /// <summary>
        /// Selects the option to view records.
        /// </summary>
        ViewRecord,

        /// <summary>
        /// Selects the option to search for specific records.
        /// </summary>
        SearchRecord,

        /// <summary>
        /// Selects the option to delete an existing record.
        /// </summary>
        DeleteRecord,

        /// <summary>
        /// Selects the option to edit an existing record.
        /// </summary>
        EditRecord,

        /// <summary>
        /// Selects the option to make a financial summary.
        /// </summary>
        FinancialSummary,

        /// <summary>
        /// Selects the option to delete the account.
        /// </summary>
        DeleteAccount,

        /// <summary>
        /// Selects the option to logout.
        /// </summary>
        Logout,
    }
}