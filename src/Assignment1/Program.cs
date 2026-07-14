using System.Numerics;
using System.Text.RegularExpressions;

namespace Contact_Manager
{
    /// <summary>
    /// This class Program does all the functionalities of the Contact Manager
    /// </summary>
    internal class Program
    {
        private static List<List<string>> contacts = new List<List<string>>();

        /// <summary>
        /// The method IsNumeric is used to find whether the record for the name exists or not.
        /// </summary>
        /// <param name="phone">The phone no to be validated.</param>
        /// <returns>Either True or False based on the validation of phone number.</returns>
        public static bool IsNumeric(string phone)
        {
            bool isNumeric = long.TryParse(phone, out long result);
            return isNumeric;
        }

        /// <summary>
        /// The method ValidateEmail is used to find whether the record for the name exists or not.
        /// </summary>
        /// <param name="email">The email to be validated.</param>
        /// <returns>Either True or False based on the validation of email.</returns>
        public static bool ValidateEmail(string email)
        {
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// The method ValidateEmail is used to find whether the record for the name exists or not.
        /// </summary>
        /// <returns>Either True or False based on the emptiness of the contact list.</returns>
        public static bool ContactIsEmpty()
        {
            return contacts.Count == 0;
        }

        /// <summary>
        /// The method ValidatePhone is used to find whether the record for the name exists or not.
        /// </summary>
        /// <param name="phone">The email to be validated.</param>
        /// <returns>Either True or False based on the validation of email.</returns>
        public static bool ValidatePhone(string phone)
        {
            if (phone.Length != 10)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The method AddContact adds a list of name, email, phone number and some notes on another list.
        /// </summary>
        public static void AddContact()
        {
            Console.WriteLine("Enter your name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ensure you entered the name properly.\n");
                return;
            }

            Console.WriteLine("Enter your email: ");
            string? email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Ensure you entered the email properly.\n");
                return;
            }

            if (!ValidateEmail(email))
            {
                Console.WriteLine("Enter a valid email.\n");
                return;
            }

            Console.WriteLine("Enter your phone number: ");
            string? phone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("Ensure you enter your phone number properly.\n");
                return;
            }

            if (!ValidatePhone(phone))
            {
                Console.WriteLine("Enter a valid phone number.\n");
                return;
            }

            if (!IsNumeric(phone))
            {
                Console.WriteLine("Phone number has characters! Enter it correctly.\n");
                return;
            }

            if (IsContactFound(phone))
            {
                Console.WriteLine("Phone number already exists in the contact list.\n");
                return;
            }

            Console.WriteLine("Enter any additional notes: ");
            string? notes = Console.ReadLine();

            List<string> entry = new List<string> { name, email, phone, notes };
            contacts.Add(entry);
            Console.WriteLine("Contact added successfully!\n");
        }

        /// <summary>
        /// The method DisplayContact displays the list of details of all contacts.
        /// </summary>
        public static void DisplayContacts()
        {
            if (ContactIsEmpty())
            {
                Console.WriteLine("Contacts list is empty");
                return;
            }

            Console.WriteLine("Contact List: \n");
            int i = 1;
            foreach (var contact in contacts)
            {
                Console.WriteLine($"Contact {i}:");
                Console.WriteLine($"Name: {contact[0]}\nEmail: {contact[1]}\nPhone: {contact[2]}\nNotes: {contact[3]}\n\n");
                i++;
            }
        }

