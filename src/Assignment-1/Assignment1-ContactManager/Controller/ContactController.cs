using ContactManager.Helper;
using ContactManager.Model;
using ContactManager.Service;
using ContactManager.View;

namespace ContactManager.Controller
{
    /// <summary>
    /// Coordinates user interactions and application flow for contact management.
    /// </summary>
    internal class ContactController
    {
        private readonly ContactService _contactService;
        private readonly ContactHelper _contactHelper;
        private readonly ConsoleOperation _contactView;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactController"/> class with the specified contact service, helper, and view.
        /// </summary>
        /// <param name="contactService">The object of the contact services layer.</param>
        /// <param name="contactHelper">The object of the helper class.</param>
        /// <param name="contactView">The object of the view class.</param>
        public ContactController(ContactService contactService, ContactHelper contactHelper, ConsoleOperation contactView)
        {
            this._contactService = contactService;
            this._contactHelper = contactHelper;
            this._contactView = contactView;
        }

        /// <summary>
        /// Starts and runs the main controller loop.
        /// </summary>
        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                string? menuChoice = this._contactView.ShowMainMenu();
                switch (menuChoice.Trim().ToUpper())
                {
                    case "A":
                        this.AddContact();
                        break;

                    case "B":
                        this.DisplayContacts();
                        break;

                    case "C":
                        this.SearchContact();
                        break;

                    case "D":
                        this.DeleteContact();
                        break;

                    case "E":
                        this.EditContact();
                        break;

                    case "F":
                        this._contactView.ShowMessage(ConsoleMessages.ExitMessage);
                        Thread.Sleep(1000);
                        isRunning = false;
                        break;

                    default:
                        this._contactView.ShowMessage(ConsoleMessages.InvalidOptionMessage);
                        break;
                }
            }
        }

        /// <summary>
        /// This method checks if there are any contacts in the contact list.
        /// </summary>
        /// <returns>True if the list is empty, otherwise false.</returns>
        private bool HasContacts()
        {
            return !this._contactService.IsContactEmpty();
        }

        /// <summary>
        /// Handles user flow for adding a new contact.
        /// </summary>
        private void AddContact()
        {
            if (!this.GetContactName(out string name))
            {
                return;
            }

            if (!this.GetContactEmail(out string email))
            {
                return;
            }

            if (!this.GetContactPhoneNumber(out string phone, "add"))
            {
                return;
            }

            string? notes = this._contactView.ReadNotes().Trim();

            this._contactService.AddContact(name.Trim(), email, phone, notes);
            this._contactView.ShowMessage(ConsoleMessages.ContactAddedMessage);
            this._contactView.ClearScreenWithKey();
        }

        /// <summary>
        /// Displays all contacts if any exist.
        /// </summary>
        private void DisplayContacts()
        {
            if (!this.HasContacts())
            {
                this._contactView.ShowMessage(ConsoleMessages.ContactListEmptyMessage);
                return;
            }

            List<ContactInfo> contacts = this._contactService.GetAllContacts();
            this._contactView.DisplayContacts(contacts);
            this._contactView.ClearScreenWithKey();
        }

        /// <summary>
        /// Handles user flow for searching a contact by phone.
        /// </summary>
        private void SearchContact()
        {
            if (!this.HasContacts())
            {
                this._contactView.ShowMessage(ConsoleMessages.ContactListEmptyMessage);
                return;
            }

            if (!this.GetRegisteredContactPhoneNumber(out string phone, "search"))
            {
                return;
            }

            ContactInfo? contact = this._contactService.SearchContactByPhoneNumber(phone);
            this._contactView.DisplayContact(contact!);
            this._contactView.ClearScreenWithKey();
        }

        /// <summary>
        /// Handles user flow for deleting a contact by phone.
        /// </summary>
        private void DeleteContact()
        {
            if (!this.HasContacts())
            {
                this._contactView.ShowMessage(ConsoleMessages.ContactListEmptyMessage);
                return;
            }

            if (!this.GetRegisteredContactPhoneNumber(out string phone, "delete"))
            {
                return;
            }

            bool isDeleted = this._contactService.DeleteContactByPhoneNumber(phone);
            string statusMessage = isDeleted
                       ? ConsoleMessages.ContactDeletedMessage
                       : ConsoleMessages.DeleteFailedMessage;
            this._contactView.ShowMessage(statusMessage);
            this._contactView.ClearScreenWithKey();
        }

        /// <summary>
        /// Handles user flow for editing contact details.
        /// </summary>
        private void EditContact()
        {
            if (!this.HasContacts())
            {
                this._contactView.ShowMessage(ConsoleMessages.ContactListEmptyMessage);
                return;
            }

            if (!this.GetRegisteredContactPhoneNumber(out string phone, "edit"))
            {
                return;
            }

            bool isRunning = true;
            while (isRunning)
            {
                string? menuChoice = this._contactView.ShowEditMenu();
                switch (menuChoice.Trim().ToUpper())
                {
                    case "A":
                        this.EditContactName(phone);
                        isRunning = false;
                        break;

                    case "B":
                        this.EditContactEmail(phone);
                        isRunning = false;
                        break;

                    case "C":
                        this.EditContactPhone(phone);
                        isRunning = false;
                        break;

                    case "D":
                        this.EditContactNotes(phone);
                        isRunning = false;
                        break;

                    default:
                        if (!this.CanRetry(ConsoleMessages.InvalidOptionMessage))
                        {
                            isRunning = false;
                            this._contactView.ShowMessage(ConsoleMessages.EditCompletedMessage);
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Used to get the right contact name from the user and validate it. If the name is invalid, the user is prompted to retry or exit.
        /// </summary>
        /// <param name="contactName">The contact name to be received from the user.</param>
        /// <returns>True if the user entered valid name, else false.</returns>
        private bool GetContactName(out string contactName)
        {
            contactName = string.Empty;
            bool isNameValid = false;
            while (!isNameValid)
            {
                contactName = this._contactView.ReadName().Trim();
                if (this._contactHelper.IsValidName(contactName))
                {
                    isNameValid = true;
                    break;
                }

                if (!this.CanRetry(ConsoleMessages.InvalidNameMessage))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Used to get the right contact email from the user and validate it. If the email is invalid, the user is prompted to retry or exit.
        /// </summary>
        /// <param name="contactEmail">The contact email to be received from the user.</param>
        /// <returns>True if the user entered valid email, else false.</returns>
        private bool GetContactEmail(out string contactEmail)
        {
            contactEmail = string.Empty;
            bool isEmailValid = false;
            while (!isEmailValid)
            {
                contactEmail = this._contactView.ReadEmail().Trim();
                if (this._contactHelper.IsValidEmail(contactEmail))
                {
                    isEmailValid = true;
                    break;
                }

                if (!this.CanRetry(ConsoleMessages.InvalidEmailMessage))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Used to get the right contact phone from the user and validate it. If the phone is invalid or already registered, the user is prompted to retry or exit.
        /// </summary>
        /// <param name="contactPhoneNumber">The contact phone number to be received from the user.</param>
        /// <returns>True if the user entered valid phone number, else false.</returns>
        private bool GetContactPhoneNumber(out string contactPhoneNumber, string operation)
        {
            contactPhoneNumber = string.Empty;
            bool isPhoneValid = false;

            while (!isPhoneValid)
            {
                contactPhoneNumber = this._contactView.ReadPhone(operation).Trim();
                string errorMessage = string.Empty;
                if (!this._contactHelper.IsValidPhone(contactPhoneNumber))
                {
                    errorMessage = ConsoleMessages.InvalidPhoneMessage;
                }
                else if (this._contactService.IsPhoneRegistered(contactPhoneNumber))
                {
                    errorMessage = ConsoleMessages.PhoneExistsMessage;
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    if (!this.CanRetry(errorMessage))
                    {
                        return false;
                    }

                    continue;
                }

                isPhoneValid = true;
            }

            return true;
        }

        /// <summary>
        /// Used to get the right phone number to search, delete or edit a contact. If the phone is invalid or not registered, the user is prompted to retry or exit.
        /// </summary>
        /// <param name="contactPhoneNumber">The contact phone number to be validated.</param>
        /// <param name="operation">The operation to be done with the phone number (edit, delete, search).</param>
        /// <returns>True if the phone number is valid, else false.</returns>
        private bool GetRegisteredContactPhoneNumber(out string contactPhoneNumber, string operation)
        {
            contactPhoneNumber = string.Empty;
            bool isPhoneValid = false;

            while (!isPhoneValid)
            {
                contactPhoneNumber = this._contactView.ReadPhone(operation).Trim();
                string errorMessage = string.Empty;
                if (!this._contactHelper.IsValidPhone(contactPhoneNumber))
                {
                    errorMessage = ConsoleMessages.InvalidPhoneMessage;
                }
                else if (!this._contactService.IsPhoneRegistered(contactPhoneNumber))
                {
                    errorMessage = ConsoleMessages.ContactNotFoundMessage;
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    if (!this.CanRetry(errorMessage))
                    {
                        return false;
                    }

                    continue;
                }

                isPhoneValid = true;
            }

            return true;
        }

        /// <summary>
        /// Used to edit the contact name for a given phone number. If the name is invalid, the user is prompted to retry or exit.
        /// </summary>
        /// <param name="phoneNo">The phone number of the contact to be edited.</param>
        private void EditContactName(string phoneNo)
        {
            if (!this.GetContactName(out string name))
            {
                return;
            }

            bool isUpdated = this._contactService.EditContactNameByPhoneNumber(phoneNo, name);
            string statusMessage = isUpdated
                       ? ConsoleMessages.NameUpdatedMessage
                       : ConsoleMessages.ContactNotFoundMessage;
            this._contactView.ShowMessage(statusMessage);
            this._contactView.ClearScreenWithKey();
        }

        /// <summary>
        /// Used to edit the contact email for a given phone number. If the email is invalid, the user is prompted to retry or exit.
        /// </summary>
        /// <param name="phoneNo">The phone number of the contact to be edited.</param>
        private void EditContactEmail(string phoneNo)
        {
            if (!this.GetContactEmail(out string email))
            {
                return;
            }

            bool isUpdated = this._contactService.EditContactEmailByPhoneNumber(phoneNo, email);
            string statusMessage = isUpdated
                       ? ConsoleMessages.EmailUpdatedMessage
                       : ConsoleMessages.ContactNotFoundMessage;
            this._contactView.ShowMessage(statusMessage);
            this._contactView.ClearScreenWithKey();
        }

        /// <summary>
        /// Used to edit the contact email for a given phone number. If the email is invalid, the user is prompted to retry or exit.
        /// </summary>
        /// <param name="phoneNo">The phone number of the contact to be edited.</param>
        private void EditContactPhone(string phoneNo)
        {
            if (!this.GetContactPhoneNumber(out string phone, string.Empty))
            {
                return;
            }

            bool isUpdated = this._contactService.EditContactPhoneByPhoneNumber(phoneNo, phone);
            string statusMessage = isUpdated
                       ? ConsoleMessages.PhoneUpdatedMessage
                       : ConsoleMessages.ContactNotFoundMessage;
            this._contactView.ShowMessage(statusMessage);
            this._contactView.ClearScreenWithKey();
        }

        /// <summary>
        /// Used to edit the contact notes for a given phone number.
        /// </summary>
        /// <param name="phoneNo">The phone number of the contact to be edited.</param>
        private void EditContactNotes(string phoneNo)
        {
            string? notes = this._contactView.ReadNotes();
            bool isUpdated = this._contactService.EditContactNotesByPhoneNumber(phoneNo, notes);
            string statusMessage = isUpdated
                       ? ConsoleMessages.NotesUpdatedMessage
                       : ConsoleMessages.ContactNotFoundMessage;
            this._contactView.ShowMessage(statusMessage);
            this._contactView.ClearScreenWithKey();
        }

        /// <summary>
        /// Displays an invalid input message for the specified field and prompts the user to decide whether to retry the operation.
        /// </summary>
        /// <param name="message">The message to be printed for invalid input.</param>
        /// <returns>True if user chooses to retry, else false.</returns>
        private bool CanRetry(string message)
        {
            this._contactView.ShowMessage(message);
            bool shouldRetry = this._contactView.AskRetry();
            if (!shouldRetry)
            {
                this._contactView.ClearScreen();
            }

            return shouldRetry;
        }
    }
}