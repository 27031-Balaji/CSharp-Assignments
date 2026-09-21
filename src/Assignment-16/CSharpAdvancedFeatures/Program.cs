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

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 8)
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }

                switch (choice)
                {
                    case 1:
                        new Task1().Run();
                        break;

                    case 2:
                        new Task2().Run();
                        break;

                    case 3:
                        new Task3().Run();
                        break;

                    case 4:
                        new Task4().Run();
                        break;

                    case 5:
                        new Task5().Run();
                        break;

                    case 6:
                        new Task6().Run();
                        break;

                    case 7:
                        new Task7().Run();
                        break;

                    case 8:
                        isRunning = false;
                        break;
                }

                if (isRunning)
                {
                    ClearScreenWithKey();
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