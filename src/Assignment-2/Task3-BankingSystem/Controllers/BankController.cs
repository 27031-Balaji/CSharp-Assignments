using BankingSystem.Helpers;
using BankingSystem.Models;
using BankingSystem.Services;
using BankingSystem.View;

namespace BankingSystem.Controllers
{
    /// <summary>
    /// Controls the application flow for the banking system.
    /// </summary>
    internal class BankController
    {
        private const int MaximumAttempts = 3;
        private const decimal MinimumBalance = 1000;

        // Error messages
        private const string InvalidChoiceMessage = "Invalid choice.";
        private const string InvalidAccountTypeMessage = "Invalid account type.";
        private const string NoAccountsMessage = "No accounts available.";
        private const string InvalidAccountNumberMessage = "Invalid account number.";
        private const string WithdrawalFailedMessage = "Withdrawal failed because of low balance.";
        private const string AccountNotFoundMessage = "Account not found.";
        private const string InvalidAmountMessage = "Invalid amount.";
        private const string MaximumAttemptsReachedMessage = "Maximum attempts reached.";

        // Success messages
        private const string SavingsAccountCreatedMessage = "Savings Account Created Successfully.";
        private const string CheckingAccountCreatedMessage = "Checking Account Created Successfully.";
        private const string DepositSuccessMessage = "Deposit successful.";
        private const string WithdrawSuccessMessage = "Withdrawal successful.";

        private readonly BankServices _bankServices;
        private readonly BankHelpers _bankHelpers;
        private readonly ConsoleOperations _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankController"/> class.
        /// </summary>
        /// <param name="bankServices">Provides banking business logic.</param>
        /// <param name="bankHelpers">Provides validation methods.</param>
        /// <param name="view">Provides console input and output.</param>
        public BankController(BankServices bankServices, BankHelpers bankHelpers, ConsoleOperations view)
        {
            this._bankServices = bankServices;
            this._bankHelpers = bankHelpers;
            this._view = view;
        }

        /// <summary>
        /// Starts the banking system.
        /// </summary>
        public void Run()
        {
            bool isRunning = true;
            do
            {
                switch (this._view.ShowMainMenu())
                {
                    case "1":
                        this.CreateAccount();
                        this._view.FlushScreen();
                        break;

                    case "2":
                        this.DisplayAccounts();
                        this._view.FlushScreen();
                        break;

                    case "3":
                        this.Deposit();
                        this._view.FlushScreen();
                        break;

                    case "4":
                        this.Withdraw();
                        this._view.FlushScreen();
                        break;

                    case "5":
                        isRunning = false;
                        break;

                    default:
                        this._view.ShowMessage(InvalidChoiceMessage);
                        this._view.FlushScreen();
                        break;
                }
            }
            while (isRunning);
        }

        /// <summary>
        /// Creates a bank account.
        /// </summary>
        private void CreateAccount()
        {
            string choice;
            do
            {
                choice = this._view.ShowAccountTypeMenu();
                if (!this._bankHelpers.IsValidChoice(choice))
                {
                    this._view.ShowMessage(InvalidChoiceMessage);
                }
            }
            while (!this._bankHelpers.IsValidChoice(choice));
            if (!this.GetValidAmount("Enter Initial Deposit: ", out decimal amount))
            {
                return;
            }

            switch (choice)
            {
                case "1":
                    SavingsAccount? savingsAccount = this._bankServices.CreateSavingsAccount(amount);
                    if (savingsAccount == null)
                    {
                        this._view.ShowMessage($"Initial deposit must be at least Rs. {MinimumBalance}.");
                    }
                    else
                    {
                        this._view.ShowMessage(SavingsAccountCreatedMessage);
                        this._view.ShowAccountDetails(savingsAccount.PrintDetails());
                    }

                    break;

                case "2":
                    CheckingAccount checkingAccount = this._bankServices.CreateCheckingAccount(amount);
                    this._view.ShowMessage(CheckingAccountCreatedMessage);
                    this._view.ShowAccountDetails(checkingAccount.PrintDetails());
                    break;

                default:
                    this._view.ShowMessage(InvalidAccountTypeMessage);
                    break;
            }
        }

        /// <summary>
        /// Displays all bank accounts.
        /// </summary>
        private void DisplayAccounts()
        {
            if (!this._bankServices.HasAccounts())
            {
                this._view.ShowMessage(NoAccountsMessage);
                return;
            }

            foreach (BankAccount account in this._bankServices.GetAllAccounts())
            {
                this._view.ShowAccountDetails(account.PrintDetails());
            }
        }

        /// <summary>
        /// Deposits money into an account.
        /// </summary>
        private void Deposit()
        {
            if (!this._bankServices.HasAccounts())
            {
                this._view.ShowMessage(NoAccountsMessage);
                return;
            }

            if (!this.GetValidAccountNumber(out string accountNumber))
            {
                return;
            }

            if (!this.GetValidAmount("Enter Amount: ", out decimal amount))
            {
                return;
            }

            BankAccount? account = this._bankServices.Deposit(accountNumber, amount);
            if (account == null)
            {
                this._view.ShowMessage(AccountNotFoundMessage);
                return;
            }

            this._view.ShowMessage(DepositSuccessMessage);
            this._view.ShowMessage($"Current Balance : Rs. {account.Balance}");
        }

        /// <summary>
        /// Withdraws money from an account.
        /// </summary>
        private void Withdraw()
        {
            if (!this._bankServices.HasAccounts())
            {
                this._view.ShowMessage(NoAccountsMessage);
                return;
            }

            if (!this.GetValidAccountNumber(out string accountNumber))
            {
                return;
            }

            if (!this.GetValidAmount("Enter Amount: ", out decimal amount))
            {
                return;
            }

            BankAccount? account = this._bankServices.Withdraw(accountNumber, amount);
            if (account == null)
            {
                this._view.ShowMessage(WithdrawalFailedMessage);
                return;
            }

            this._view.ShowMessage(WithdrawSuccessMessage);
            this._view.ShowMessage($"Current Balance : Rs. {account.Balance}");
        }

        /// <summary>
        /// Gets a valid account number.
        /// </summary>
        /// <param name="accountNumber">The validated account number.</param>
        /// <returns>True if valid; otherwise, false.</returns>
        private bool GetValidAccountNumber(out string accountNumber)
        {
            accountNumber = string.Empty;
            for (int attempt = 1; attempt <= MaximumAttempts; attempt++)
            {
                accountNumber = this._view.ReadAccountNumber();
                if (this._bankHelpers.IsValidAccountNumber(accountNumber))
                {
                    return true;
                }

                this._view.ShowMessage($"{InvalidAccountNumberMessage} Attempts left: {MaximumAttempts - attempt}");
            }

            this._view.ShowMessage(MaximumAttemptsReachedMessage);

            return false;
        }

        /// <summary>
        /// Gets a valid amount.
        /// </summary>
        /// <param name="message">The prompt message.</param>
        /// <param name="amount">The validated amount.</param>
        /// <returns>True if valid; otherwise, false.</returns>
        private bool GetValidAmount(string message, out decimal amount)
        {
            amount = 0;
            for (int attempt = 1; attempt <= MaximumAttempts; attempt++)
            {
                string input = this._view.ReadAmount(message);
                if (this._bankHelpers.IsValidAmount(input, out amount))
                {
                    return true;
                }

                this._view.ShowMessage($"{InvalidAmountMessage} Attempts left: {MaximumAttempts - attempt}");
            }

            this._view.ShowMessage(MaximumAttemptsReachedMessage);

            return false;
        }
    }
}