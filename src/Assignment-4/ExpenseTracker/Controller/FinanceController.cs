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
    internal class FinanceController
    {
        private readonly FinanceService service;
        private readonly FinanceHelper helper;
        private readonly ConsoleOperation view;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinanceController"/> class.
        /// </summary>
        /// <param name="service">The <see cref="FinanceService"/> used for business operations.</param>
        /// <param name="helper">The <see cref="FinanceHelper"/> used for validation and parsing.</param>
        /// <param name="view">The <see cref="ConsoleOperation"/> used for console input/output.</param>
        public FinanceController(FinanceService service, FinanceHelper helper, ConsoleOperation view)
        {
            this.service = service;
            this.helper = helper;
            this.view = view;
        }

        /// <summary>
        /// Starts the controller loop with all the basic functionalities.
        /// </summary>
        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                MainMenuOption menuOption = this.view.ShowMainMenu();
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
                        isRunning = false;
                        break;

                    case MainMenuOption.Invalid:
                        this.view.ShowInvalidMessage("option");
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
                AddMenuOption addOption = this.view.ShowAddMenu();
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
                        this.view.ClearScreen();
                        break;

                    case AddMenuOption.Invalid:
                        this.view.ShowInvalidMessage("option");
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
                this.view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return;
            }

            bool isRunning = true;
            while (isRunning)
            {
                ViewMenuOption viewOption = this.view.ShowViewMenu();
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
                        this.view.ClearScreen();
                        break;

                    case ViewMenuOption.Invalid:
                        this.view.ShowInvalidMessage("option");
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
                this.view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return;
            }

            IEnumerable<FinancialRecord> searchedRecords = this.GetMatchingRecords();
            if (searchedRecords.Count() == 0)
            {
                this.view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Info);
                this.view.ClearScreenWithKey();
                return;
            }

            this.view.DisplayRecords(searchedRecords);
            this.view.ClearScreenWithKey();
        }

        /// <summary>
        /// Deletes a selected record after confirmation.
        /// </summary>
        private void DeleteRecord()
        {
            if (!this.HasRecords())
            {
                this.view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return;
            }

            IEnumerable<FinancialRecord> searchedRecords = this.GetMatchingRecords();
            if (searchedRecords.Count() == 0)
            {
                this.view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Info);
                return;
            }

            this.view.DisplayRecords(searchedRecords);

            if (!this.GetValidRecordId(out string recordId, "delete", searchedRecords))
            {
                return;
            }

            FinancialRecord record = this.service.GetRecordById(recordId) !;
            this.view.DisplaySingleRecord(record);

            if (!this.view.ConfirmDelete())
            {
                this.view.ShowMessage(ConsoleMessages.DeleteOperationAbortedMessage, MessageType.Error);
                this.view.ClearScreenWithKey();
                return;
            }

            this.service.DeleteRecord(record);
            this.view.ShowMessage(ConsoleMessages.DeleteOperationSuccessMessage, MessageType.Success);
            this.view.ClearScreenWithKey();
        }

        /// <summary>
        /// Edits a selected record by presenting edit options to the user.
        /// </summary>
        private void EditRecord()
        {
            if (!this.HasRecords())
            {
                this.view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return;
            }

            IEnumerable<FinancialRecord> searchedRecords = this.GetMatchingRecords();
            if (searchedRecords.Count() == 0)
            {
                this.view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Info);
                return;
            }

            this.view.DisplayRecords(searchedRecords);

            if (!this.GetValidRecordId(out string recordId, "edit", searchedRecords))
            {
                return;
            }

            FinancialRecord record = this.service.GetRecordById(recordId) !;

            bool isRunning = true;
            while (isRunning)
            {
                EditMenuOption editOption = this.view.ShowEditMenu();
                switch (editOption)
                {
                    case EditMenuOption.Date:
                        this.EditDate(record);
                        this.view.ShowMessage(ConsoleMessages.DateEditedSuccessMessage, MessageType.Success);
                        break;

                    case EditMenuOption.Amount:
                        this.EditAmount(record);
                        this.view.ShowMessage(ConsoleMessages.AmountEditedSuccessMessage, MessageType.Success);
                        break;

                    case EditMenuOption.Classification:
                        this.EditClassification(record);
                        this.view.ShowMessage(ConsoleMessages.ClassificationEditedSuccessMessage, MessageType.Success);
                        break;

                    case EditMenuOption.Description:
                        this.EditDescription(record);
                        this.view.ShowMessage(ConsoleMessages.DescriptionEditedSuccessMessage, MessageType.Success);
                        break;

                    case EditMenuOption.SaveAndExit:
                        isRunning = false;
                        this.view.ShowMessage(ConsoleMessages.EditOperationSuccessMessage, MessageType.Success);
                        break;

                    case EditMenuOption.Invalid:
                        this.view.ShowInvalidMessage("option");
                        if (!this.view.AskRetry())
                        {
                            isRunning = false;
                        }

                        break;
                }
            }

            this.view.ClearScreenWithKey();
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

            (decimal netIncome,
             decimal netExpense,
             decimal netBalance,
             decimal savingsRate,
             Expense? highestExpense) = this.service.GetMonthlySummary(month, year);
            this.view.ShowFinancialSummary(month, year, netIncome, netExpense, netBalance, savingsRate, highestExpense);
            this.view.ClearScreenWithKey();
        }

        /// <summary>
        /// Determines whether there are any records available.
        /// </summary>
        /// <returns>True if there are no records, otherwise false.</returns>
        private bool HasRecords()
        {
            return !this.service.IsRecordListEmpty();
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

            string? description = this.view.ReadRecordDescription().Trim();

            this.service.AddIncome(date, amount, source, description);
            this.view.ShowMessage(ConsoleMessages.IncomeAddedMessage, MessageType.Success);
            this.view.ClearScreenWithKey();
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

            string? description = this.view.ReadRecordDescription().Trim();

            this.service.AddExpense(date, amount, category, description);
            this.view.ShowMessage(ConsoleMessages.ExpenseAddedMessage, MessageType.Success);
            this.view.ClearScreenWithKey();
        }

        /// <summary>
        /// Displays all records to the user.
        /// </summary>
        private void DisplayAllRecords()
        {
            IEnumerable<FinancialRecord> records = this.service.GetAllRecords();
            this.view.DisplayRecords(records);
            this.view.ClearScreenWithKey();
        }

        /// <summary>
        /// Displays all income records to the user.
        /// </summary>
        private void DisplayAllIncomes()
        {
            IEnumerable<FinancialRecord> records = this.service.GetIncomeRecords();
            this.view.DisplayRecords(records);
            this.view.ClearScreenWithKey();
        }

        /// <summary>
        /// Displays all expense records to the user.
        /// </summary>
        private void DisplayAllExpenses()
        {
            IEnumerable<FinancialRecord> records = this.service.GetExpenseRecords();
            this.view.DisplayRecords(records);
            this.view.ClearScreenWithKey();
        }

        /// <summary>
        /// Produces a set of records that match the user's search input.
        /// </summary>
        /// <returns>A collection of <see cref="FinancialRecord"/> that match the search input.</returns>
        private IEnumerable<FinancialRecord> GetMatchingRecords()
        {
            string searchTerm = this.view.ReadSearchTerm();
            return this.service.Search(searchTerm);
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

            this.service.EditRecordDate(record, date);
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

            this.service.EditRecordAmount(record, amount);
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

                this.service.EditRecordSource(income, source);
                return;
            }
            else if (record is Expense expense)
            {
                if (!this.GetValidCategory(out ExpenseCategory category))
                {
                    return;
                }

                this.service.EditRecordCategory(expense, category);
                return;
            }
        }

        /// <summary>
        /// Prompts for and updates the description for the provided record.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        private void EditDescription(FinancialRecord record)
        {
            string? description = this.view.ReadRecordDescription();

            this.service.EditRecordDescription(record, description);
        }

        /// <summary>
        /// Prompts the user for a date and validates it.
        /// </summary>
        /// <param name="date">When this method returns, contains the validated date if successful.</param>
        /// <returns>True if a valid date was provided, otherwise false.</returns>
        private bool GetValidDate(out DateOnly date)
        {
            date = DateOnly.FromDateTime(DateTime.Now);
            string input;
            bool shouldContinue = true;
            while (shouldContinue)
            {
                input = this.view.ReadRecordDate();
                if (this.helper.IsValidDate(input, out date))
                {
                    return true;
                }

                shouldContinue = this.CanRetry("date");
            }

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
            bool shouldContinue = true;
            while (shouldContinue)
            {
                input = this.view.ReadRecordAmount();
                if (this.helper.IsValidAmount(input, out amount))
                {
                    return true;
                }

                shouldContinue = this.CanRetry("amount");
            }

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
            bool shouldContinue = true;
            while (shouldContinue)
            {
                input = this.view.ReadRecordSource();
                if (this.helper.IsValidClassificationChoice(input, sources.Length, out int choice))
                {
                    source = sources[choice - 1];
                    return true;
                }

                shouldContinue = this.CanRetry("source");
            }

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
            bool shouldContinue = true;
            while (shouldContinue)
            {
                input = this.view.ReadRecordCategory();
                if (this.helper.IsValidClassificationChoice(input, categories.Length, out int choice))
                {
                    category = categories[choice - 1];
                    return true;
                }

                shouldContinue = this.CanRetry("category");
            }

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
            bool shouldContinue = true;
            while (shouldContinue)
            {
                recordId = this.view.ReadRecordId(action);
                if (!this.helper.IsValidRecordId(recordId))
                {
                    shouldContinue = this.CanRetry("id");
                    continue;
                }

                if (this.service.IsDisplayedRecord(recordId, searchedRecords))
                {
                    return true;
                }

                this.view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                shouldContinue = this.CanRetry("id");
            }

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
            bool shouldContinue = true;
            while (shouldContinue)
            {
                string? input = this.view.ReadMonthAndYear();
                if (this.helper.IsValidMonthAndYear(input, out month, out year))
                {
                    return true;
                }

                shouldContinue = this.CanRetry("month and year");
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
            this.view.ShowInvalidMessage(field);
            bool shouldRetry = this.view.AskRetry();
            if (!shouldRetry)
            {
                this.view.ClearScreen();
            }

            return shouldRetry;
        }
    }
}