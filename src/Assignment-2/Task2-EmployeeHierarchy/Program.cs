using EmployeeHierarchy.Models;

namespace EmployeeHierarchy
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// It handles user interaction for selecting employees and performing operations.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Starts the Employee Bonus Calculator application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to employee bonus calculator.");
            Employee employee = GetEmployee();
            ShowOperations(employee);
        }

        /// <summary>
        /// Prompts the user to select an <see cref="Employee"/> type and creates the corresponding object.
        /// </summary>
        /// <returns>A <see cref="Developer"/> or <see cref="Manager"/> object.</returns>
        private static Employee GetEmployee()
        {
            bool isEmployeeSelected = false;
            Employee employee = null!;
            while (!isEmployeeSelected)
            {
                Console.WriteLine();
                Console.WriteLine("Choose the designation:");
                Console.WriteLine("[A] Developer");
                Console.WriteLine("[B] Manager");
                Console.Write("Enter your choice: ");

                char employeeChoice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                switch (employeeChoice)
                {
                    case 'A':
                        employee = CreateDeveloper();
                        isEmployeeSelected = true;
                        break;

                    case 'B':
                        employee = CreateManager();
                        isEmployeeSelected = true;
                        break;

                    default:
                        Console.WriteLine("Enter a valid choice.");
                        break;
                }
            }

            return employee;
        }

        /// <summary>
        /// Creates a <see cref="Developer"/> object after collecting validated input.
        /// </summary>
        /// <returns>A <see cref="Developer"/> object.</returns>
        private static Developer CreateDeveloper()
        {
            string name = GetEmployeeName();
            decimal salary = GetSalary();

            return new Developer(name, salary);
        }

        /// <summary>
        /// Creates a <see cref="Manager"/> object after collecting validated input.
        /// </summary>
        /// <returns>A <see cref="Manager"/> object.</returns>
        private static Manager CreateManager()
        {
            string name = GetEmployeeName();
            decimal salary = GetSalary();

            return new Manager(name, salary);
        }

        /// <summary>
        /// Prompts the user until a valid <see cref="Employee"/> name is entered.
        /// </summary>
        /// <returns>A validated <see cref="Employee"/> name.</returns>
        private static string GetEmployeeName()
        {
            bool isValidName = false;
            string name = string.Empty;
            while (!isValidName)
            {
                Console.Write("Enter employee name: ");
                name = (Console.ReadLine() ?? string.Empty).Trim();
                isValidName = !string.IsNullOrWhiteSpace(name);
                if (!isValidName)
                {
                    Console.WriteLine("Invalid name. Please enter a non-empty name.");
                }
            }

            return name;
        }

        /// <summary>
        /// Prompts the user until a valid positive salary is entered.
        /// </summary>
        /// <returns>A validated salary.</returns>
        private static decimal GetSalary()
        {
            bool isValidSalary = false;
            decimal salary = 0;
            while (!isValidSalary)
            {
                Console.Write("Enter employee salary: ");
                isValidSalary = decimal.TryParse(Console.ReadLine() !.Trim(), out salary) && salary > 0;
                if (!isValidSalary)
                {
                    Console.WriteLine("Invalid salary. Please enter a positive number.");
                }
            }

            return salary;
        }

        /// <summary>
        /// Displays the operations menu until the user chooses to exit.
        /// </summary>
        /// <param name="employee">The selected <see cref="Employee"/>.</param>
        private static void ShowOperations(Employee employee)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("[A] Calculate bonus");
                Console.WriteLine("[B] Print details");
                Console.WriteLine("[C] Exit");
                Console.Write("Enter your choice: ");

                char operationChoice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                switch (operationChoice)
                {
                    case 'A':
                        Console.WriteLine($"Bonus: Rs. {employee.CalculateBonus():F2}");
                        break;

                    case 'B':
                        Console.WriteLine(employee.PrintDetails());
                        break;

                    case 'C':
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Enter a valid choice.");
                        break;
                }
            }
        }
    }
}