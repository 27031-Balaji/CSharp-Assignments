using Assignment1.View;

namespace Contact_Manager
{
    /// <summary>
    /// This class Program does all the functionalities of the Contact Manager.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            ConsoleOperation console = new ConsoleOperation();
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
                if (string.IsNullOrWhiteSpace(option))
                {
                    Console.WriteLine("Please enter a valid option.\n");
                    Pause();
                    continue;
                }

                switch (option.ToUpper())
                {
                    case "A":
                        console.AddContact();
                        Pause();
                        break;

                    case "B":
                        console.DisplayContacts();
                        Pause();
                        break;

                    case "C":
                        console.SearchContact();
                        Pause();
                        break;

                    case "D":
                        console.DeleteContact();
                        Pause();
                        break;

                    case "E":
                        console.EditContact();
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

            Console.ReadKey();
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