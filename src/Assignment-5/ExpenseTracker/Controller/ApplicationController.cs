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
    internal class ApplicationController
    {
        private readonly FinanceService financeService;
        private readonly AuthenticationService authenticationService;
        private readonly FinanceHelper helper;
        private readonly ApplicationView view;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationController"/> class.
        /// </summary>
        /// <param name="financeService">The <see cref="FinanceService"/>used for finance based operations.</param>
        /// <param name="authenticationService">The <see cref="AuthenticationService"/> used for authentication operations.</param>
        /// <param name="helper">The <see cref="FinanceHelper"/> used for validation and parsing.</param>
        /// <param name="view">The <see cref="ApplicationView"/> used for console input/output.</param>
        public ApplicationController(FinanceService financeService, AuthenticationService authenticationService, FinanceHelper helper, ApplicationView view)
        {
            this.financeService = financeService;
            this.authenticationService = authenticationService;
            this.helper = helper;
            this.view = view;
        }

        /// <summary>
        /// Starts the controller loop with all the basic functionalities.
        /// </summary>
        public void Run()
        {
            bool isRunning = true;
            AuthenticatedUser user = this.authenticationService.LoggedInUser!;
            while (isRunning && this.authenticationService.IsAuthenticated)
            {
                this.view.ShowWelcome(user.UserName);
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

                    case MainMenuOption.DeleteAccount:
                        isRunning = !this.DeleteAccount(user.Id);
                        break;

                    case MainMenuOption.Logout:
                        isRunning = !this.Logout();
                        break;

                    case MainMenuOption.Invalid:
                        this.view.ShowMessage(ConsoleMessages.InvalidOptionMessage, MessageType.Error);
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
                        this.view.ShowMessage(ConsoleMessages.InvalidOptionMessage, MessageType.Error);
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
                        this.view.ClearScreenWithKey();
                        isRunning = false;
                        break;

                    case ViewMenuOption.ViewIncomes:
                        this.DisplayAllIncomes();
                        this.view.ClearScreenWithKey();
                        isRunning = false;
                        break;

                    case ViewMenuOption.ViewExpenses:
                        this.DisplayAllExpenses();
                        this.view.ClearScreenWithKey();
                        isRunning = false;
                        break;

                    case ViewMenuOption.BackToMainMenu:
                        isRunning = false;
                        this.view.ClearScreen();
                        break;

                    case ViewMenuOption.Invalid:
                        this.view.ShowMessage(ConsoleMessages.InvalidOptionMessage, MessageType.Error);
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

            FinancialRecord? record = this.financeService.GetRecordById(recordId);
            if (record == null)
            {
                this.view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return;
            }

            this.view.DisplaySingleRecord(record);

            if (!this.view.ConfirmAction("delete"))
            {
                this.view.ShowMessage(ConsoleMessages.DeleteOperationAbortedMessage, MessageType.Error);
                this.view.ClearScreenWithKey();
                return;
            }

            bool isDeleted = this.financeService.DeleteRecord(record);
            string message = isDeleted
                ? ConsoleMessages.DeleteOperationSuccessMessage
                : ConsoleMessages.DeleteOperationFailedMessage;

            MessageType messageType = isDeleted ? MessageType.Success : MessageType.Error;

            this.view.ShowMessage(message, messageType);
            this.view.ClearScreenWithKey();
        }

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

            FinancialRecord? record = this.financeService.GetRecordById(recordId);
            if (record == null)
            {
                this.view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return;
            }

            bool isRunning = true;
            while (isRunning)
            {
                EditMenuOption editOption = this.view.ShowEditMenu();
                switch (editOption)
                {
                    case EditMenuOption.Date:
                        this.EditDate(record);
                        break;

                    case EditMenuOption.Amount:
                        this.EditAmount(record);
                        break;

                    case EditMenuOption.Classification:
                        this.EditClassification(record);
                        break;

                    case EditMenuOption.Description:
                        this.EditDescription(record);
                        break;

                    case EditMenuOption.SaveAndExit:
                        isRunning = false;
                        this.view.ShowMessage(ConsoleMessages.EditOperationSuccessMessage, MessageType.Success);
                        break;

                    case EditMenuOption.Invalid:
                        this.view.ShowInvalidMessage("option");
                        if (!this.view.ConfirmAction())
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
                this.view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return;
            }

            if (!this.GetValidDateRange(out DateOnly? startDate, out DateOnly? endDate))
            {
                return;
            }

            (decimal netIncome,
             decimal netExpense,
             decimal netBalance,
             decimal savingsRate,
             Expense? highestExpense) = this.financeService.GetMonthlySummary(startDate, endDate);
            this.view.ShowFinancialSummary(startDate, endDate, netIncome, netExpense, netBalance, savingsRate, highestExpense);
            this.view.ClearScreenWithKey();
        }

        /// <summary>
        /// Deletes the user account along with the transactions associated with the user.
        /// </summary>
        /// <param name="userId">The ID of the user to be deleted.</param>
        /// <returns>True if the account is deleted, else false.</returns>
        private bool DeleteAccount(Guid userId)
        {
            if (!this.view.ConfirmAction("delete"))
            {
                this.view.ShowMessage(ConsoleMessages.DeleteOperationAbortedMessage, MessageType.Error);
                return false;
            }

            this.financeService.DeleteRecordsByUserId();
            this.authenticationService.DeleteAccount(userId);
            this.view.ShowMessage(ConsoleMessages.AccountDeletedMessage, MessageType.Success);
            this.view.ClearScreenWithKey();

            return true;
        }

        /// <summary>
        /// Logs out the user from the application.
        /// </summary>
        /// <returns>True if the user is logged out, else false.</returns>
        private bool Logout()
        {
            if (!this.view.ConfirmAction("logout"))
            {
                return false;
            }

            this.authenticationService.Logout();
            this.view.ShowMessage(ConsoleMessages.LogoutSuccessMessage, MessageType.Success);
            this.view.ClearScreenWithKey();

            return true;
        }

        /// <summary>
        /// Determines whether there are any records available.
        /// </summary>
        /// <returns>True if there are no records, otherwise false.</returns>
        private bool HasRecords()
        {
            return !this.financeService.IsRecordListEmpty();
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

            this.financeService.AddIncome(date, amount, source, description);
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

            this.financeService.AddExpense(date, amount, category, description);
            this.view.ShowMessage(ConsoleMessages.ExpenseAddedMessage, MessageType.Success);
            this.view.ClearScreenWithKey();
        }

        /// <summary>
        /// Displays all records to the user.
        /// </summary>
        private void DisplayAllRecords()
        {
            if (!this.GetValidDateRange(out DateOnly? startDate, out DateOnly? endDate))
            {
                return;
            }

            IEnumerable<FinancialRecord> records = this.financeService.GetByDateRange<FinancialRecord>(startDate, endDate);
            this.view.DisplayRecords(records);
        }

        /// <summary>
        /// Displays all income records to the user.
        /// </summary>
        private void DisplayAllIncomes()
        {
            if (!this.GetValidDateRange(out DateOnly? startDate, out DateOnly? endDate))
            {
                return;
            }

            IEnumerable<FinancialRecord> records = this.financeService.GetByDateRange<Income>(startDate, endDate);
            this.view.DisplayRecords(records);
        }

        /// <summary>
        /// Displays all expense records to the user.
        /// </summary>
        private void DisplayAllExpenses()
        {
            if (!this.GetValidDateRange(out DateOnly? startDate, out DateOnly? endDate))
            {
                return;
            }

            IEnumerable<FinancialRecord> records = this.financeService.GetByDateRange<Expense>(startDate, endDate);
            this.view.DisplayRecords(records);
        }

        /// <summary>
        /// Produces a set of records that match the user's search input.
        /// </summary>
        /// <returns>A collection of <see cref="FinancialRecord"/> that match the search input.</returns>
        private IEnumerable<FinancialRecord> GetMatchingRecords()
        {
            string searchTerm = this.view.ReadSearchTerm();
            return this.financeService.Search(searchTerm);
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

            bool isEdited = this.financeService.EditRecordDate(record, date);
            string message = isEdited
                ? ConsoleMessages.EditOperationSuccessMessage
                : ConsoleMessages.EditOperationFailedMessage;

            MessageType messageType = isEdited ? MessageType.Success : MessageType.Error;

            this.view.ShowMessage(message, messageType);
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

            bool isEdited = this.financeService.EditRecordAmount(record, amount);
            string message = isEdited
                ? ConsoleMessages.EditOperationSuccessMessage
                : ConsoleMessages.EditOperationFailedMessage;

            MessageType messageType = isEdited ? MessageType.Success : MessageType.Error;

            this.view.ShowMessage(message, messageType);
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

                bool isEdited = this.financeService.EditRecordSource(income, source);
                string message = isEdited
                    ? ConsoleMessages.EditOperationSuccessMessage
                    : ConsoleMessages.EditOperationFailedMessage;

                MessageType messageType = isEdited ? MessageType.Success : MessageType.Error;

                this.view.ShowMessage(message, messageType);
                return;
            }
            else if (record is Expense expense)
            {
                if (!this.GetValidCategory(out ExpenseCategory category))
                {
                    return;
                }

                bool isEdited = this.financeService.EditRecordCategory(expense, category);
                string message = isEdited
                    ? ConsoleMessages.EditOperationSuccessMessage
                    : ConsoleMessages.EditOperationFailedMessage;

                MessageType messageType = isEdited ? MessageType.Success : MessageType.Error;

                this.view.ShowMessage(message, messageType);
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

            bool isEdited = this.financeService.EditRecordDescription(record, description);
            string message = isEdited
                ? ConsoleMessages.EditOperationSuccessMessage
                : ConsoleMessages.EditOperationFailedMessage;

            MessageType messageType = isEdited ? MessageType.Success : MessageType.Error;

            this.view.ShowMessage(message, messageType);
        }

        /// <summary>
        /// Prompts the user for a date and validates it.
        /// </summary>
        /// <param name="date">When this method returns, contains the validated date if successful.</param>
        /// <returns>True if a valid date was provided, otherwise false.</returns>
        private bool GetValidDate(out DateOnly date)
        {
            string input;
            bool shouldContinue = true;
            while (shouldContinue)
            {
                input = this.view.ReadRecordDate();
                if (string.IsNullOrEmpty(input))
                {
                    date = DateOnly.FromDateTime(DateTime.Now);
                    return true;
                }

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

                if (this.financeService.IsDisplayedRecord(recordId, searchedRecords))
                {
                    return true;
                }

                this.view.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                shouldContinue = this.CanRetry("id");
            }

            return false;
        }

        /// <summary>
        /// Gets a valid date range from the user.
        /// </summary>
        /// <param name="startDate">The start date entered by the user. (Can be empty).</param>
        /// <param name="endDate">The end date entered by the user. (Can be empty).</param>
        /// <returns>True if the user entered the right dates for date range, else false.</returns>
        private bool GetValidDateRange(out DateOnly? startDate, out DateOnly? endDate)
        {
            startDate = null;
            endDate = null;
            bool shouldRetryForStartDate = true;
            while (shouldRetryForStartDate)
            {
                string? input = this.view.ReadStartDate();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                if (this.helper.IsValidDate(input, out DateOnly validStartDate))
                {
                    startDate = validStartDate;
                    break;
                }

                shouldRetryForStartDate = this.CanRetry("start date");
            }

            bool shouldRetryForEndDate = true;
            while (shouldRetryForEndDate)
            {
                string? input = this.view.ReadEndDate();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                if (this.helper.IsValidDate(input, out DateOnly validEndDate))
                {
                    endDate = validEndDate;
                    break;
                }

                shouldRetryForEndDate = this.CanRetry("end date");
            }

            if (startDate.HasValue && endDate.HasValue && startDate.Value > endDate.Value)
            {
                this.view.ShowMessage(ConsoleMessages.InvalidDateRangeMessage, MessageType.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Shows an invalid-input message for the specified field and asks the user whether to retry.
        /// </summary>
        /// <param name="field">The name of the field with invalid input.</param>
        /// <returns>True if the user chooses to retry, otherwise false.</returns>
        private bool CanRetry(string field)
        {
            this.view.ShowInvalidMessage(field);
            bool shouldRetry = this.view.ConfirmAction();
            if (!shouldRetry)
            {
                this.view.ClearScreen();
            }

            return shouldRetry;
        }
    }
}