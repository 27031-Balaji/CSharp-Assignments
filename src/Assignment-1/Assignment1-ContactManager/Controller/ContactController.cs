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

        private bool GetContactName(out string name)
        {
            name = string.Empty;
            bool isNameValid = false;
            while (!isNameValid)
            {
                name = this._view.ReadName();
                if (this._helper.IsValidName(name))
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

        private bool GetContactEmail(out string email)
        {
            email = string.Empty;
            bool isEmailValid = false;
            while (!isEmailValid)
            {
                email = this._view.ReadEmail();
                if (this._helper.IsValidEmail(email))
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

        private bool GetContactPhoneNumber(out string phone)
        {
            phone = string.Empty;
            bool isPhoneValid = false;
            while (!isPhoneValid)
            {
                phone = this._view.ReadPhone("add");
                if (!this._helper.IsValidPhone(phone))
                {
                    if (!this.CanRetry(ConsoleMessages.InvalidPhoneMessage))
                    {
                        return false;
                    }

                    continue;
                }

                if (this._contactService.IsPhoneRegistered(phone))
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

        private bool GetRegisteredContactPhoneNumber(out string phone, string operation)
        {
            phone = string.Empty;
            bool isPhoneValid = false;
            while (!isPhoneValid)
            {
                phone = this._view.ReadPhone(operation);
                if (!this._helper.IsValidPhone(phone))
                {
                    if (!this.CanRetry(ConsoleMessages.InvalidPhoneMessage))
                    {
                        return false;
                    }

                    continue;
                }

                if (!this._contactService.IsPhoneRegistered(phone))
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

        private void EditContactName(string phone)
        {
            if (!this.GetContactName(out string name))
            {
                return;
            }

            bool isUpdated = this._contactService.EditName(phone, name);
            string statusMessage = isUpdated
                       ? ConsoleMessages.NameUpdatedMessage
                       : ConsoleMessages.ContactNotFoundMessage;
            this._view.ShowMessage(statusMessage);
        }

        private void EditContactEmail(string phone)
        {
            if (!this.GetContactEmail(out string email))
            {
                return;
            }

            bool isUpdated = this._contactService.EditEmail(phone, email);
            string statusMessage = isUpdated
                       ? ConsoleMessages.EmailUpdatedMessage
                       : ConsoleMessages.ContactNotFoundMessage;
            this._view.ShowMessage(statusMessage);
        }

        private void EditContactNotes(string phone)
        {
            string? notes = this._view.ReadNotes();
            bool isUpdated = this._contactService.EditNotes(phone, notes);
            string statusMessage = isUpdated
                       ? ConsoleMessages.NotesUpdatedMessage
                       : ConsoleMessages.ContactNotFoundMessage;
            this._view.ShowMessage(statusMessage);
        }

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