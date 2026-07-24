using Task2.Models;

namespace Task2.Services
{
    /// <summary>
    /// Provides services and utility operations for employees.
    /// </summary>
    internal class EmployeeServices
    {
        /// <summary>
        /// Creates a new manager object for bonus calculation and printing description.
        /// </summary>
        /// <param name="name">The name of the manager.</param>
        /// <param name="salary">The salary of the manager.</param>
        /// <returns>The manager object.</returns>
        public Employee CreateManager(string name, decimal salary)
        {
            return new Manager(name, salary);
        }

        /// <summary>
        /// Creates a new developer object for bonus calculation and printing description.
        /// </summary>
        /// <param name="name">The name of the developer.</param>
        /// <param name="salary">The salary of the developer.</param>
        /// <returns>The developer object.</returns>
        public Employee CreateDeveloper(string name, decimal salary)
        {
            return new Developer(name, salary);
        }
    }
}
