using BankingSystem.Classes;

namespace BankingSystem
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        private const int MaximumAttempts = 3;
        private const decimal MinimumBalance = 1000;

        private static readonly List<BankAccount> Accounts = new List<BankAccount>();
        private static readonly Random Random = new Random();

        /// <summary>
        /// Starts the Banking System application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Banking System.");
            ShowMenu();
        }

        /// <summary>
        /// Displays the main menu and handles user operations.
        /// </summary>
        private static void ShowMenu()
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
        /// Creates a Savings or Checking account.
        /// </summary>
        private static void CreateAccount()
        {
            Console.WriteLine();
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Checking Account");
            Console.Write("Choose Account Type: ");
            string accountTypeChoice = Console.ReadLine() ?? string.Empty;

            if (!GetValidAmount("Enter Initial Deposit: ", out decimal amount))
            {
                return;
            }

            string accountNumber = GenerateAccountNumber();
            switch (accountTypeChoice)
            {
                case "1":
                    if (amount < MinimumBalance)
                    {
                        Console.WriteLine($"Initial deposit must be at least Rs. {MinimumBalance}");
                        return;
                    }

                    BankAccount savingsAccount = new SavingsAccount(accountNumber, amount);
                    Accounts.Add(savingsAccount);
                    Console.WriteLine("Savings Account Created Successfully.");
                    Console.WriteLine(savingsAccount.PrintDetails());
                    break;

                case "2":
                    BankAccount checkingAccount = new CheckingAccount(accountNumber, amount);
                    Accounts.Add(checkingAccount);
                    Console.WriteLine("Checking Account Created Successfully.");
                    Console.WriteLine(checkingAccount.PrintDetails());
                    break;

                default:
                    Console.WriteLine("Invalid account type.");
                    break;
            }
        }

        /// <summary>
        /// Displays all available bank accounts.
        /// </summary>
        private static void DisplayAccounts()
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
        private static void Deposit()
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
        private static void Withdraw()
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
        private static bool GetValidAmount(string message, out decimal amount)
        {
            amount = 0;
            for (int i = 1; i <= MaximumAttempts; i++)
            {
                Console.Write(message);
                if (decimal.TryParse(Console.ReadLine() !.Trim(), out amount) && amount > 0)
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
        private static bool GetValidAccountNumber(out string accountNumber)
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
        private static string GenerateAccountNumber()
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