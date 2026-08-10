using ExpenseTracker.Enums;
using ExpenseTracker.Helper;
using ExpenseTracker.Model;
using ExpenseTracker.Service;
using ExpenseTracker.View;

namespace ExpenseTracker.Controller
{
    /// <summary>
    /// Coordinates user interactions and application flow.
    /// </summary>
    internal class ExpenseController
    {
        private readonly ExpenseService _service;
        private readonly ExpenseHelper _helper;
        private readonly ConsoleOperation _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseController"/> class.
        /// </summary>
        /// <param name="service">The <see cref="ExpenseService"/> used for business operations.</param>
        /// <param name="helper">The <see cref="ExpenseHelper"/> used for validation and parsing.</param>
        /// <param name="view">The <see cref="ConsoleOperation"/> used for console input/output.</param>
        public ExpenseController(ExpenseService service, ExpenseHelper helper, ConsoleOperation view)
        {
            this._service = service;
            this._helper = helper;
            this._view = view;
        }

        /// <summary>
        /// Starts the controller loop with all the basic functionalities.
        /// </summary>
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
                        this.GetFinancialSummary();
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

        /// <summary>
        /// Shows the add menu and coordinates the add functionalities.
        /// </summary>
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

        /// <summary>
        /// Shows the view menu and processes view functionalities.
        /// </summary>
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

        /// <summary>
        /// Searches records using user input and displays matching records.
        /// </summary>
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

        /// <summary>
        /// Deletes a selected record after confirmation.
        /// </summary>
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

        /// <summary>
        /// Edits a selected record by presenting edit options to the user.
        /// </summary>
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

        /// <summary>
        /// Prompts user for month and year, then shows a financial summary for that period.
        /// </summary>
        private void GetFinancialSummary()
        {
            if (!this.HasRecords())
            {
                return;
            }

            if (!this.GetValidMonthAndYear(out int month, out int year))
            {
                return;
            }

            var summary = this._service.GetMonthlySummary(month, year);
            this._view.ShowFinancialSummary(month, year, summary.NetIncome, summary.NetExpense, summary.NetBalance, summary.SavingsRate, summary.HighestExpense);
            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Determines whether there are any records available.
        /// </summary>
        /// <returns>True if there are no records, otherwise false.</returns>
        private bool HasRecords()
        {
            return !this._service.IsRecordListEmpty();
        }

        /// <summary>
        /// Validates input and creates a new <see cref="Income"/> record.
        /// </summary>
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

        /// <summary>
        /// Validates input and creates a new <see cref="Expense"/> record.
        /// </summary>
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

        /// <summary>
        /// Displays all records to the user.
        /// </summary>
        private void DisplayAllRecords()
        {
            IEnumerable<FinancialRecord> records = this._service.GetAllRecords();
            this._view.DisplayRecords(records);
            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Displays all income records to the user.
        /// </summary>
        private void DisplayAllIncomes()
        {
            IEnumerable<FinancialRecord> records = this._service.GetIncomeRecords();
            this._view.DisplayRecords(records);
            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Displays all expense records to the user.
        /// </summary>
        private void DisplayAllExpenses()
        {
            IEnumerable<FinancialRecord> records = this._service.GetExpenseRecords();
            this._view.DisplayRecords(records);
            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Produces a set of records that match the user's search input.
        /// </summary>
        /// <returns>A collection of <see cref="FinancialRecord"/> that match the search input.</returns>
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

        /// <summary>
        /// Prompts and validates a new date value for the provided record, then updates it.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        private void EditDate(FinancialRecord record)
        {
            if (!this.GetValidDate(out DateOnly date))
            {
                return;
            }

            this._service.EditRecordDate(record, date);
        }

        /// <summary>
        /// Prompts and validates a new amount value for the provided record, then updates it.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        private void EditAmount(FinancialRecord record)
        {
            if (!this.GetValidAmount(out decimal amount))
            {
                return;
            }

            this._service.EditRecordAmount(record, amount);
        }

        /// <summary>
        /// Prompts and updates the classification (source or category) for the provided record.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> whose classification will be edited.</param>
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

        /// <summary>
        /// Prompts for and updates the description for the provided record.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        private void EditDescription(FinancialRecord record)
        {
            string? description = this._view.ReadRecordDescription();

            this._service.EditRecordDescription(record, description);
        }

        /// <summary>
        /// Prompts the user for a date and validates it.
        /// </summary>
        /// <param name="date">When this method returns, contains the validated date if successful.</param>
        /// <returns>True if a valid date was provided, otherwise false.</returns>
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

        /// <summary>
        /// Prompts the user for an amount and validates it.
        /// </summary>
        /// <param name="amount">When this method returns, contains the validated amount if successful.</param>
        /// <returns>True if a valid amount was provided, otherwise false.</returns>
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

        /// <summary>
        /// Prompts the user to choose and validates an income source option.
        /// </summary>
        /// <param name="source">When this method returns, contains the selected <see cref="IncomeSource"/> if successful.</param>
        /// <returns>True if a valid source was chosen, otherwise false.</returns>
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

        /// <summary>
        /// Prompts the user to choose and validates an expense category option.
        /// </summary>
        /// <param name="category">When this method returns, contains the selected <see cref="ExpenseCategory"/> if successful.</param>
        /// <returns>True if a valid category was chosen, otherwise false.</returns>
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

        /// <summary>
        /// Prompts for a record id and validates the record id that must exist within the provided search results.
        /// </summary>
        /// <param name="recordId">When this method returns, contains the validated identifier if successful.</param>
        /// <param name="action">The action being performed (Eg: edit, delete).</param>
        /// <param name="searchedRecords">The set of records displayed to the user to pick from.</param>
        /// <returns>True if a valid record id was obtained, otherwise false.</returns>
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

        /// <summary>
        /// Prompts the user for a month and year and validates the values.
        /// </summary>
        /// <param name="month">When this method returns, contains the validated month if successful.</param>
        /// <param name="year">When this method returns, contains the validated year if successful.</param>
        /// <returns>True if valid month and year were provided, otherwise false.</returns>
        private bool GetValidMonthAndYear(out int month, out int year)
        {
            month = 0;
            year = 0;
            do
            {
                string? input = this._view.ReadMonthAndYear();
                if (this._helper.IsValidMonthAndYear(input, out month, out year))
                {
                    return true;
                }
            }
            while (this.CanRetry("month and year"));

            return false;
        }

        /// <summary>
        /// Determines whether a given record identifier is present in the provided collection.
        /// </summary>
        /// <param name="recordId">The identifier to search for.</param>
        /// <param name="records">The collection of <see cref="FinancialRecord"/> to search.</param>
        /// <returns>True if the identifier is present in the collection, otherwise false.</returns>
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
        /// Shows an invalid-input message for the specified field and asks the user whether to retry.
        /// </summary>
        /// <param name="field">The name of the field with invalid input.</param>
        /// <returns>True if the user chooses to retry, otherwise false.</returns>
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