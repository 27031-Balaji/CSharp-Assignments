namespace ExpenseTracker.Helper
{
    /// <summary>
    /// Used to store menu-oriented messages used for printing in the UI.
    /// </summary>
    internal static class MenuMessages
    {
        /// <summary>
        /// Menu displayed for the main menu.
        /// </summary>
        public static readonly string[] MainMenu =
        {
            "Add Record",
            "View Record",
            "Search Record",
            "Delete Record",
            "Edit Record",
            "Financial Summary",
            "Delete Account",
            "Logout",
        };

        /// <summary>
        /// Menu displayed for the add menu.
        /// </summary>
        public static readonly string[] AddMenu =
        {
            "Add Income",
            "Add Expense",
            "Back",
        };

        /// <summary>
        /// Menu displayed for the view menu.
        /// </summary>
        public static readonly string[] ViewMenu =
        {
            "View All Records",
            "View All Incomes",
            "View All Expenses",
            "Back to Main Menu",
        };

        /// <summary>
        /// Menu displayed for the edit menu.
        /// </summary>
        public static readonly string[] EditMenu =
        {
            "Edit Date",
            "Edit Amount",
            "Edit Classification",
            "Edit Description",
            "Back to Main Menu",
        };
    }
}