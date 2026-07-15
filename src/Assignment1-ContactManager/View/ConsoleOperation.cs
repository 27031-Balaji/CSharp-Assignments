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
            if (_contactService.IsNullName(name))
            {
                Console.WriteLine("Ensure you entered the name properly.");
                return;
            }

            Console.Write("Enter your email: ");
            string? email = Console.ReadLine();
            if (_contactService.IsNullEmail(email))
            {
                Console.WriteLine("Ensure you entered the email properly.");
                return;
            }

            if (!_contactService.ValidateEmail(email))
            {
                Console.WriteLine("Enter a valid email.");
                return;
            }

            Console.Write("Enter your phone number: ");
            string? phone = Console.ReadLine();
            if (_contactService.IsNullPhone(phone))
            {
                Console.WriteLine("Ensure you entered the phone number properly.");
                return;
            }

            if (!_contactService.ValidatePhone(phone))
            {
                Console.WriteLine("The phone number should only be 10 digits and no characters.");
                return;
            }

            if (_contactService.IsContactExists(phone))
            {
                Console.WriteLine("Phone number already exists. Try again with a different phone number.");
                return;
            }

            Console.Write("Enter notes (Optional): ");
            string? notes = Console.ReadLine();
            Console.WriteLine(_contactService.AddContact(name, email, phone, notes));
        }

        /// <summary>
        /// Displays all contacts.
        /// </summary>
        public void DisplayContacts()
        {
            Console.WriteLine(_contactService.GetAllContacts());
        }

        /// <summary>
        /// Searches a contact.
        /// </summary>
        public void SearchContact()
        {
            if (_contactService.IsContactListEmpty())
            {
                Console.WriteLine("Contact list is empty.");
                return;
            }

            Console.Write("Enter Name, Email or Phone Number: ");
            string? value = Console.ReadLine();
            if (_contactService.IsNullOption(value))
            {
                Console.WriteLine("Enter the search value properly.");
                return;
            }

            Console.WriteLine(_contactService.SearchContact(value));
        }

        /// <summary>
        /// Deletes a contact.
        /// </summary>
        public void DeleteContact()
        {
            if (_contactService.IsContactListEmpty())
            {
                Console.WriteLine("Contact list is empty.");
                return;
            }

            Console.Write("Enter Name, Email or Phone Number: ");
            string? value = Console.ReadLine();
            if (_contactService.IsNullOption(value))
            {
                Console.WriteLine("Enter the search value properly.");
                return;
            }

            Console.WriteLine(_contactService.DeleteContact(value));
        }

        /// <summary>
        /// Edits a contact with phone number as the input.
        /// </summary>
        public void EditContact()
        {
            if (_contactService.IsContactListEmpty())
            {
                Console.WriteLine("Contact list is empty.");
                return;
            }

            Console.Write("Enter the name or email or phone number of the contact to edit: ");
            string? value = Console.ReadLine();
            if (_contactService.IsNullPhone(value))
            {
                Console.WriteLine("Enter the value properly.");
                return;
            }

            if (!_contactService.IsContactExists(value))
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
                if (_contactService.IsNullOption(option))
                {
                    Console.WriteLine("Enter a valid option.\n");
                    continue;
                }

                switch (option.ToUpper())
                {
                    case "A":
                        Console.Write("Enter the new name: ");
                        string? name = Console.ReadLine();
                        if (_contactService.IsNullName(name))
                        {
                            Console.WriteLine("Enter the name properly.");
                            break;
                        }

                        Console.WriteLine(_contactService.EditName(value, name));
                        break;

                    case "B":
                        Console.Write("Enter the new email: ");
                        string? email = Console.ReadLine();
                        if (_contactService.IsNullEmail(email))
                        {
                            Console.WriteLine("Enter the email properly.");
                            break;
                        }

                        if (!_contactService.ValidateEmail(email))
                        {
                            Console.WriteLine("Enter a valid email.");
                            break;
                        }

                        Console.WriteLine(_contactService.EditEmail(value, email));
                        break;

                    case "C":
                        Console.Write("Enter the new notes: ");
                        string? notes = Console.ReadLine();
                        Console.WriteLine(_contactService.EditNotes(value, notes));
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