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
        /// <param name="name">The name of the <see cref="Manager"/>.</param>
        /// <param name="salary">The salary of the <see cref="Manager"/>.</param>
        public Manager(string name, decimal salary)
            : base(name, salary)
        {
        }

        /// <summary>
        /// Calculates the bonus amount of <see cref="Manager"/>.
        /// </summary>
        /// <returns>The calculated bonus of the <see cref="Manager"/>.</returns>
        public override decimal CalculateBonus()
        {
            return this.Salary * BonusPercentage;
        }

        /// <summary>
        /// Produces a string that contains the details of the <see cref="Manager"/>.
        /// </summary>
        /// <returns>A string describing the details of the <see cref="Manager"/>.</returns>
        public override string PrintDetails()
        {
            return $"\nManager: \nName: {this.Name}\nSalary = Rs. {this.Salary}\nBonus = Rs. {this.CalculateBonus():F2}\n";
        }
    }
}