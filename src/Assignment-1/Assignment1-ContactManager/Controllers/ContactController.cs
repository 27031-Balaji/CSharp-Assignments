using Assignment1.Helpers;
using Assignment1.Messages;
using Assignment1.Models;
using Assignment1.Services;
using Assignment1.View;

namespace Assignment1.Controllers
{
    /// <summary>
    /// Coordinates user interactions and application flow for contact management.
    /// </summary>
    internal class ContactController
    {
        private readonly ContactServices _contactService = new ContactServices();
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
                string? option = this._view.ShowMainMenu();
                switch (option.Trim().ToUpper())
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
                        isRunning = false;
                        break;

                    default:
                        this._view.ShowMessage(ConsoleMessages.InvalidOptionMessage);
                        this._view.FlushScreenWithKey();
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
            if (this._contactService.IsContactEmpty())
            {
                this._view.ShowMessage(ConsoleMessages.ContactListEmptyMessage);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Handles user flow for adding a new contact.
        /// </summary>
        private void AddContact()
        {
            string name;
            while (true)
            {
                name = this._view.ReadName();
                if (!this._helper.IsValidName(name))
                {
                    this._view.ShowMessage(ConsoleMessages.InvalidNameMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                break;
            }

            string email;
            while (true)
            {
                email = this._view.ReadEmail();
                if (!this._helper.IsValidEmail(email))
                {
                    this._view.ShowMessage(ConsoleMessages.InvalidEmailMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                break;
            }

            string phone;
            while (true)
            {
                phone = this._view.ReadPhone();
                if (!this._helper.IsValidPhone(phone))
                {
                    this._view.ShowMessage(ConsoleMessages.InvalidPhoneMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                if (this._contactService.IsContactPhoneNumberExists(phone))
                {
                    this._view.ShowMessage(ConsoleMessages.PhoneExistsMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                break;
            }

            string? notes = this._view.ReadNotes();

            this._contactService.AddContact(name.Trim(), email, phone, notes);
            this._view.ShowMessage(ConsoleMessages.ContactAddedMessage);
            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Displays all contacts if any exist.
        /// </summary>
        private void DisplayContacts()
        {
            if (!this.HasContacts())
            {
                return;
            }

            List<ContactInfo> contacts = this._contactService.GetAllContacts();
            this._view.DisplayContacts(contacts);
            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Handles user flow for searching a contact by phone.
        /// </summary>
        private void SearchContact()
        {
            if (!this.HasContacts())
            {
                return;
            }

            string phone;
            while (true)
            {
                phone = this._view.ReadPhone();
                if (!this._helper.IsValidPhone(phone))
                {
                    this._view.ShowMessage(ConsoleMessages.InvalidPhoneMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                ContactInfo? contact = this._contactService.SearchContact(phone);
                if (contact == null)
                {
                    this._view.ShowMessage(ConsoleMessages.ContactNotFoundMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                this._view.DisplayContact(contact);
                break;
            }

            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Handles user flow for deleting a contact by phone.
        /// </summary>
        private void DeleteContact()
        {
            if (!this.HasContacts())
            {
                return;
            }

            string phone;
            while (true)
            {
                phone = this._view.ReadPhone();
                if (!this._helper.IsValidPhone(phone))
                {
                    this._view.ShowMessage(ConsoleMessages.InvalidPhoneMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                if (!this._contactService.IsContactPhoneNumberExists(phone))
                {
                    this._view.ShowMessage(ConsoleMessages.ContactNotFoundMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                this._view.ShowMessage(this._contactService.DeleteContact(phone) ? ConsoleMessages.ContactDeletedMessage : ConsoleMessages.DeleteFailedMessage);
                break;
            }

            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Handles user flow for editing contact details.
        /// </summary>
        private void EditContact()
        {
            if (!this.HasContacts())
            {
                return;
            }

            string phone;
            while (true)
            {
                phone = this._view.ReadEditPhone();
                if (!this._helper.IsValidPhone(phone))
                {
                    this._view.ShowMessage(ConsoleMessages.InvalidPhoneMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                if (!this._contactService.IsContactPhoneNumberExists(phone))
                {
                    this._view.ShowMessage(ConsoleMessages.ContactNotFoundMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                break;
            }

            bool isRunning = true;
            while (isRunning)
            {
                string? option = this._view.ShowEditMenu();
                switch (option?.Trim().ToUpper())
                {
                    case "A":
                        string name;
                        do
                        {
                            name = this._view.ReadName();
                            if (!this._helper.IsValidName(name))
                            {
                                this._view.ShowMessage(ConsoleMessages.InvalidNameMessage);
                                if (!this._view.AskRetry())
                                {
                                    this._view.FlushScreen();
                                    return;
                                }

                                continue;
                            }

                            break;
                        }
                        while (true);

                        this._view.ShowMessage(this._contactService.EditName(phone, name) ? ConsoleMessages.NameUpdatedMessage : ConsoleMessages.ContactNotFoundMessage);
                        break;

                    case "B":
                        string email;
                        do
                        {
                            email = this._view.ReadEmail();
                            if (!this._helper.IsValidEmail(email))
                            {
                                this._view.ShowMessage(ConsoleMessages.InvalidEmailMessage);
                                if (!this._view.AskRetry())
                                {
                                    this._view.FlushScreen();
                                    return;
                                }

                                continue;
                            }

                            break;
                        }
                        while (true);

                        this._view.ShowMessage(this._contactService.EditName(phone, email) ? ConsoleMessages.EmailUpdatedMessage : ConsoleMessages.ContactNotFoundMessage);
                        break;

                    case "C":
                        string? notes = this._view.ReadNotes();
                        this._view.ShowMessage(this._contactService.EditName(phone, notes) ? ConsoleMessages.NotesUpdatedMessage : ConsoleMessages.ContactNotFoundMessage);
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

            this._view.FlushScreenWithKey();
        }
    }
}