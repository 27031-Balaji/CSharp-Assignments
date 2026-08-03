using BankingSystem.Models;

namespace BankingSystem
{
    /// <summary>
    /// The program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        private const int MaximumAttempts = 3;
        private const decimal MinimumBalance = 1000;

        private static readonly List<BankAccount> Accounts = new List<BankAccount>();
        private static readonly Random Random = new Random();

        /// <summary>
        /// The main class starts the application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n===== Banking System =====");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Display Accounts");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                string menuChoice = Console.ReadLine() ?? string.Empty;

                switch (menuChoice)
                {
                    case "1":
                        CreateAccount();
                        break;

                    case "2":
                        DisplayAccounts();
                        break;

                    case "3":
                        Deposit();
                        break;

                    case "4":
                        Withdraw();
                        break;

                    case "5":
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        /// <summary>
        /// Used to create a savings or checking account.
        /// </summary>
        public static void CreateAccount()
        {
            Console.WriteLine("\n1. Savings Account");
            Console.WriteLine("2. Checking Account");
            Console.Write("Choose Account Type: ");
            string accountTypeChoice = Console.ReadLine() ?? string.Empty;

            decimal amount;
            if (!GetValidAmount("Enter Initial Deposit: ", out amount))
            {
                return;
            }

            string accountNumber = GenerateAccountNumber();
            if (accountTypeChoice == "1")
            {
                if (amount < MinimumBalance)
                {
                    Console.WriteLine($"Initial deposit must be at least Rs. {MinimumBalance}");
                    return;
                }

                BankAccount account = new SavingsAccount(accountNumber, amount);
                Accounts.Add(account);
                Console.WriteLine("Savings Account Created Successfully.");
                Console.WriteLine(account.PrintDetails());
            }
            else if (accountTypeChoice == "2")
            {
                BankAccount account = new CheckingAccount(accountNumber, amount);
                Accounts.Add(account);
                Console.WriteLine("Checking Account Created Successfully.");
                Console.WriteLine(account.PrintDetails());
            }
            else
            {
                Console.WriteLine("Invalid account type.");
            }
        }

        /// <summary>
        /// Displays details for all available bank accounts.
        /// </summary>
        public static void DisplayAccounts()
        {
            if (Accounts.Count == 0)
            {
                Console.WriteLine("No Accounts available.");
                return;
            }

            foreach (BankAccount account in Accounts)
            {
                Console.WriteLine(account.PrintDetails());
            }
        }

        /// <summary>
        /// Used to deposit amount in a specific account using Account Number.
        /// </summary>
        public static void Deposit()
        {
            if (Accounts.Count == 0)
            {
                Console.WriteLine("No Accounts available.");
                return;
            }

            if (!GetValidAccountNumber(out string accountNumber))
            {
                return;
            }

            BankAccount? account = Accounts.Find(a => a.AccountNumber == accountNumber);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            if (!GetValidAmount("Enter Amount: ", out decimal amount))
            {
                return;
            }

            account.Deposit(amount);
            Console.WriteLine("Deposit successful.");
            Console.WriteLine($"Current Balance: Rs. {account.Balance}");
        }

        /// <summary>
        /// Withdraws specific amount from a specific account.
        /// </summary>
        public static void Withdraw()
        {
            if (Accounts.Count == 0)
            {
                Console.WriteLine("No Accounts available.");
                return;
            }

            if (!GetValidAccountNumber(out string accountNumber))
            {
                return;
            }

            BankAccount? account = Accounts.Find(a => a.AccountNumber == accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            if (!GetValidAmount("Enter Amount: ", out decimal amount))
            {
                return;
            }

            if (account.Withdraw(amount))
            {
                Console.WriteLine("Withdrawal successful.");
                Console.WriteLine($"Current Balance: Rs. {account.Balance}");
            }
            else
            {
                Console.WriteLine("Withdrawal failed because of low balance.");
            }
        }

        /// <summary>
        /// Used to get the valid amount from the user.
        /// </summary>
        /// <param name="message">The message displayed when asking for amount.</param>
        /// <param name="amount">The amount entered by the user.</param>
        /// <returns>True if the right amount is entered by the user in maximum tries, else false.</returns>
        public static bool GetValidAmount(string message, out decimal amount)
        {
            amount = 0;

            for (int i = 1; i <= MaximumAttempts; i++)
            {
                Console.Write(message);
                if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
                {
                    return true;
                }

                Console.WriteLine($"Invalid amount. Attempts left: {MaximumAttempts - i}");
            }

            Console.WriteLine("Maximum attempts reached.");

            return false;
        }

        /// <summary>
        /// Used to get the valid account number from the user.
        /// </summary>
        /// <param name="accountNumber">The account number entered by the user.</param>
        /// <returns>true if a valid account number is entered, otherwise false.</returns>
        public static bool GetValidAccountNumber(out string accountNumber)
        {
            accountNumber = string.Empty;
            for (int i = 1; i <= MaximumAttempts; i++)
            {
                Console.Write("Enter Account Number: ");
                accountNumber = Console.ReadLine() ?? string.Empty;

                if (accountNumber.Length == 10 && accountNumber.All(char.IsDigit))
                {
                    return true;
                }

                Console.WriteLine($"Invalid account number. Attempts left: {MaximumAttempts - i}");
            }

            Console.WriteLine("Maximum attempts reached.");

            return false;
        }

        /// <summary>
        /// Generates a unique 10-digit account number.
        /// </summary>
        /// <returns>A string containing the generated account number.</returns>
        public static string GenerateAccountNumber()
        {
            string accountNumber;
            do
            {
                accountNumber = string.Empty;
                for (int i = 0; i < 10; i++)
                {
                    accountNumber += Random.Next(10);
                }
            }
            while (Accounts.Any(a => a.AccountNumber == accountNumber));

            return accountNumber;
        }
    }
}