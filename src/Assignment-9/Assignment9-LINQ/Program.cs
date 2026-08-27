using Assignment9.Data;
using Assignment9.Tasks;

namespace Assignment9
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starts the LINQ operation according to the different tasks given by the user.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main()
        {
            SampleDatabaseContext context = SampleDataLoader.Load();
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("=================================");
                Console.WriteLine("        LINQ ASSIGNMENT");
                Console.WriteLine("=================================");
                Console.WriteLine();
                Console.WriteLine("1. Task 1 - Basic LINQ Queries");
                Console.WriteLine("2. Task 2 - Complex LINQ Queries");
                Console.WriteLine("3. Task 3 - LINQ to Objects");
                Console.WriteLine("4. Task 4 - Performance Analysis");
                Console.WriteLine("5. Task 5 - Fluent API Query Builder");
                Console.WriteLine("6. Exit");
                Console.WriteLine();
                Console.Write("Choose a task: ");

                string? choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        Task1.Run(context);
                        ClearScreenWithKey();
                        break;

                    case "2":
                        Task2.Run(context);
                        ClearScreenWithKey();
                        break;

                    case "3":
                        ClearScreenWithKey();
                        break;

                    case "4":
                        ClearScreenWithKey();
                        break;

                    case "5":
                        ClearScreenWithKey();
                        break;

                    case "6":
                        isRunning = false;
                        Console.WriteLine("Exiting application...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        ClearScreenWithKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Waits for a keypress from the user and clears the console.
        /// </summary>
        private static void ClearScreenWithKey()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}