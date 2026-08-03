using Task2.Models;

namespace Task2
{
    /// <summary>
    /// The program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main method is the entry method that runs in the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Bonus Calculator.");
            Employee employee;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose the designation:");
                Console.WriteLine("[A] Developer");
                Console.WriteLine("[B] Manager");
                Console.Write("Enter your choice: ");

                char menuChoice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                if (menuChoice != 'A' && menuChoice != 'B')
                {
                    Console.WriteLine("Enter a valid choice.");
                    continue;
                }

                string name;

                while (true)
                {
                    Console.Write("Enter Employee Name: ");
                    name = Console.ReadLine() ?? string.Empty;

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        break;
                    }

                    Console.WriteLine("Invalid name. Please try again.");
                }

                decimal salary;

                while (true)
                {
                    Console.Write("Enter Employee Salary: ");

                    if (decimal.TryParse(Console.ReadLine(), out salary) && salary > 0)
                    {
                        break;
                    }

                    Console.WriteLine("Invalid salary. Please enter a positive number.");
                }

                if (menuChoice == 'A')
                {
                    employee = new Developer(name, salary);
                }
                else
                {
                    employee = new Manager(name, salary);
                }

                break;
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose an Operation:");
                Console.WriteLine("[A] Calculate Bonus");
                Console.WriteLine("[B] Print Details");
                Console.WriteLine("[C] Exit");
                Console.Write("Enter your choice: ");

                char operationChoice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                switch (operationChoice)
                {
                    case 'A':
                        Console.WriteLine($"Bonus: {employee.CalculateBonus():F2}");
                        break;

                    case 'B':
                        Console.WriteLine(employee.PrintDetails());
                        break;

                    case 'C':
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}