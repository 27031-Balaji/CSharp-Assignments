namespace EmployeeHierarchy.Models
{
    /// <summary>
    /// Represents a developer and provides developer-specific bonus calculation and details.
    /// </summary>
    internal class Developer : Employee
    {
        private const decimal BonusPercentage = 0.1m;

        /// <summary>
        /// Initializes a new instance of the <see cref="Developer"/> class.
        /// </summary>
        /// <param name="name">The name of the developer.</param>
        /// <param name="salary">The salary of the developer.</param>
        public Developer(string name, decimal salary)
            : base(name, salary)
        {
        }

        /// <summary>
        /// Calculates the developer's bonus amount.
        /// </summary>
        /// <returns>The calculated bonus as a decimal.</returns>
        public override decimal CalculateBonus()
        {
            return this.Salary * BonusPercentage;
        }

        /// <summary>
        /// Produces a string that contains the developer's details.
        /// </summary>
        /// <returns>A string describing the developer, salary and bonus.</returns>
        public override string PrintDetails()
        {
            return $"\nDeveloper: \nName: {this.Name}\nSalary: Rs. {this.Salary}\nBonus: Rs. {this.CalculateBonus():F2}\n";
        }
    }
}