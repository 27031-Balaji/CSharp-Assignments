using Task2.Helpers;
using Task2.Models;
using Task2.Services;

namespace Task2.View
{
    /// <summary>
    /// Handles all console interactions related to employee operations.
    /// </summary>
    internal class ConsoleOperations
    {
        private readonly EmployeeServices _employeeServices;
        private readonly EmployeeHelpers _employeeHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleOperations"/> class.
        /// </summary>
        /// <param name="employeeServices">Provides employee-related business logic.</param>
        /// <param name="employeeHelper">Provides validation methods for employee input.</param>
        public ConsoleOperations(EmployeeServices employeeServices, EmployeeHelpers employeeHelper)
        {
            this._employeeServices = employeeServices;
            this._employeeHelper = employeeHelper;
        }

        /// <summary>
        /// Starts the employee bonus calculator application.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Welcome to Employee Bonus Calculator.");
            Employee employee = this.GetEmployee();
            this.ShowOperations(employee);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Gets the employee details from the user and creates the selected employee type.
        /// </summary>
        /// <returns>A employee object based on the user's choice.</returns>
        public Employee GetEmployee()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose the designation:");
                Console.WriteLine("[A] Developer");
                Console.WriteLine("[B] Manager");
                Console.Write("Enter your choice: ");
                char choice = char.ToUpper(Console.ReadKey().KeyChar);
                if (!this._employeeHelper.IsValidChoice(choice))
                {
                    Console.WriteLine("\nEnter a valid choice (A or B). Please try again.");
                    continue;
                }

                Console.WriteLine();
                string name = this.GetName();
                decimal salary = this.GetSalary();
                switch (choice)
                {
                    case 'A':
                        return this._employeeServices.CreateDeveloper(name, salary);

                    case 'B':
                        return this._employeeServices.CreateManager(name, salary);
                }
            }
        }

        /// <summary>
        /// Gets and validates the employee's name.
        /// </summary>
        /// <returns>A valid employee name.</returns>
        private string GetName()
        {
            while (true)
            {
                Console.Write("Enter the employee's name: ");
                string name = Console.ReadLine() ?? string.Empty;
                if (!this._employeeHelper.IsValidName(name))
                {
                    Console.WriteLine("Invalid name. Please try again.");
                    continue;
                }

                return name;
            }
        }

        /// <summary>
        /// Gets and validates the employee's salary.
        /// </summary>
        /// <returns>A valid positive salary.</returns>
        private decimal GetSalary()
        {
            while (true)
            {
                Console.Write("Enter the employee's salary: ");
                string input = Console.ReadLine() ?? string.Empty;
                if (!this._employeeHelper.IsValidPositiveNumber(input, out decimal salary))
                {
                    Console.WriteLine("Invalid salary. Please enter a positive number.");
                    continue;
                }

                return salary;
            }
        }

        /// <summary>
        /// Displays the available operations for the selected employee.
        /// </summary>
        /// <param name="employee">The employee on whom the operations are performed.</param>
        private void ShowOperations(Employee employee)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("[A] Calculate Bonus");
                Console.WriteLine("[B] Print Details");
                Console.WriteLine("[C] Exit");
                Console.Write("Enter your choice: ");
                char choice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
                switch (choice)
                {
                    case 'A':
                        Console.WriteLine($"Your bonus amount: {employee.CalculateBonus():F2}");
                        break;

                    case 'B':
                        Console.WriteLine(employee.PrintDetails());
                        break;

                    case 'C':
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}