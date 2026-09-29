namespace ExpenseTracker.Enums
{
    /// <summary>
    /// Represents the choices available in the edit menu.
    /// </summary>
    public enum EditMenuOption
    {
        /// <summary>
        /// Selects the option to edit the date of the record.
        /// </summary>
        Date,

        /// <summary>
        /// Selects the option to edit the amount of the record.
        /// </summary>
        Amount,

        /// <summary>
        /// Selects the option to edit the classification of the record.
        /// </summary>
        Classification,

        /// <summary>
        /// Selects the option to edit the description of the record.
        /// </summary>
        Description,

        /// <summary>
        /// Saves the edit operation and goes back to the main menu.
        /// </summary>
        SaveAndExit,
    }
}