namespace Task2.Models
{
    /// <summary>
    /// Represents a generic employee with common properties and behavior for concrete roles.
    /// </summary>
    internal abstract class Employee
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="name">The name of the employee.</param>
        /// <param name="salary">The salary of the employee.</param>
        protected Employee(string name, decimal salary)
        {
            this.Name = name;
            this.Salary = salary;
        }

        /// <summary>
        /// Gets the employee's name.
        /// </summary>
        /// <value>The employee's name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the employee's salary.
        /// </summary>
        /// <value>The employee's salary.</value>
        public decimal Salary { get; }

        /// <summary>
        /// Calculates the employee's bonus amount.
        /// </summary>
        /// <returns>The calculated bonus as a decimal.</returns>
        public abstract decimal CalculateBonus();

        /// <summary>
        /// Produces a string containing the employee's details.
        /// </summary>
        /// <returns>A string describing the employee.</returns>
        public abstract string PrintDetails();
    }
}