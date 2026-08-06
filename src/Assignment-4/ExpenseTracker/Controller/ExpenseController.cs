using ExpenseTracker.Enums;
using ExpenseTracker.Helper;
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

        public void AddRecord()
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

        private bool GetValidDate(out DateOnly date, string action)
        {
            date = DateOnly.FromDateTime(DateTime.Now);
            bool isDateValid = false;
            while (!isDateValid)
            {
                string input = this._view.ReadRecordDate(action);
                if (this._helper.IsValidDate(input, out date))
                {
                    isDateValid = true;
                    break;
                }

                if (!this.CanRetry("date"))
                {
                    return false;
                }
            }

            return true;
        }

        private bool GetValidAmount(out decimal amount, string action)
        {
            amount = 0;
            bool isAmountValid = false;
            while (!isAmountValid)
            {
                string input = this._view.ReadRecordAmount(action);
                if (this._helper.IsValidAmount(input, out amount))
                {
                    isAmountValid = true;
                    break;
                }

                if (!this.CanRetry("amount"))
                {
                    return false;
                }
            }

            return true;
        }

        private bool GetValidSource(out IncomeSource source, string action)
        {
            source = IncomeSource.Other;
            bool isSourceValid = false;
            while (!isSourceValid)
            {
                string input = this._view.ReadRecordSource(action);
                if (this._helper.IsValidSource(input, out source))
                {
                    isSourceValid = true;
                    break;
                }

                if (!this.CanRetry("source"))
                {
                    return false;
                }
            }

            return true;
        }

        private bool GetValidCategory(out ExpenseCategory category, string action)
        {
            category = ExpenseCategory.Other;
            bool isSourceValid = false;
            while (!isSourceValid)
            {
                string input = this._view.ReadRecordCategory(action);
                if (this._helper.IsValidCategory(input, out category))
                {
                    isSourceValid = true;
                    break;
                }

                if (!this.CanRetry("category"))
                {
                    return false;
                }
            }

            return true;
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