        /// <summary>
        /// The method IsContactFound is used to find whether the record for the name exists or not.
        /// </summary>
        /// <param name="phone">The name to be found.</param>
        /// <returns>Either True or False based on the availability of the record.</returns>
        public static bool IsContactFound(string phone)
        {
            List<string>? result = contacts.Find(row => row[2] == phone);
            if (result == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The method GetAndPrintContact displays the list of details of the searched name.
        /// </summary>
        /// <param name="phone">The name to be found.</param>
        public static void GetAndPrintContact(string phone)
        {
            if (!IsContactFound(phone))
            {
                return;
            }

            List<string>? result = contacts.Find(row => row[2] == phone);
            Console.WriteLine("Contact found.");
            Console.WriteLine($"Name: {result[0]}\nEmail: {result[1]}\nPhone: {result[2]}\nNotes: {result[3]}\n\n");
        }

        /// <summary>
        /// The method DeleteContact deletes a list of contacts with the following name.
        /// </summary>
        /// <param name="phone">The name to be deleted.</param>
        public static void DeleteContact(string phone)
        {
            if (!IsContactFound(phone))
            {
                Console.WriteLine("There is no record with the following phone number! Try again.\n");
                return;
            }
            else
            {
                try
                {
                    List<string>? result = contacts.Find(row => row[2] == phone);
                    if (result == null)
                    {
                        throw new Exception("Contact cannot be found\n");
                    }

                    GetAndPrintContact(phone);
                    contacts.Remove(result);

                    Console.WriteLine("Contact Deleted Successfully\n");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// The method EditContact renames the name for the given old name.
        /// </summary>
        /// <param name="phone">The phone no of the contact to be edited.</param>
        public static void EditContact(string phone)
        {
            if (!IsContactFound(phone))
            {
                Console.WriteLine("There is no record with the following phone number! Try again.\n");
                return;
            }
            else
            {
                List<string>? result = contacts.Find(row => row[2] == phone);
                bool shallExit = false;
                while (!shallExit)
                {
                    Console.WriteLine("Enter which field you want to edit:");
                    Console.WriteLine("[A] - Name");
                    Console.WriteLine("[B] - Email");
                    Console.WriteLine("[C] - Notes");
                    string? option = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(option))
                    {
                        Console.WriteLine("Enter the option properly!\n");
                        break;
                    }

                    switch (option.ToUpper())
                    {
                        case "A":
                            Console.WriteLine("Enter the name to be changed: ");
                            string? name = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(name))
                            {
                                Console.WriteLine("Enter the name properly!\n");
                                break;
                            }

                            result[0] = name;
                            Console.WriteLine("Name changed successfully\n");
                            shallExit = true;
                            break;

                        case "B":
                            Console.WriteLine("Enter the email to be changed: ");
                            string? email = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(email))
                            {
                                Console.WriteLine("Enter the name properly!\n");
                                break;
                            }

                            if (!ValidateEmail(email))
                            {
                                Console.WriteLine("Enter a valid email.\n");
                                break;
                            }

                            result[1] = email;
                            Console.WriteLine("Email changed successfully\n");
                            shallExit = true;
                            break;

                        case "C":
                            Console.WriteLine("Enter the notes to be changed: ");
                            string? notes = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(notes))
                            {
                                Console.WriteLine("Enter the notes properly!\n");
                                break;
                            }

                            result[3] = notes;
                            Console.WriteLine("Notes changed successfully\n");
                            shallExit = true;
                            break;

                        default:
                            Console.WriteLine("Enter a valid option!\n");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// This method SortContact() is the method used to sort the contacts based on their names.
        /// </summary>
        public static void SortContact()
        {
            contacts.Sort((a, b) => a[0].CompareTo(b[0]));
            Console.WriteLine("Contact List Sorted");
            DisplayContacts();
        }

        /// <summary>
        /// This method ConsoleOperation() is the method used to do the console operations.
        /// </summary>
        public static void ConsoleOperation()
        {
            string? userInput;
            Console.WriteLine("Welcome to Contact Manager");
            Console.WriteLine("===================================================================");
            bool shallExit = false;
            while (!shallExit)
            {
                Console.WriteLine("Enter inputs for the following tasks: ");
                Console.WriteLine("[A] - To Add a New Contact");
                Console.WriteLine("[B] - To Display Names of all Contacts");
                Console.WriteLine("[C] - To Search for a Specific Contact");
                Console.WriteLine("[D] - Delete a specific Contact");
                Console.WriteLine("[E] - Edit a Specific Contact");
                Console.WriteLine("[F] - Sort the Contact List");
                Console.WriteLine("[G] - Exit the Application\n");
                userInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Enter a valid option.\n");
                    continue;
                }

                userInput = userInput.ToUpper();

                switch (userInput)
                {
                    case "A":
                        AddContact();
                        break;

                    case "B":
                        DisplayContacts();
                        break;

                    case "C":
                        if (ContactIsEmpty())
                        {
                            Console.WriteLine("Contact list is empty\n");
                            break;
                        }

                        Console.WriteLine("Enter your phone number of the contact to search: ");
                        string? phoneToSearch = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(phoneToSearch))
                        {
                            Console.WriteLine("Enter a valid phone no!");
                            break;
                        }

                        bool isAllowed = IsNumeric(phoneToSearch);
                        if (isAllowed)
                        {
                            GetAndPrintContact(phoneToSearch);
                        }
                        else
                        {
                            Console.WriteLine("Enter the right phone number instead of characters!\n");
                        }

                        break;

                    case "D":
                        if (ContactIsEmpty())
                        {
                            Console.WriteLine("The contact list is empty\n");
                            break;
                        }

                        Console.WriteLine("Enter phone number of the contact to remove: ");
                        string? phoneToDelete = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(phoneToDelete) == true)
                        {
                            Console.WriteLine("Ensure you entered the phone number properly without whitespaces.\n");
                        }

                        bool isNumeric = IsNumeric(phoneToDelete);
                        if (isNumeric)
                        {
                            DeleteContact(phoneToDelete);
                        }
                        else
                        {
                            Console.WriteLine("Enter the right phone number instead of characters!\n");
                        }

                        break;

                    case "E":
                        if (ContactIsEmpty())
                        {
                            Console.WriteLine("The contact list is empty\n");
                            break;
                        }

                        Console.WriteLine("Enter phone number of the contact to edit: ");
                        string? phoneNoToEdit = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(phoneNoToEdit) == true)
                        {
                            Console.WriteLine("Enter the phone number properly without whitespaces.\n");
                            break;
                        }

                        bool allowed = IsNumeric(phoneNoToEdit);
                        if (allowed)
                        {
                            EditContact(phoneNoToEdit);
                        }
                        else
                        {
                            Console.WriteLine("Enter the right phone number instead of characters!");
                        }

                        break;

                    case "F":
                        if (ContactIsEmpty())
                        {
                            Console.WriteLine("The contact list is empty\n");
                            break;
                        }

                        SortContact();
                        break;

                    case "G":
                        Console.WriteLine("Exiting the application...");
                        shallExit = true;
                        break;

                    default:
                        Console.WriteLine("Give the right option.\n");
                        break;
                }
            }
        }

        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            ConsoleOperation();
            Console.ReadKey();
        }
    }
}