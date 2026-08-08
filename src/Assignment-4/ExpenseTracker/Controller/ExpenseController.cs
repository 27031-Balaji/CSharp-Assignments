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
                MainMenuOption menuOption = this._view.ShowMainMenu();
                switch (menuOption)
                {
                    case MainMenuOption.AddRecord:
                        this.AddRecord();
                        break;

                    case MainMenuOption.ViewRecord:
                        this.ViewRecords();
                        break;

                    case MainMenuOption.SearchRecord:
                        this.SearchRecords();
                        break;

                    case MainMenuOption.DeleteRecord:
                        this.DeleteRecord();
                        break;

                    case MainMenuOption.EditRecord:
                        this.EditRecord();
                        break;

                    case MainMenuOption.FinancialSummary:
                        break;

                    case MainMenuOption.Exit:
                        this._view.ShowMessage(ConsoleMessages.ExitMessage, MessageType.Info);
                        Thread.Sleep(1000);
                        isRunning = false;
                        break;

                    case MainMenuOption.Invalid:
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
                AddMenuOption addOption = this._view.ShowAddMenu();
                switch (addOption)
                {
                    case AddMenuOption.AddIncome:
                        this.AddIncome();
                        isRunning = false;
                        break;

                    case AddMenuOption.AddExpense:
                        this.AddExpense();
                        isRunning = false;
                        break;

                    case AddMenuOption.BackToMainMenu:
                        isRunning = false;
                        this._view.ClearScreen();
                        break;

                    case AddMenuOption.Invalid:
                        this._view.ShowInvalidMessage("option");
                        break;
                }
            }
        }

        private void ViewRecords()
        {
            if (!this.HasRecords())
            {
                this._view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return;
            }

            bool isRunning = true;
            while (isRunning)
            {
                ViewMenuOption viewOption = this._view.ShowViewMenu();
                switch (viewOption)
                {
                    case ViewMenuOption.ViewAll:
                        this.DisplayAllRecords();
                        isRunning = false;
                        break;

                    case ViewMenuOption.ViewIncomes:
                        this.DisplayAllIncomes();
                        isRunning = false;
                        break;

                    case ViewMenuOption.ViewExpenses:
                        this.DisplayAllExpenses();
                        isRunning = false;
                        break;

                    case ViewMenuOption.BackToMainMenu:
                        isRunning = false;
                        this._view.ClearScreen();
                        break;

                    case ViewMenuOption.Invalid:
                        this._view.ShowInvalidMessage("option");
                        break;
                }
            }
        }

        private void SearchRecords()
        {
            if (!this.HasRecords())
            {
                this._view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return;
            }

            IEnumerable<FinancialRecord> searchedRecords = this.GetMatchingRecords();
            if (searchedRecords.Count() == 0)
            {
                this._view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Info);
                this._view.ClearScreenWithKey();
                return;
            }

            this._view.DisplayRecords(searchedRecords);
            this._view.ClearScreenWithKey();
        }

        private void DeleteRecord()
        {
            if (!this.HasRecords())
            {
                this._view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return;
            }

            IEnumerable<FinancialRecord> searchedRecords = this.GetMatchingRecords();
            if (searchedRecords.Count() == 0)
            {
                this._view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Info);
                return;
            }

            this._view.DisplayRecords(searchedRecords);

            if (!this.GetValidRecordId(out string recordId, "delete", searchedRecords))
            {
                return;
            }

            FinancialRecord record = this._service.GetRecordById(recordId) !;
            this._view.DisplaySingleRecord(record);

            if (!this._view.ConfirmDelete())
            {
                this._view.ShowMessage(ConsoleMessages.DeleteOperationAbortedMessage, MessageType.Error);
                this._view.ClearScreenWithKey();
                return;
            }

            this._service.DeleteRecord(record);
            this._view.ShowMessage(ConsoleMessages.DeleteOperationSuccessMessage, MessageType.Success);
            this._view.ClearScreenWithKey();
        }

        private void EditRecord()
        {
            if (!this.HasRecords())
            {
                this._view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return;
            }

            IEnumerable<FinancialRecord> searchedRecords = this.GetMatchingRecords();
            if (searchedRecords.Count() == 0)
            {
                this._view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Info);
                return;
            }

            this._view.DisplayRecords(searchedRecords);

            if (!this.GetValidRecordId(out string recordId, "edit", searchedRecords))
            {
                return;
            }

            FinancialRecord record = this._service.GetRecordById(recordId) !;

            bool isRunning = true;
            while (isRunning)
            {
                EditMenuOption editOption = this._view.ShowEditMenu();
                switch (editOption)
                {
                    case EditMenuOption.Date:
                        this.EditDate(record);
                        this._view.ShowMessage(ConsoleMessages.DateEditedSuccessMessage, MessageType.Success);
                        break;

                    case EditMenuOption.Amount:
                        this.EditAmount(record);
                        this._view.ShowMessage(ConsoleMessages.AmountEditedSuccessMessage, MessageType.Success);
                        break;

                    case EditMenuOption.Classification:
                        this.EditClassification(record);
                        this._view.ShowMessage(ConsoleMessages.ClassificationEditedSuccessMessage, MessageType.Success);
                        break;

                    case EditMenuOption.Description:
                        this.EditDescription(record);
                        this._view.ShowMessage(ConsoleMessages.DescriptionEditedSuccessMessage, MessageType.Success);
                        break;

                    case EditMenuOption.SaveAndExit:
                        isRunning = false;
                        this._view.ShowMessage(ConsoleMessages.EditOperationSuccessMessage, MessageType.Success);
                        break;

                    case EditMenuOption.Invalid:
                        this._view.ShowInvalidMessage("option");
                        if (!this._view.AskRetry())
                        {
                            isRunning = false;
                        }

                        break;
                }
            }

            this._view.ClearScreenWithKey();
        }

        private bool HasRecords()
        {
            return !this._service.IsRecordListEmpty();
        }

        private void AddIncome()
        {
            if (!this.GetValidDate(out DateOnly date))
            {
                return;
            }

            if (!this.GetValidAmount(out decimal amount))
            {
                return;
            }

            if (!this.GetValidSource(out IncomeSource source))
            {
                return;
            }

            string? description = this._view.ReadRecordDescription().Trim();

            this._service.AddIncome(date, amount, source, description);
            this._view.ShowMessage(ConsoleMessages.IncomeAddedMessage, MessageType.Success);
            this._view.ClearScreenWithKey();
        }

        private void AddExpense()
        {
            if (!this.GetValidDate(out DateOnly date))
            {
                return;
            }

            if (!this.GetValidAmount(out decimal amount))
            {
                return;
            }

            if (!this.GetValidCategory(out ExpenseCategory category))
            {
                return;
            }

            string? description = this._view.ReadRecordDescription().Trim();

            this._service.AddExpense(date, amount, category, description);
            this._view.ShowMessage(ConsoleMessages.ExpenseAddedMessage, MessageType.Success);
            this._view.ClearScreenWithKey();
        }

        private void DisplayAllRecords()
        {
            IEnumerable<FinancialRecord> records = this._service.GetAllRecords();
            this._view.DisplayRecords(records);
            this._view.ClearScreenWithKey();
        }

        private void DisplayAllIncomes()
        {
            IEnumerable<FinancialRecord> records = this._service.GetIncomeRecords();
            this._view.DisplayRecords(records);
            this._view.ClearScreenWithKey();
        }

        private void DisplayAllExpenses()
        {
            IEnumerable<FinancialRecord> records = this._service.GetExpenseRecords();
            this._view.DisplayRecords(records);
            this._view.ClearScreenWithKey();
        }

        private IEnumerable<FinancialRecord> GetMatchingRecords()
        {
            string searchTerm = this._view.ReadSearchTerm();

            if (this._helper.IsValidSource(searchTerm, out IncomeSource sameSource)
                && this._helper.IsValidCategory(searchTerm, out ExpenseCategory sameCategory))
            {
                return this._service.SearchBySource(sameSource)
                    .Concat(this._service.SearchByCategory(sameCategory))
                    .ToList();
            }

            switch (this._helper.ReturnSearchType(searchTerm))
            {
                case SearchType.Date:
                    this._helper.IsValidDate(searchTerm, out DateOnly date);
                    return this._service.SearchByDate(date);

                case SearchType.Amount:
                    this._helper.IsValidAmount(searchTerm, out decimal amount);
                    return this._service.SearchByAmount(amount);

                case SearchType.Source:
                    this._helper.IsValidSource(searchTerm, out IncomeSource source);
                    return this._service.SearchBySource(source);

                case SearchType.Category:
                    this._helper.IsValidCategory(searchTerm, out ExpenseCategory category);
                    return this._service.SearchByCategory(category);

                default:
                    return new List<FinancialRecord>();
            }
        }

        private void EditDate(FinancialRecord record)
        {
            if (!this.GetValidDate(out DateOnly date))
            {
                return;
            }

            this._service.EditRecordDate(record, date);
        }

        private void EditAmount(FinancialRecord record)
        {
            if (!this.GetValidAmount(out decimal amount))
            {
                return;
            }

            this._service.EditRecordAmount(record, amount);
        }

        private void EditClassification(FinancialRecord record)
        {
            if (record is Income income)
            {
                if (!this.GetValidSource(out IncomeSource source))
                {
                    return;
                }

                this._service.EditRecordSource(income, source);
                return;
            }
            else if (record is Expense expense)
            {
                if (!this.GetValidCategory(out ExpenseCategory category))
                {
                    return;
                }

                this._service.EditRecordCategory(expense, category);
                return;
            }
        }

        private void EditDescription(FinancialRecord record)
        {
            string? description = this._view.ReadRecordDescription();

            this._service.EditRecordDescription(record, description);
        }

        private bool GetValidDate(out DateOnly date)
        {
            date = default;
            string input;
            do
            {
                input = this._view.ReadRecordDate();
                if (this._helper.IsValidDate(input, out date))
                {
                    return true;
                }
            }
            while (this.CanRetry("date"));

            return false;
        }

        private bool GetValidAmount(out decimal amount)
        {
            amount = 0;
            string input;
            do
            {
                input = this._view.ReadRecordAmount();
                if (this._helper.IsValidAmount(input, out amount))
                {
                    return true;
                }
            }
            while (this.CanRetry("amount"));

            return false;
        }

        private bool GetValidSource(out IncomeSource source)
        {
            source = IncomeSource.Other;
            IncomeSource[] sources = Enum.GetValues<IncomeSource>();
            string input;
            do
            {
                input = this._view.ReadRecordSource();
                if (this._helper.IsValidClassificationChoice(input, sources.Length, out int choice))
                {
                    source = sources[choice - 1];
                    return true;
                }

                this._view.ShowMessage(ConsoleMessages.InvalidOptionMessage, MessageType.Error);
            }
            while (this.CanRetry("source"));

            return false;
        }

        private bool GetValidCategory(out ExpenseCategory category)
        {
            category = ExpenseCategory.Other;
            ExpenseCategory[] categories = Enum.GetValues<ExpenseCategory>();
            string input;
            do
            {
                input = this._view.ReadRecordCategory();
                if (this._helper.IsValidClassificationChoice(input, categories.Length, out int choice))
                {
                    category = categories[choice - 1];
                    return true;
                }

                this._view.ShowMessage(ConsoleMessages.InvalidOptionMessage, MessageType.Error);
            }
            while (this.CanRetry("category"));

            return false;
        }

        private bool GetValidRecordId(out string recordId, string action, IEnumerable<FinancialRecord> searchedRecords)
        {
            recordId = string.Empty;
            do
            {
                recordId = this._view.ReadRecordId(action);

                if (!this._helper.IsValidRecordId(recordId))
                {
                    continue;
                }

                if (!this.IsDisplayedRecord(recordId, searchedRecords))
                {
                    this._view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                    continue;
                }

                return true;
            }
            while (this.CanRetry("id"));

            return false;
        }

        private bool IsDisplayedRecord(string recordId, IEnumerable<FinancialRecord> records)
        {
            foreach (FinancialRecord record in records)
            {
                if (record.Id == recordId)
                {
                    return true;
                }
            }

            return false;
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