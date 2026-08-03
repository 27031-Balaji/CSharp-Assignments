using ContactManager.Model;

namespace ContactManager.View
{
    /// <summary>
    /// Handles console input/output operations for the application.
    /// </summary>
    internal class ConsoleOperation
    {
        /// <summary>
        /// Displays the main menu and reads the user's choice.
        /// </summary>
        /// <returns>The user's selected option as a string.</returns>
        public string ShowMainMenu()
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

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Reads the contact name from the console.
        /// </summary>
        /// <returns>The entered name.</returns>
        public string ReadName()
        {
            Console.Write("Enter your name: ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Reads the contact email from the console.
        /// </summary>
        /// <returns>The entered email.</returns>
        public string ReadEmail()
        {
            Console.Write("Enter your email: ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Reads the contact phone number from the console.
        /// </summary>
        /// <param name="operation">The operation for which we are asking phone number for.</param>
        /// <returns>The entered phone number.</returns>
        public string ReadPhone(string operation)
        {
            Console.Write($"Enter the phone number to {operation}: ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Reads optional notes for a contact.
        /// </summary>
        /// <returns>The entered notes (may be empty).</returns>
        public string ReadNotes()
        {
            Console.Write("Enter notes (Optional): ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Displays the edit menu and reads the user's choice.
        /// </summary>
        /// <returns>The user's selected edit option.</returns>
        public string ShowEditMenu()
        {
            Console.WriteLine("\n[A] Edit Name");
            Console.WriteLine("[B] Edit Email");
            Console.WriteLine("[C] Edit Notes");
            Console.WriteLine("[D] Exit");
            Console.Write("Choose an option: ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Shows a message to the console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Displays a list of contacts.
        /// </summary>
        /// <param name="contacts">The contacts to display.</param>
        public void DisplayContacts(List<ContactInfo> contacts)
        {
            int count = 1;
            Console.WriteLine("Contact List\n");
            foreach (ContactInfo contact in contacts)
            {
                Console.WriteLine($"Contact {count}:");
                Console.WriteLine($"Name: {contact.Name}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine($"Phone No.: {contact.Phone}");
                Console.WriteLine($"Notes: {contact.Notes}\n");
                count++;
            }
        }

        /// <summary>
        /// Displays a single contact's details.
        /// </summary>
        /// <param name="contact">The contact to display.</param>
        public void DisplayContact(ContactInfo contact)
        {
            Console.WriteLine("\nContact Found!\n");
            Console.WriteLine($"Name : {contact.Name}");
            Console.WriteLine($"Email : {contact.Email}");
            Console.WriteLine($"Phone Number : {contact.Phone}");
            Console.WriteLine($"Notes : {contact.Notes}");
        }

        /// <summary>
        /// Clears the console.
        /// </summary>
        public void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Clears the console with a key press from the user.
        /// </summary>
        public void ClearScreenWithKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Asks the user whether they want to retry and returns the result.
        /// </summary>
        /// <returns>True if the user chooses to retry; otherwise false.</returns>
        public bool AskRetry()
        {
            while (true)
            {
                Console.Write("\nTry again? (Y/N): ");
                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice.Trim().ToUpper())
                {
                    case "Y":
                        return true;

                    case "N":
                        return false;

                    default:
                        Console.WriteLine("Please enter Y or N.");
                        break;
                }
            }
        }
    }
}