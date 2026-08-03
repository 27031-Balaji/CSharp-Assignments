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
        private readonly ContactService _contactService = new ContactService();
        private readonly ContactHelper _helper = new ContactHelper();
        private readonly ConsoleOperation _view = new ConsoleOperation();

        /// <summary>
        /// Starts and runs the main controller loop.
        /// </summary>
        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                string? menuChoice = this._view.ShowMainMenu();
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
                        this._view.ShowMessage(ConsoleMessages.ExitMessage);
                        Thread.Sleep(1000);
                        isRunning = false;
                        break;

                    default:
                        this._view.ShowMessage(ConsoleMessages.InvalidOptionMessage);
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
            return this._contactService.IsContactEmpty() ? false : true;
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

            if (!this.GetContactPhoneNumber(out string phone))
            {
                return;
            }

            string? notes = this._view.ReadNotes();

            this._contactService.AddContact(name.Trim(), email, phone, notes);
            this._view.ShowMessage(ConsoleMessages.ContactAddedMessage);
            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Displays all contacts if any exist.
        /// </summary>
        private void DisplayContacts()
        {
            if (!this.HasContacts())
            {
                this._view.ShowMessage(ConsoleMessages.ContactListEmptyMessage);
                return;
            }

            List<ContactInfo> contacts = this._contactService.GetAllContacts();
            this._view.DisplayContacts(contacts);
            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Handles user flow for searching a contact by phone.
        /// </summary>
        private void SearchContact()
        {
            if (!this.HasContacts())
            {
                this._view.ShowMessage(ConsoleMessages.ContactListEmptyMessage);
                return;
            }

            if (!this.GetRegisteredContactPhoneNumber(out string phone, "search"))
            {
                return;
            }

            ContactInfo? contact = this._contactService.SearchContact(phone);
            this._view.DisplayContact(contact!);
            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Handles user flow for deleting a contact by phone.
        /// </summary>
        private void DeleteContact()
        {
            if (!this.HasContacts())
            {
                this._view.ShowMessage(ConsoleMessages.ContactListEmptyMessage);
                return;
            }

            if (!this.GetRegisteredContactPhoneNumber(out string phone, "delete"))
            {
                return;
            }

            bool isDeleted = this._contactService.DeleteContact(phone);
            string statusMessage = isDeleted
                       ? ConsoleMessages.ContactDeletedMessage
                       : ConsoleMessages.DeleteFailedMessage;
            this._view.ShowMessage(statusMessage);
            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Handles user flow for editing contact details.
        /// </summary>
        private void EditContact()
        {
            if (!this.HasContacts())
            {
                this._view.ShowMessage(ConsoleMessages.ContactListEmptyMessage);
                return;
            }

            if (!this.GetRegisteredContactPhoneNumber(out string phone, "edit"))
            {
                return;
            }

            bool isRunning = true;
            while (isRunning)
            {
                string? menuChoice = this._view.ShowEditMenu();
                switch (menuChoice.Trim().ToUpper())
                {
                    case "A":
                        this.EditContactName(phone);
                        break;

                    case "B":
                        this.EditContactEmail(phone);
                        break;

                    case "C":
                        this.EditContactNotes(phone);
                        break;

                    case "D":
                        isRunning = false;
                        this._view.ShowMessage(ConsoleMessages.EditCompletedMessage);
                        break;

                    default:
                        this._view.ShowMessage(ConsoleMessages.InvalidOptionMessage);
                        break;
                }
            }

            this._view.ClearScreenWithKey();
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
                contactName = this._view.ReadName();
                if (this._helper.IsValidName(contactName))
                {
                    isNameValid = true;
                    continue;
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
                contactEmail = this._view.ReadEmail();
                if (this._helper.IsValidEmail(contactEmail))
                {
                    isEmailValid = true;
                    continue;
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
        private bool GetContactPhoneNumber(out string contactPhoneNumber)
        {
            contactPhoneNumber = string.Empty;
            bool isPhoneValid = false;
            while (!isPhoneValid)
            {
                contactPhoneNumber = this._view.ReadPhone("add");
                if (!this._helper.IsValidPhone(contactPhoneNumber))
                {
                    if (!this.CanRetry(ConsoleMessages.InvalidPhoneMessage))
                    {
                        return false;
                    }

                    continue;
                }

                if (this._contactService.IsPhoneRegistered(contactPhoneNumber))
                {
                    if (!this.CanRetry(ConsoleMessages.PhoneExistsMessage))
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
        /// <param name="phoneNo">The contact phone number to be validated.</param>
        /// <param name="operation">The operation to be done with the phone number (edit, delete, search).</param>
        /// <returns>True if the phone number is valid, else false.</returns>
        private bool GetRegisteredContactPhoneNumber(out string phoneNo, string operation)
        {
            phoneNo = string.Empty;
            bool isPhoneValid = false;
            while (!isPhoneValid)
            {
                phoneNo = this._view.ReadPhone(operation);
                if (!this._helper.IsValidPhone(phoneNo))
                {
                    if (!this.CanRetry(ConsoleMessages.InvalidPhoneMessage))
                    {
                        return false;
                    }

                    continue;
                }

                if (!this._contactService.IsPhoneRegistered(phoneNo))
                {
                    if (!this.CanRetry(ConsoleMessages.ContactNotFoundMessage))
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

            bool isUpdated = this._contactService.EditName(phoneNo, name);
            string statusMessage = isUpdated
                       ? ConsoleMessages.NameUpdatedMessage
                       : ConsoleMessages.ContactNotFoundMessage;
            this._view.ShowMessage(statusMessage);
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

            bool isUpdated = this._contactService.EditEmail(phoneNo, email);
            string statusMessage = isUpdated
                       ? ConsoleMessages.EmailUpdatedMessage
                       : ConsoleMessages.ContactNotFoundMessage;
            this._view.ShowMessage(statusMessage);
        }

        /// <summary>
        /// Used to edit the contact notes for a given phone number.
        /// </summary>
        /// <param name="phoneNo">The phone number of the contact to be edited.</param>
        private void EditContactNotes(string phoneNo)
        {
            string? notes = this._view.ReadNotes();
            bool isUpdated = this._contactService.EditNotes(phoneNo, notes);
            string statusMessage = isUpdated
                       ? ConsoleMessages.NotesUpdatedMessage
                       : ConsoleMessages.ContactNotFoundMessage;
            this._view.ShowMessage(statusMessage);
        }

        /// <summary>
        /// Displays an invalid input message for the specified field and prompts the user to decide whether to retry the operation.
        /// </summary>
        /// <param name="message">The message to be printed for invalid input.</param>
        /// <returns>True if user chooses to retry, else false.</returns>
        private bool CanRetry(string message)
        {
            this._view.ShowMessage(message);
            bool shouldRetry = this._view.AskRetry();
            if (!shouldRetry)
            {
                this._view.ClearScreen();
            }

            return shouldRetry;
        }
    }
}