namespace EmployeeHierarchy.Models
{
    /// <summary>
    /// Represents a generic employee with common properties and behavior for concrete roles.
    /// </summary>
    internal abstract class Employee
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="name">The name of the <see cref="Employee"/>.</param>
        /// <param name="salary">The salary of the <see cref="Employee"/>.</param>
        protected Employee(string name, decimal salary)
        {
            this.Name = name;
            this.Salary = salary;
        }

        /// <summary>
        /// Gets the name of the <see cref="Employee"/>.
        /// </summary>
        /// <value>The name of the <see cref="Employee"/>.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the salary of the <see cref="Employee"/>.
        /// </summary>
        /// <value>The salary of the <see cref="Employee"/>.</value>
        public decimal Salary { get; }

        /// <summary>
        /// Calculates the bonus amount of the <see cref="Employee"/>.
        /// </summary>
        /// <returns>The calculated bonus of the <see cref="Employee"/>.</returns>
        public abstract decimal CalculateBonus();

        /// <summary>
        /// Produces a string containing the details of the <see cref="Employee"/>.
        /// </summary>
        /// <returns>A string describing the <see cref="Employee"/>.</returns>
        public abstract string PrintDetails();
    }
}