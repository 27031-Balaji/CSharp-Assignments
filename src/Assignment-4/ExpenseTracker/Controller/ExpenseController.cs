using ExpenseTracker.Enums;
using ExpenseTracker.Helper;
using ExpenseTracker.Model;
using ExpenseTracker.Service;
using ExpenseTracker.View;

namespace ExpenseTracker.Controller
{
    internal class ExpenseController
    {
        private readonly ExpenseService _service;
        private readonly ExpenseHelper _helper;
        private readonly ConsoleOperation _view;

        public ExpenseController(ExpenseService service, ExpenseHelper helper, ConsoleOperation view)
        {
            this._service = service;
            this._helper = helper;
            this._view = view;
        }

        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                string menuChoice = this._view.ShowMainMenu();
                switch (menuChoice.Trim().ToUpper())
                {
                    case "A":
                        this.AddRecord();
                        break;

                    case "B":
                        this.ViewRecords();
                        break;

                    case "C":
                        break;

                    case "D":
                        break;

                    case "E":
                        break;

                    case "F":
                        break;

                    case "G":
                        this._view.ShowMessage(ConsoleMessages.ExitMessage, MessageType.Info);
                        Thread.Sleep(1000);
                        isRunning = false;
                        break;

                    default:
                        this._view.ShowInvalidMessage("option");
                        break;
                }
            }
        }

        private void AddRecord()
        {
            bool isRunning = true;
            while (isRunning)
            {
                string addChoice = this._view.AddMenu();
                switch (addChoice.Trim().ToUpper())
                {
                    case "A":
                        this.AddIncome();
                        isRunning = false;
                        break;
                    case "B":
                        this.AddExpense();
                        isRunning = false;
                        break;
                    case "C":
                        isRunning = false;
                        this._view.ClearScreen();
                        break;
                    default:
                        this._view.ShowInvalidMessage("option");
                        break;
                }
            }
        }

        private void ViewRecords()
        {
            bool isRunning = true;
            while (isRunning)
            {
                string viewChoice = this._view.ViewMenu();
                switch (viewChoice.Trim().ToUpper())
                {
                    case "A":
                        this.DisplayAllRecords();
                        isRunning = false;
                        break;
                    case "B":
                        this.DisplayAllIncomes();
                        isRunning = false;
                        break;
                    case "C":
                        this.DisplayAllExpenses();
                        isRunning = false;
                        break;
                    case "D":
                        isRunning = false;
                        this._view.ClearScreen();
                        break;
                    default:
                        this._view.ShowInvalidMessage("option");
                        break;
                }
            }
        }

        private void AddIncome()
        {
            if (!this.GetValidDate(out DateOnly date, "add"))
            {
                return;
            }

            if (!this.GetValidAmount(out decimal amount, "add"))
            {
                return;
            }

            if (!this.GetValidSource(out IncomeSource source, "add"))
            {
                return;
            }

            string? description = this._view.ReadRecordDescription("add").Trim();

            this._service.AddIncome(date, amount, source, description);
            this._view.ShowMessage(ConsoleMessages.IncomeAddedMessage, MessageType.Success);
            this._view.ClearScreenWithKey();
        }

        private void AddExpense()
        {
            if (!this.GetValidDate(out DateOnly date, "add"))
            {
                return;
            }

            if (!this.GetValidAmount(out decimal amount, "add"))
            {
                return;
            }

            if (!this.GetValidCategory(out ExpenseCategory category, "add"))
            {
                return;
            }

            string? description = this._view.ReadRecordDescription("add").Trim();

            this._service.AddExpense(date, amount, category, description);
            this._view.ShowMessage(ConsoleMessages.ExpenseAddedMessage, MessageType.Success);
            this._view.ClearScreenWithKey();
        }

        private void DisplayAllRecords()
        {
            if (!this.HasContacts())
            {
                return;
            }

            List<FinancialRecord> records = this._service.GetAllRecords();
            this._view.DisplayRecords(records);
            this._view.ClearScreenWithKey();
        }

        private void DisplayAllIncomes()
        {
            if (!this.HasContacts())
            {
                return;
            }

            List<FinancialRecord> records = this._service.GetIncomeRecords();
            this._view.DisplayRecords(records);
            this._view.ClearScreenWithKey();
        }

        private void DisplayAllExpenses()
        {
            if (!this.HasContacts())
            {
                return;
            }

            List<FinancialRecord> records = this._service.GetExpenseRecords();
            this._view.DisplayRecords(records);
            this._view.ClearScreenWithKey();
        }

        private bool GetValidDate(out DateOnly date, string action)
        {
            date = default;
            string input;
            do
            {
                input = this._view.ReadRecordDate(action);
                if (this._helper.IsValidDate(input, out date))
                {
                    return true;
                }
            }
            while (this.CanRetry("date"));

            return false;
        }

        private bool GetValidAmount(out decimal amount, string action)
        {
            amount = 0;
            string input;
            do
            {
                input = this._view.ReadRecordAmount(action);
                if (this._helper.IsValidAmount(input, out amount))
                {
                    return true;
                }
            }
            while (this.CanRetry("amount"));

            return false;
        }

        private bool GetValidSource(out IncomeSource source, string action)
        {
            source = IncomeSource.Other;
            string input;
            do
            {
                input = this._view.ReadRecordSource(action);
                if (this._helper.IsValidSource(input, out source))
                {
                    return true;
                }
            }
            while (this.CanRetry("source"));

            return false;
        }

        private bool GetValidCategory(out ExpenseCategory category, string action)
        {
            category = ExpenseCategory.Other;
            string input;
            do
            {
                input = this._view.ReadRecordCategory(action);
                if (this._helper.IsValidCategory(input, out category))
                {
                    return true;
                }
            }
            while (this.CanRetry("source"));

            return false;
        }

        private bool HasContacts()
        {
            return !this._service.IsRecordListEmpty();
        }

        /// <summary>
        /// Displays an invalid input message for the specified field and prompts the user to decide whether to retry the operation.
        /// </summary>
        /// <param name="field">The name of the field that contains invalid input.</param>
        /// <returns>True if the user chooses to retry, else false.</returns>
        private bool CanRetry(string field)
        {
            this._view.ShowInvalidMessage(field);
            bool shouldRetry = this._view.AskRetry();
            if (!shouldRetry)
            {
                this._view.ClearScreen();
            }

            return shouldRetry;
        }
    }
}
