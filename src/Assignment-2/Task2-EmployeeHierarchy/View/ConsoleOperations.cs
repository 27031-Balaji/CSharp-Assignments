namespace Task2.View
{
    /// <summary>
    /// Handles all console input and output operations for employee management.
    /// </summary>
    internal class ConsoleOperations
    {
        /// <summary>
        /// Displays the welcome message.
        /// </summary>
        public void ShowWelcomeMessage()
        {
            Console.WriteLine("Welcome to Employee Bonus Calculator.");
        }

        /// <summary>
        /// Displays the employee selection menu.
        /// </summary>
        /// <returns>The selected menu option.</returns>
        public char ShowEmployeeMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Choose the designation:");
            Console.WriteLine("[A] Developer");
            Console.WriteLine("[B] Manager");
            Console.Write("Enter your choice: ");

            return char.ToUpper(Console.ReadKey().KeyChar);
        }

        /// <summary>
        /// Reads the employee's name.
        /// </summary>
        /// <returns>The entered name.</returns>
        public string ReadName()
        {
            Console.Write("\nEnter the employee's name: ");
            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Reads the employee's salary.
        /// </summary>
        /// <returns>The entered salary.</returns>
        public string ReadSalary()
        {
            Console.Write("Enter the employee's salary: ");
            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Displays the operation menu.
        /// </summary>
        /// <returns>The selected menu option.</returns>
        public char ShowOperationMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("[A] Calculate Bonus");
            Console.WriteLine("[B] Print Details");
            Console.WriteLine("[C] Exit");
            Console.Write("Enter your choice: ");

            char choice = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            return choice;
        }

        /// <summary>
        /// Displays the calculated bonus.
        /// </summary>
        /// <param name="bonus">The calculated bonus amount.</param>
        public void ShowBonus(decimal bonus)
        {
            Console.WriteLine($"Your bonus amount: {bonus:F2}");
        }

        /// <summary>
        /// Displays the employee details.
        /// </summary>
        /// <param name="details">The employee details.</param>
        public void ShowDetails(string details)
        {
            Console.WriteLine(details);
        }

        /// <summary>
        /// Displays a message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Waits for the user to press a key before exiting.
        /// </summary>
        public void FlushScreenWithKey()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}