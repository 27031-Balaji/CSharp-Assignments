using Task2.Helpers;
using Task2.Models;
using Task2.Services;
using Task2.View;

namespace Task2.Controllers
{
    /// <summary>
    /// Controls the application flow for employee operations.
    /// </summary>
    internal class EmployeeController
    {
        private const string InvalidEmployeeChoiceMessage = "Enter a valid choice (A or B). Please try again.";
        private const string InvalidNameMessage = "Invalid name. Please try again.";
        private const string InvalidSalaryMessage = "Invalid salary. Please enter a positive number.";
        private const string InvalidOperationMessage = "Invalid choice. Please try again.";

        private readonly EmployeeServices _employeeServices;
        private readonly EmployeeHelpers _employeeHelpers;
        private readonly ConsoleOperations _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="employeeServices">Provides employee-related services.</param>
        /// <param name="employeeHelpers">Provides validation methods.</param>
        /// <param name="view">Provides console input and output operations.</param>
        public EmployeeController(EmployeeServices employeeServices, EmployeeHelpers employeeHelpers, ConsoleOperations view)
        {
            this._employeeServices = employeeServices;
            this._employeeHelpers = employeeHelpers;
            this._view = view;
        }

        /// <summary>
        /// Starts the employee bonus calculator.
        /// </summary>
        public void Run()
        {
            this._view.ShowWelcomeMessage();
            Employee employee = this.GetEmployee();
            this.ShowOperations(employee);
            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Gets the employee details from the user.
        /// </summary>
        /// <returns>The created employee.</returns>
        private Employee GetEmployee()
        {
            do
            {
                char choice = this._view.ShowEmployeeMenu();
                if (!this._employeeHelpers.IsValidChoice(choice))
                {
                    this._view.ShowMessage(InvalidEmployeeChoiceMessage);
                    continue;
                }

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
            while (true);
        }

        /// <summary>
        /// Gets a valid employee name.
        /// </summary>
        /// <returns>The validated employee name.</returns>
        private string GetName()
        {
            do
            {
                string name = this._view.ReadName();
                if (this._employeeHelpers.IsValidName(name))
                {
                    return name;
                }

                this._view.ShowMessage(InvalidNameMessage);
            }
            while (true);
        }

        /// <summary>
        /// Gets a valid employee salary.
        /// </summary>
        /// <returns>The validated salary.</returns>
        private decimal GetSalary()
        {
            do
            {
                string input = this._view.ReadSalary();
                if (this._employeeHelpers.IsValidPositiveNumber(input, out decimal salary))
                {
                    return salary;
                }

                this._view.ShowMessage(InvalidSalaryMessage);
            }
            while (true);
        }

        /// <summary>
        /// Displays the available operations for the selected employee.
        /// </summary>
        /// <param name="employee">The selected employee.</param>
        private void ShowOperations(Employee employee)
        {
            char choice;
            do
            {
                choice = this._view.ShowOperationMenu();
                switch (choice)
                {
                    case 'A':
                        this._view.ShowBonus(employee.CalculateBonus());
                        break;

                    case 'B':
                        this._view.ShowDetails(employee.PrintDetails());
                        break;

                    case 'C':
                        break;

                    default:
                        this._view.ShowMessage(InvalidOperationMessage);
                        break;
                }
            }
            while (choice != 'C');
        }
    }
}