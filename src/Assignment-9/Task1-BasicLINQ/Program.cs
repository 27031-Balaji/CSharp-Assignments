using Assignment9.Data;

namespace Assignment9;

public class Program
{
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
                    ClearScreenWithKey();
                    break;

                case "2":
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

    private static void ClearScreenWithKey()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
        Console.Clear();
    }
}