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
        private ContactHelper _helper = new ContactHelper();

        /// <summary>
        /// Displays the main menu and performs the selected operation.
        /// </summary>
        public void Run()
        {
            bool shallExit = false;

            while (!shallExit)
            {
                Console.WriteLine("Welcome to Contact Manager");
                Console.WriteLine("========================================================");
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("[A] Add Contact");
                Console.WriteLine("[B] Display Contacts");
                Console.WriteLine("[C] Search Contact");
                Console.WriteLine("[D] Delete Contact");
                Console.WriteLine("[E] Edit Contact");
                Console.WriteLine("[F] Exit");
                Console.Write("\nEnter your choice: ");
                string? option = Console.ReadLine();
                if (this._helper.IsNullString(option))
                {
                    Console.WriteLine("Please enter a valid option.\n");
                    Pause();
                    continue;
                }

                switch (option!.ToUpper())
                {
                    case "A":
                        this.AddContact();
                        Pause();
                        break;

                    case "B":
                        this.DisplayContacts();
                        Pause();
                        break;

                    case "C":
                        this.SearchContact();
                        Pause();
                        break;

                    case "D":
                        this.DeleteContact();
                        Pause();
                        break;

                    case "E":
                        this.EditContact();
                        Pause();
                        break;

                    case "F":
                        Console.WriteLine("Exiting Application...");
                        shallExit = true;
                        break;

                    default:
                        Console.WriteLine("Enter a valid option.");
                        Pause();
                        break;
                }
            }
        }

        /// <summary>
        /// Gets user input and adds a contact.
        /// </summary>
        public void AddContact()
        {
            Console.Write("Enter your name: ");
            string? name = Console.ReadLine();
            if (this._helper.IsNullString(name))
            {
                Console.WriteLine("Ensure you entered the name properly.");
                return;
            }

            Console.Write("Enter your email: ");
            string? email = Console.ReadLine();
            if (this._helper.IsNullString(email))
            {
                Console.WriteLine("Ensure you entered the email properly.");
                return;
            }

            if (!this._helper.ValidateEmail(email!))
            {
                Console.WriteLine("Enter a valid email.");
                return;
            }

            Console.Write("Enter your phone number: ");
            string? phone = Console.ReadLine();
            if (this._helper.IsNullString(phone))
            {
                Console.WriteLine("Ensure you entered the phone number properly.");
                return;
            }

            if (!this._helper.ValidatePhone(phone!))
            {
                Console.WriteLine("Phone number should contain exactly 10 digits and no characters.");
                return;
            }

            if (this._contactService.IsContactExists(phone!))
            {
                Console.WriteLine("Phone number already exists. Try again with a different phone number.");
                return;
            }

            Console.Write("Enter notes (Optional): ");
            string? notes = Console.ReadLine();
            Console.WriteLine(this._contactService.AddContact(name!.Trim(), email!, phone!, notes));
        }

        /// <summary>
        /// Displays all contacts.
        /// </summary>
        public void DisplayContacts()
        {
            List<ContactInfo> contacts = this._contactService.GetAllContacts();
            if (this._helper.IsContactListEmpty(contacts))
            {
                Console.WriteLine("The contact list is empty.\n");
                return;
            }

            int i = 1;
            Console.WriteLine("Contact List\n");
            foreach (ContactInfo contact in contacts)
            {
                Console.WriteLine($"Contact {i}: ");
                Console.WriteLine($"Name: {contact.Name}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine($"Phone No.: {contact.Phone}");
                Console.WriteLine($"Notes: {contact.Notes}\n");
                i++;
            }
        }

        /// <summary>
        /// Searches a contact.
        /// </summary>
        public void SearchContact()
        {
            List<ContactInfo> contacts = this._contactService.GetAllContacts();
            if (this._helper.IsContactListEmpty(contacts))
            {
                Console.WriteLine("Contact list is empty.");
                return;
            }

            Console.Write("Enter the phone number: ");
            string? phone = Console.ReadLine();
            if (this._helper.IsNullString(phone))
            {
                Console.WriteLine("Enter the phone number properly.");
                return;
            }

            if (!this._helper.ValidatePhone(phone!))
            {
                Console.WriteLine("Phone number should contain exactly 10 digits and no characters.");
                return;
            }

            ContactInfo? contact = this._contactService.SearchContact(phone!);
            if (contact == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            Console.WriteLine("\nContact Found!\n");
            Console.WriteLine($"Name : {contact.Name}");
            Console.WriteLine($"Email : {contact.Email}");
            Console.WriteLine($"Phone Number : {contact.Phone}");
            Console.WriteLine($"Notes : {contact.Notes}");
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
            if (this._helper.IsNullString(phone))
            {
                Console.WriteLine("Enter the phone number properly.");
                return;
            }

            if (!this._helper.ValidatePhone(phone!))
            {
                Console.WriteLine("Phone number should contain exactly 10 digits and no characters.");
                return;
            }

            Console.WriteLine(this._contactService.DeleteContact(phone!));
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
            if (this._helper.IsNullString(phone))
            {
                Console.WriteLine("Enter the phone number properly.");
                return;
            }

            if (!this._helper.ValidatePhone(phone!))
            {
                Console.WriteLine("Phone number should contain exactly 10 digits and no characters.");
                return;
            }

            if (!this._contactService.IsContactExists(phone!))
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

                if (this._helper.IsNullString(option))
                {
                    Console.WriteLine("Enter a valid option.\n");
                    continue;
                }

                switch (option!.ToUpper())
                {
                    case "A":
                        Console.Write("Enter the new name: ");
                        string? name = Console.ReadLine();

                        if (this._helper.IsNullString(name))
                        {
                            Console.WriteLine("Enter the name properly.");
                            break;
                        }

                        Console.WriteLine(this._contactService.EditName(phone!, name!));
                        break;

                    case "B":
                        Console.Write("Enter the new email: ");
                        string? email = Console.ReadLine();

                        if (this._helper.IsNullString(email))
                        {
                            Console.WriteLine("Enter the email properly.");
                            break;
                        }

                        if (!this._helper.ValidateEmail(email!))
                        {
                            Console.WriteLine("Enter a valid email.");
                            break;
                        }

                        Console.WriteLine(this._contactService.EditEmail(phone!, email!));
                        break;

                    case "C":
                        Console.Write("Enter the new notes: ");
                        string? notes = Console.ReadLine();
                        Console.WriteLine(this._contactService.EditNotes(phone!, notes));
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

        /// <summary>
        /// This method Pause is used to clear the console for a better user experience.
        /// </summary>
        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}