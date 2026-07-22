using BankingSystem.Helpers;
using BankingSystem.Services;
using Task3.Models;

namespace BankingSystem.View
{
    /// <summary>
    /// Handles all console interactions for the banking system.
    /// </summary>
    internal class ConsoleOperations
    {
        private const int MaximumAttempts = 3;
        private readonly BankServices _bankServices;
        private readonly BankHelpers _bankHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleOperations"/> class.
        /// </summary>
        /// <param name="bankServices">Provides banking business logic.</param>
        /// <param name="bankHelper">Provides validation methods for user input.</param>
        public ConsoleOperations(BankServices bankServices, BankHelpers bankHelper)
        {
            this._bankServices = bankServices;
            this._bankHelper = bankHelper;
        }

        /// <summary>
        /// Starts the banking system and displays the main menu.
        /// </summary>
        public void Start()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Welcome to Banking System!\nEnter the following options.");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Display Accounts");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. Exit");
                Console.Write("\nEnter your choice: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        this.CreateAccount();
                        ClearScreen();
                        break;

                    case "2":
                        this.DisplayAccounts();
                        ClearScreen();
                        break;

                    case "3":
                        this.Deposit();
                        ClearScreen();
                        break;

                    case "4":
                        this.Withdraw();
                        ClearScreen();
                        break;

                    case "5":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.\n");
                        ClearScreen();
                        break;
                }
            }
        }

        /// <summary>
        /// Clears the console screen after prompting the user to press any key to continue.
        /// </summary>
        private static void ClearScreen()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Creates a new bank account based on the user's selection.
        /// </summary>
        private void CreateAccount()
        {
            Console.WriteLine("\n1. Savings Account");
            Console.WriteLine("2. Checking Account\n");
            Console.Write("Choose Account Type: ");
            string? choice = Console.ReadLine();
            if (!this.GetValidAmount("Enter Initial Deposit: ", out decimal amount))
            {
                return;
            }

            switch (choice)
            {
                case "1":
                    Console.WriteLine(this._bankServices.CreateSavingsAccount(amount));
                    break;

                case "2":
                    Console.WriteLine(this._bankServices.CreateCheckingAccount(amount));
                    break;

                default:
                    Console.WriteLine("Invalid account type.\n");
                    break;
            }
        }

        /// <summary>
        /// Displays all available bank accounts.
        /// </summary>
        private void DisplayAccounts()
        {
            if (!this._bankServices.HasAccounts())
            {
                Console.WriteLine("No accounts available.\n");
                return;
            }

            List<BankAccount> accounts = this._bankServices.GetAllAccounts();
            foreach (BankAccount account in accounts)
            {
                Console.WriteLine();
                Console.WriteLine(account.PrintDetails());
            }
        }

        /// <summary>
        /// Deposits an amount into the specified bank account.
        /// </summary>
        private void Deposit()
        {
            if (!this._bankServices.HasAccounts())
            {
                Console.WriteLine("No accounts available.\n");
                return;
            }

            if (!this.GetValidAccountNumber(out string accountNumber))
            {
                return;
            }

            if (!this._bankServices.AccountExists(accountNumber))
            {
                Console.WriteLine("Account not found.\n");
                return;
            }

            if (!this.GetValidAmount("Enter Amount: ", out decimal amount))
            {
                return;
            }

            Console.WriteLine(this._bankServices.Deposit(accountNumber, amount));
        }

        /// <summary>
        /// Withdraws an amount from the specified bank account.
        /// </summary>
        private void Withdraw()
        {
            if (!this._bankServices.HasAccounts())
            {
                Console.WriteLine("No accounts available.\n");
                return;
            }

            if (!this.GetValidAccountNumber(out string accountNumber))
            {
                return;
            }

            if (!this._bankServices.AccountExists(accountNumber))
            {
                Console.WriteLine("Account not found.\n");
                return;
            }

            if (!this.GetValidAmount("Enter Amount: ", out decimal amount))
            {
                return;
            }

            Console.WriteLine(this._bankServices.Withdraw(accountNumber, amount));
        }

        /// <summary>
        /// Gets and validates the account number entered by the user.
        /// </summary>
        /// <param name="accountNumber">The validated account number.</param>
        /// <returns>True if a valid account number is entered, otherwise false.</returns>
        private bool GetValidAccountNumber(out string accountNumber)
        {
            accountNumber = string.Empty;
            for (int i = 1; i <= MaximumAttempts; i++)
            {
                Console.Write("Enter Account Number: ");
                accountNumber = Console.ReadLine() ?? string.Empty;
                if (this._bankHelper.IsValidAccountNumber(accountNumber))
                {
                    return true;
                }

                Console.WriteLine($"Invalid account number. Attempts left: {MaximumAttempts - i}");
            }

            Console.WriteLine("Maximum attempts reached.");
            return false;
        }

        /// <summary>
        /// Gets and validates the amount entered by the user.
        /// </summary>
        /// <param name="message">The message displayed to prompt the user.</param>
        /// <param name="amount">The validated amount.</param>
        /// <returns>True if a valid amount is entered, otherwise false.</returns>
        private bool GetValidAmount(string message, out decimal amount)
        {
            amount = 0;
            for (int i = 1; i <= MaximumAttempts; i++)
            {
                Console.Write(message);
                if (this._bankHelper.IsValidAmount(Console.ReadLine() !, out amount))
                {
                    return true;
                }

                Console.WriteLine($"Invalid amount. Attempts left: {MaximumAttempts - i}");
            }

            Console.WriteLine("Maximum attempts reached.");
            return false;
        }
    }
}