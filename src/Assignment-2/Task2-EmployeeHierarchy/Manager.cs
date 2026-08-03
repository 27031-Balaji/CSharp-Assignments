namespace EmployeeHierarchy.Models
{
    /// <summary>
    /// Represents a manager and provides manager-specific bonus calculation and details.
    /// </summary>
    internal class Manager : Employee
    {
        private const decimal BonusPercentage = 0.2m;

        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// </summary>
        /// <param name="name">The name of the manager.</param>
        /// <param name="salary">The salary of the manager.</param>
        public Manager(string name, decimal salary)
            : base(name, salary)
        {
        }

        /// <summary>
        /// Calculates the manager's bonus amount.
        /// </summary>
        /// <returns>The calculated bonus as a decimal.</returns>
        public override decimal CalculateBonus()
        {
            return this.Salary * BonusPercentage;
        }

        /// <summary>
        /// Produces a string that contains the manager's details.
        /// </summary>
        /// <returns>A string describing the manager, salary and bonus.</returns>
        public override string PrintDetails()
        {
            return $"\nManager: \nName: {this.Name}\nSalary = {this.Salary}\nBonus = {this.CalculateBonus():F2}\n";
        }
    }
}