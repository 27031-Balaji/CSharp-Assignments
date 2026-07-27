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
        private ContactServices _contactService = new ContactServices();
        private ContactHelper _helper = new ContactHelper();
        private ConsoleOperation _view = new ConsoleOperation();

        /// <summary>
        /// Starts and runs the main controller loop.
        /// </summary>
        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                string? option = this._view.ShowMainMenu();
                switch (option!.ToUpper())
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
                        this._view.ShowMessage("Exiting Application...");
                        isRunning = false;
                        break;

                    default:
                        this._view.ShowMessage("Enter a valid option.");
                        break;
                }

                if (isRunning)
                {
                    this._view.FlushScreen();
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
                this._view.ShowMessage("Contact list is empty.");
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
                    this._view.ShowMessage("Enter a valid name.");
                    if (!this._view.AskRetry())
                    {
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
                    this._view.ShowMessage("Enter a valid email.");
                    if (!this._view.AskRetry())
                    {
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
                    this._view.ShowMessage("Enter a valid phone number.");
                    if (!this._view.AskRetry())
                    {
                        return;
                    }

                    continue;
                }

                if (this._contactService.IsContactPhoneNumberExists(phone))
                {
                    this._view.ShowMessage("Phone number already exists.");
                    if (!this._view.AskRetry())
                    {
                        return;
                    }

                    continue;
                }

                break;
            }

            string? notes = this._view.ReadNotes();
            this._contactService.AddContact(name.Trim(), email, phone, notes);
            this._view.ShowMessage("Contact added successfully.");
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
                    this._view.ShowMessage("Enter a valid phone number.");
                    if (!this._view.AskRetry())
                    {
                        return;
                    }

                    continue;
                }

                ContactInfo? contact = this._contactService.SearchContact(phone);
                if (contact == null)
                {
                    this._view.ShowMessage("Contact not found.");
                    if (!this._view.AskRetry())
                    {
                        return;
                    }

                    continue;
                }

                this._view.DisplayContact(contact);
                break;
            }
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
                    this._view.ShowMessage("Enter a valid phone number.");
                    if (!this._view.AskRetry())
                    {
                        return;
                    }

                    continue;
                }

                if (!this._contactService.IsContactPhoneNumberExists(phone))
                {
                    this._view.ShowMessage("Contact not found.");
                    if (!this._view.AskRetry())
                    {
                        return;
                    }

                    continue;
                }

                if (this._contactService.DeleteContact(phone))
                {
                    this._view.ShowMessage("Contact deleted successfully.");
                }
                else
                {
                    this._view.ShowMessage("Failed to delete contact.");
                }

                break;
            }
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
                    this._view.ShowMessage("Enter a valid phone number.");
                    if (!this._view.AskRetry())
                    {
                        return;
                    }

                    continue;
                }

                if (!this._contactService.IsContactPhoneNumberExists(phone))
                {
                    this._view.ShowMessage("Contact not found.");
                    if (!this._view.AskRetry())
                    {
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
                        while (true)
                        {
                            name = this._view.ReadName();
                            if (!this._helper.IsValidName(name))
                            {
                                this._view.ShowMessage("Enter a valid name.");
                                if (!this._view.AskRetry())
                                {
                                    return;
                                }

                                continue;
                            }

                            break;
                        }

                        if (this._contactService.EditName(phone, name))
                        {
                            this._view.ShowMessage("Name updated successfully.");
                        }
                        else
                        {
                            this._view.ShowMessage("Contact not found.");
                        }

                        break;

                    case "B":
                        string email;
                        while (true)
                        {
                            email = this._view.ReadEmail();
                            if (!this._helper.IsValidEmail(email))
                            {
                                this._view.ShowMessage("Enter a valid email.");
                                if (!this._view.AskRetry())
                                {
                                    return;
                                }

                                continue;
                            }

                            break;
                        }

                        if (this._contactService.EditEmail(phone, email))
                        {
                            this._view.ShowMessage("Email updated successfully.");
                        }
                        else
                        {
                            this._view.ShowMessage("Contact not found.");
                        }

                        break;

                    case "C":
                        string? notes = this._view.ReadNotes();
                        if (this._contactService.EditNotes(phone, notes))
                        {
                            this._view.ShowMessage("Notes updated successfully.");
                        }
                        else
                        {
                            this._view.ShowMessage("Contact not found.");
                        }

                        break;

                    case "D":
                        isRunning = false;
                        this._view.ShowMessage("Edit function completed.");
                        break;

                    default:
                        this._view.ShowMessage("Enter a valid option.");
                        break;
                }
            }
        }
    }
}