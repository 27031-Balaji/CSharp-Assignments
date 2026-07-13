namespace Contact_Manager
{
    internal class Program
    {
        private List<List<string>> _contacts = new List<List<string>>();

        public static void AddContact()
        {
            int i;
        }

        public static void DisplayContacts()
        {
            int i;
        }

        public static bool IsContactFound(string name)
        {
            if (name == null)
            {
                return false;
            }

            return true;
        }

        public static void GetAndPrintContact(string name)
        {
            int i = 0;
        }

        public static void DeleteContact(string name)
        {
            int i = 0;
        }

        public static void RenameContact(string name)
        {
            int i = 0;
        }

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
                Console.WriteLine("[R] - Rename a specific Contact");
                Console.WriteLine("[E] - Exit the Application");
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
                        string? nameToRename = Console.ReadLine();
                        RenameContact(nameToRename);
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