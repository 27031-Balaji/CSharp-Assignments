using CSharpAdvancedFeatures.Tasks;

namespace CSharpAdvancedFeatures
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("======================================================");
                Console.WriteLine("        CSHARP ADVANCED FEATURES ASSIGNMENT");
                Console.WriteLine("======================================================");
                Console.WriteLine();
                Console.WriteLine("1. Task 1 - Understanding and Implementing Events and Delegates in C#");
                Console.WriteLine("2. Task 2 - Understanding the Use of Dynamic and Var Keywords and Their Differences");
                Console.WriteLine("3. Task 3 - Implementing Anonymous Methods");
                Console.WriteLine("4. Task 4 - Understanding and Using Lambda Expressions and Statements");
                Console.WriteLine("5. Task 5 - Advanced Use of Delegates for Sorting");
                Console.WriteLine("6. Task 6 - Implementing and Manipulating Records in C# 9.0 and Above");
                Console.WriteLine("7. Task 7 - Implementing Advanced Pattern Matching in C# 7.0 and Above");
                Console.WriteLine("8. Exit");
                Console.WriteLine();
                Console.Write("Choose a task: ");

                string? choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        Task1 task1 = new Task1();
                        task1.Run();
                        ClearScreenWithKey();
                        break;

                    case "2":
                        Task2 task2 = new Task2();
                        task2.Run();
                        ClearScreenWithKey();
                        break;

                    case "3":
                        Task3 task3 = new Task3();
                        task3.Run();
                        ClearScreenWithKey();
                        break;

                    case "4":
                        Task4 task4 = new Task4();
                        task4.Run();
                        ClearScreenWithKey();
                        break;

                    case "5":
                        Task5 task5 = new Task5();
                        task5.Run();
                        ClearScreenWithKey();
                        break;

                    case "6":
                        Task6 task6 = new Task6();
                        task6.Run();
                        ClearScreenWithKey();
                        break;

                    case "7":
                        Task7 task7 = new Task7();
                        task7.Run();
                        ClearScreenWithKey();
                        break;

                    case "8":
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