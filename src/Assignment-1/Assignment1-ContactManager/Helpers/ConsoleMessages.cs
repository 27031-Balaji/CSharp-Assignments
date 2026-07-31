namespace Assignment1.Helpers
{
    /// <summary>
    /// ConsoleMessages class is used to store messages used for printing in the UI.
    /// </summary>
    internal class ConsoleMessages
    {
        /// <summary>
        /// Message displayed when an invalid name is entered.
        /// </summary>
        public const string InvalidNameMessage = "Enter a valid name.";

        /// <summary>
        /// Message displayed when an invalid email address is entered.
        /// </summary>
        public const string InvalidEmailMessage = "Enter a valid email.";

        /// <summary>
        /// Message displayed when an invalid phone number is entered.
        /// </summary>
        public const string InvalidPhoneMessage = "Enter a valid phone number.";

        /// <summary>
        /// Message displayed when an invalid menu option is selected.
        /// </summary>
        public const string InvalidOptionMessage = "Enter a valid option.";

        /// <summary>
        /// Message displayed when no contacts are available.
        /// </summary>
        public const string ContactListEmptyMessage = "Contact list is empty.";

        /// <summary>
        /// Message displayed when the requested contact cannot be found.
        /// </summary>
        public const string ContactNotFoundMessage = "Contact not found.";

        /// <summary>
        /// Message displayed when a phone number already exists in the contact list.
        /// </summary>
        public const string PhoneExistsMessage = "Phone number already exists.";

        /// <summary>
        /// Message displayed when a contact is added successfully.
        /// </summary>
        public const string ContactAddedMessage = "Contact added successfully.";

        /// <summary>
        /// Message displayed when a contact is deleted successfully.
        /// </summary>
        public const string ContactDeletedMessage = "Contact deleted successfully.";

        /// <summary>
        /// Message displayed when contact deletion fails.
        /// </summary>
        public const string DeleteFailedMessage = "Failed to delete contact.";

        /// <summary>
        /// Message displayed when a contact's name is updated successfully.
        /// </summary>
        public const string NameUpdatedMessage = "Name updated successfully.";

        /// <summary>
        /// Message displayed when a contact's email address is updated successfully.
        /// </summary>
        public const string EmailUpdatedMessage = "Email updated successfully.";

        /// <summary>
        /// Message displayed when a contact's notes are updated successfully.
        /// </summary>
        public const string NotesUpdatedMessage = "Notes updated successfully.";

        /// <summary>
        /// Message displayed when the contact edit operation is completed.
        /// </summary>
        public const string EditCompletedMessage = "Edit function completed.";

        /// <summary>
        /// Message displayed when the application is being closed.
        /// </summary>
        public const string ExitMessage = "Exiting Application...";
    }
}