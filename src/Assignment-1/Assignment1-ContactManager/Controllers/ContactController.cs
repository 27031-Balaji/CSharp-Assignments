using Assignment1.Helpers;
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
        // Validation Messages
        private const string InvalidNameMessage = "Enter a valid name.";
        private const string InvalidEmailMessage = "Enter a valid email.";
        private const string InvalidPhoneMessage = "Enter a valid phone number.";
        private const string InvalidOptionMessage = "Enter a valid option.";

        // Contact Messages
        private const string ContactListEmptyMessage = "Contact list is empty.";
        private const string ContactNotFoundMessage = "Contact not found.";
        private const string PhoneExistsMessage = "Phone number already exists.";
        private const string ContactAddedMessage = "Contact added successfully.";
        private const string ContactDeletedMessage = "Contact deleted successfully.";
        private const string DeleteFailedMessage = "Failed to delete contact.";

        // Edit Messages
        private const string NameUpdatedMessage = "Name updated successfully.";
        private const string EmailUpdatedMessage = "Email updated successfully.";
        private const string NotesUpdatedMessage = "Notes updated successfully.";
        private const string EditCompletedMessage = "Edit function completed.";

        // Application Messages
        private const string ExitMessage = "Exiting Application...";

        private readonly ContactServices _contactService = new ContactServices();
        private readonly ContactHelper _helper = new ContactHelper();
        private readonly ConsoleOperation _view = new ConsoleOperation();

        /// <summary>
        /// Starts and runs the main controller loop.
        /// </summary>
        public void Run()
        {
            bool isRunning = true;
            do
            {
                string? option = this._view.ShowMainMenu();
                switch (option!.Trim().ToUpper())
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
                        this._view.ShowMessage(ExitMessage);
                        isRunning = false;
                        break;

                    default:
                        this._view.ShowMessage(InvalidOptionMessage);
                        break;
                }
            }
            while (isRunning);
        }

        /// <summary>
        /// This method checks if there are any contacts in the contact list.
        /// </summary>
        /// <returns>True if the list is empty, otherwise false.</returns>
        private bool HasContacts()
        {
            if (this._contactService.IsContactEmpty())
            {
                this._view.ShowMessage(ContactListEmptyMessage);
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
            do
            {
                name = this._view.ReadName();
                if (!this._helper.IsValidName(name))
                {
                    this._view.ShowMessage(InvalidNameMessage);
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

            string email;
            do
            {
                email = this._view.ReadEmail();
                if (!this._helper.IsValidEmail(email))
                {
                    this._view.ShowMessage(InvalidEmailMessage);
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

            string phone;
            do
            {
                phone = this._view.ReadPhone();
                if (!this._helper.IsValidPhone(phone))
                {
                    this._view.ShowMessage(InvalidPhoneMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                if (this._contactService.IsContactPhoneNumberExists(phone))
                {
                    this._view.ShowMessage(PhoneExistsMessage);
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

            string? notes = this._view.ReadNotes();
            this._contactService.AddContact(name.Trim(), email, phone, notes);
            this._view.ShowMessage(ContactAddedMessage);
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
            do
            {
                phone = this._view.ReadPhone();
                if (!this._helper.IsValidPhone(phone))
                {
                    this._view.ShowMessage(InvalidPhoneMessage);
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
                    this._view.ShowMessage(ContactNotFoundMessage);
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
            while (true);

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
            do
            {
                phone = this._view.ReadPhone();
                if (!this._helper.IsValidPhone(phone))
                {
                    this._view.ShowMessage(InvalidPhoneMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                if (!this._contactService.IsContactPhoneNumberExists(phone))
                {
                    this._view.ShowMessage(ContactNotFoundMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                this._view.ShowMessage(this._contactService.DeleteContact(phone) ? ContactDeletedMessage : DeleteFailedMessage);
                break;
            }
            while (true);

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
            do
            {
                phone = this._view.ReadEditPhone();
                if (!this._helper.IsValidPhone(phone))
                {
                    this._view.ShowMessage(InvalidPhoneMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return;
                    }

                    continue;
                }

                if (!this._contactService.IsContactPhoneNumberExists(phone))
                {
                    this._view.ShowMessage(ContactNotFoundMessage);
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

            bool isRunning = true;
            do
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
                                this._view.ShowMessage(InvalidNameMessage);
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

                        this._view.ShowMessage(this._contactService.EditName(phone, name) ? NameUpdatedMessage : ContactNotFoundMessage);
                        break;

                    case "B":
                        string email;
                        do
                        {
                            email = this._view.ReadEmail();
                            if (!this._helper.IsValidEmail(email))
                            {
                                this._view.ShowMessage(InvalidEmailMessage);
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

                        this._view.ShowMessage(this._contactService.EditName(phone, email) ? EmailUpdatedMessage : ContactNotFoundMessage);
                        break;

                    case "C":
                        string? notes = this._view.ReadNotes();
                        this._view.ShowMessage(this._contactService.EditName(phone, notes) ? NotesUpdatedMessage : ContactNotFoundMessage);
                        break;

                    case "D":
                        isRunning = false;
                        this._view.ShowMessage(EditCompletedMessage);
                        break;

                    default:
                        this._view.ShowMessage(InvalidOptionMessage);
                        break;
                }
            }
            while (isRunning);
            this._view.FlushScreenWithKey();
        }
    }
}