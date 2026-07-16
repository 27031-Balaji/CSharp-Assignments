using Assignment1.Helpers;
using Assignment1.Models;
using Assignment1.Services;

namespace Assignment1.View
{
    /// <summary>
    /// Handles all user interactions for contact operations.
    /// </summary>
    internal class ConsoleOperation
    {
        private ContactServices _contactService = new ContactServices();

        /// <summary>
        /// Gets user input and adds a contact.
        /// </summary>
        public void AddContact()
        {
            Console.Write("Enter your name: ");
            string? name = Console.ReadLine();
            if (this._contactService.IsNullString(name))
            {
                Console.WriteLine("Ensure you entered the name properly.");
                return;
            }

            Console.Write("Enter your email: ");
            string? email = Console.ReadLine();
            if (this._contactService.IsNullString(email))
            {
                Console.WriteLine("Ensure you entered the email properly.");
                return;
            }

            if (!this._contactService.ValidateEmail(email))
            {
                Console.WriteLine("Enter a valid email.");
                return;
            }

            Console.Write("Enter your phone number: ");
            string? phone = Console.ReadLine();
            if (this._contactService.IsNullString(phone))
            {
                Console.WriteLine("Ensure you entered the phone number properly.");
                return;
            }

            if (!this._contactService.ValidatePhoneLength(phone))
            {
                Console.WriteLine("The phone number should only be 10 digits.");
                return;
            }

            if (!this._contactService.ValidatePhoneNoCharacters(phone))
            {
                Console.WriteLine("The phone number should only be numbers and no characters.");
                return;
            }

            if (this._contactService.IsContactExists(phone))
            {
                Console.WriteLine("Phone number already exists. Try again with a different phone number.");
                return;
            }

            Console.Write("Enter notes (Optional): ");
            string? notes = Console.ReadLine();
            Console.WriteLine(this._contactService.AddContact(name, email, phone, notes));
        }

        /// <summary>
        /// Displays all contacts.
        /// </summary>
        public void DisplayContacts()
        {
            Console.WriteLine(this._contactService.GetAllContacts());
        }

        /// <summary>
        /// Searches a contact.
        /// </summary>
        public void SearchContact()
        {
            if (this._contactService.IsContactListEmpty())
            {
                Console.WriteLine("Contact list is empty.");
                return;
            }

            Console.Write("Enter the phone number: ");
            string? phone = Console.ReadLine();
            if (this._contactService.IsNullString(phone))
            {
                Console.WriteLine("Enter the phone number properly.");
                return;
            }

            if (!this._contactService.ValidatePhoneLength(phone))
            {
                Console.WriteLine("The phone number should only be 10 digits.");
                return;
            }

            if (!this._contactService.ValidatePhoneNoCharacters(phone))
            {
                Console.WriteLine("The phone number should only be numbers and no characters.");
                return;
            }

            Console.WriteLine(this._contactService.SearchContact(phone));
        }

        /// <summary>
        /// Deletes a contact with the phone number as the input.
        /// </summary>
        public void DeleteContact()
        {
            if (this._contactService.IsContactListEmpty())
            {
                Console.WriteLine("Contact list is empty.");
                return;
            }

            Console.Write("Enter the phone number: ");
            string? phone = Console.ReadLine();
            if (this._contactService.IsNullString(phone))
            {
                Console.WriteLine("Enter the phone number properly.");
                return;
            }

            if (!this._contactService.ValidatePhoneLength(phone))
            {
                Console.WriteLine("The phone number should only be 10 digits.");
                return;
            }

            if (!this._contactService.ValidatePhoneNoCharacters(phone))
            {
                Console.WriteLine("The phone number should only be numbers and no characters.");
                return;
            }

            Console.WriteLine(this._contactService.DeleteContact(phone));
        }

        /// <summary>
        /// Edits a contact with phone number as the input.
        /// </summary>
        public void EditContact()
        {
            if (this._contactService.IsContactListEmpty())
            {
                Console.WriteLine("Contact list is empty.");
                return;
            }

            Console.Write("Enter the phone number of the contact to edit: ");
            string? phone = Console.ReadLine();
            if (this._contactService.IsNullString(phone))
            {
                Console.WriteLine("Enter the phone number properly.");
                return;
            }

            if (!this._contactService.ValidatePhoneLength(phone))
            {
                Console.WriteLine("The phone number should only be 10 digits.");
                return;
            }

            if (!this._contactService.ValidatePhoneNoCharacters(phone))
            {
                Console.WriteLine("The phone number should only be numbers and no characters.");
                return;
            }

            if (!this._contactService.IsContactExists(phone))
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            bool shallExit = false;
            while (!shallExit)
            {
                Console.WriteLine("\n[A] Edit Name");
                Console.WriteLine("[B] Edit Email");
                Console.WriteLine("[C] Edit Notes");
                Console.WriteLine("[D] Exit");
                Console.Write("Choose an option: ");
                string? option = Console.ReadLine();

                if (this._contactService.IsNullString(option))
                {
                    Console.WriteLine("Enter a valid option.\n");
                    continue;
                }

                switch (option.ToUpper())
                {
                    case "A":
                        Console.Write("Enter the new name: ");
                        string? name = Console.ReadLine();

                        if (this._contactService.IsNullString(name))
                        {
                            Console.WriteLine("Enter the name properly.");
                            break;
                        }

                        Console.WriteLine(this._contactService.EditName(phone, name));
                        break;

                    case "B":
                        Console.Write("Enter the new email: ");
                        string? email = Console.ReadLine();

                        if (this._contactService.IsNullString(email))
                        {
                            Console.WriteLine("Enter the email properly.");
                            break;
                        }

                        if (!this._contactService.ValidateEmail(email))
                        {
                            Console.WriteLine("Enter a valid email.");
                            break;
                        }

                        Console.WriteLine(this._contactService.EditEmail(phone, email));
                        break;

                    case "C":
                        Console.Write("Enter the new notes: ");
                        string? notes = Console.ReadLine();
                        Console.WriteLine(this._contactService.EditNotes(phone, notes));
                        break;

                    case "D":
                        shallExit = true;
                        Console.WriteLine("Edit function completed.\n");
                        break;

                    default:
                        Console.WriteLine("Enter a valid option.\n");
                        break;
                }
            }
        }
    }
}