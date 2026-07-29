namespace BankingSystem.View
{
    /// <summary>
    /// Handles all console input and output operations for the banking system.
    /// </summary>
    internal class ConsoleOperations
    {
        /// <summary>
        /// Displays the main menu.
        /// </summary>
        /// <returns>The selected menu option.</returns>
        public string ShowMainMenu()
        {
            Console.WriteLine("Welcome to Banking System!");
            Console.WriteLine("Enter the following options.");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Display Accounts");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Withdraw");
            Console.WriteLine("5. Exit");
            Console.Write("\nEnter your choice: ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Displays the account type menu.
        /// </summary>
        /// <returns>The selected account type.</returns>
        public string ShowAccountTypeMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Checking Account");
            Console.Write("\nChoose Account Type: ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Reads an account number.
        /// </summary>
        /// <returns>The entered account number.</returns>
        public string ReadAccountNumber()
        {
            Console.Write("Enter Account Number: ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Reads an amount from the user.
        /// </summary>
        /// <param name="message">The prompt message.</param>
        /// <returns>The entered amount.</returns>
        public string ReadAmount(string message)
        {
            Console.Write(message);

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Displays an account's details.
        /// </summary>
        /// <param name="details">The account details.</param>
        public void ShowAccountDetails(string details)
        {
            Console.WriteLine();
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
        /// Waits for a key press and clears the console.
        /// </summary>
        public void FlushScreen()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}