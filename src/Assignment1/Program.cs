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

            Console.WriteLine("Enter any additional notes: ");
            string? notes = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(notes))
            {
                Console.WriteLine("Ensure you enter the notes properly.\n");
                return;
            }

            List<string> entry = new List<string> { name, email, phone, notes };
            contacts.Add(entry);
            Console.WriteLine("Contact added succesfully!\n");
        }

        /// <summary>
        /// The method DisplayContact displays the list of details of all contacts.
        /// </summary>
        public static void DisplayContacts()
        {
            int i;
        }

        /// <summary>
        /// The method IsContactFound is used to find whether the record for the name exists or not.
        /// </summary>
        /// <param name="name">The name to be found.</param>
        /// <returns>Either True or False based on the availability of the record.</returns>
        public static bool IsContactFound(string name)
        {
            if (name == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The method GetAndPrintContact displays the list of details of the searched name.
        /// </summary>
        /// <param name="name">The name to be found.</param>
        public static void GetAndPrintContact(string name)
        {
            int i = 0;
        }

        /// <summary>
        /// The method DeleteContact deletes a list of contacts with the following name.
        /// </summary>
        /// <param name="name">The name to be deleted.</param>
        public static void DeleteContact(string name)
        {
            int i = 0;
        }

        /// <summary>
        /// The method EditContact renames the name for the given old name.
        /// </summary>
        /// <param name="phone">The phone no of the contact to be edited.</param>
        public static void EditContact(string phone)
        {
            List<string>? result = contacts.Find(row => row[2] == phone);
            if (result == null)
            {
                Console.WriteLine("There is no record with the following phone number! Try again.\n");
                return;
            }
            else
            {
                Console.WriteLine("Enter which field you want to edit:");
                Console.WriteLine("[A] - Name");
                Console.WriteLine("[B] - Email");
                Console.WriteLine("[C] - Notes");
                bool shallExit = false;
                while (!shallExit)
                {
                    string? option = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(option))
                    {
                        Console.WriteLine("Enter the option properly!");
                        break;
                    }
                    switch (option)
                    {
                        case "A":
                        case "a":
                            Console.WriteLine("Enter the name to be changed: ");
                            string? name = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(name))
                            {
                                Console.WriteLine("Enter the name properly!");
                                break;
                            }

                            result[0] = name;
                            Console.WriteLine("Name changed successfully");
                            shallExit = true;
                            break;

                        case "B":
                        case "b":
                            Console.WriteLine("Enter the email to be changed: ");
                            string? email = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(email))
                            {
                                Console.WriteLine("Enter the name properly!");
                                break;
                            }

                            if (!ValidateEmail(email))
                            {
                                Console.WriteLine("Enter a valid email.\n");
                                break;
                            }

                            result[1] = email;
                            Console.WriteLine("Email changed successfully");
                            shallExit = true;
                            break;

                        case "C":
                        case "c":
                            Console.WriteLine("Enter the notes to be changed: ");
                            string? notes = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(notes))
                            {
                                Console.WriteLine("Enter the notes properly!");
                                break;
                            }

                            result[3] = notes;
                            Console.WriteLine("Notes changed successfully");
                            shallExit = true;
                            break;

                        default:
                            Console.WriteLine("Enter a valid option!");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            string? userInput;
            Console.WriteLine("Welcome to Contact Manager");
            Console.WriteLine("===================================================================");
            bool shallExit = false;
            while (!shallExit)
            {
                Console.WriteLine("Enter inputs for the following tasks: ");
                Console.WriteLine("[A] - To Add a New Contact");
                Console.WriteLine("[D] - To Display Names of all Contacts");
                Console.WriteLine("[S] - To Search for a Specific Contact");
                Console.WriteLine("[W] - Delete a specific Contact");
                Console.WriteLine("[R] - Edit a Specific Contact");
                Console.WriteLine("[E] - Exit the Application\n");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "A":
                    case "a":
                        AddContact();
                        break;

                    case "D":
                    case "d":
                        DisplayContacts();
                        break;

                    case "S":
                    case "s":
                        string? nameToSearch = Console.ReadLine();
                        if (IsContactFound(nameToSearch) == true)
                        {
                            GetAndPrintContact(nameToSearch);
                        }
                        else
                        {
                            Console.WriteLine($"There is no record of {nameToSearch}. Add the data before searching");
                        }

                        break;

                    case "W":
                    case "w":
                        string? nameToDelete = Console.ReadLine();
                        DeleteContact(nameToDelete);
                        break;

                    case "R":
                    case "r":
                        Console.WriteLine("Enter phone number of the contact to edit: ");
                        string? phoneNoToEdit = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(phoneNoToEdit) == true)
                        {
                            Console.WriteLine("Enter the phone number properly without whitespaces.\n");
                            break;
                        }

                        bool isNumeric = int.TryParse(phoneNoToEdit, out int result);
                        if (isNumeric)
                        {
                            EditContact(phoneNoToEdit);
                        }
                        else
                        {
                            Console.WriteLine("Enter the right phone number instead of characters!");
                        }
                        break;

                    case "E":
                    case "e":
                        Console.WriteLine("Exiting the application...");
                        shallExit = true;
                        break;
                }
            }

            Console.ReadKey();
        }
    }
}