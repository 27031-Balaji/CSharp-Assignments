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
        private readonly FinanceHelper financeHelper;
        private readonly ApplicationView applicationView;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationController"/> class.
        /// </summary>
        /// <param name="financeService">The <see cref="FinanceService"/>used for finance based operations.</param>
        /// <param name="authenticationService">The <see cref="AuthenticationService"/> used for authentication operations.</param>
        /// <param name="financeHelper">The <see cref="FinanceHelper"/> used for validation and parsing.</param>
        /// <param name="view">The <see cref="ApplicationView"/> used for console input/output.</param>
        public ApplicationController(FinanceService financeService, AuthenticationService authenticationService, FinanceHelper financeHelper, ApplicationView view)
        {
            this.financeService = financeService;
            this.authenticationService = authenticationService;
            this.financeHelper = financeHelper;
            this.applicationView = view;
        }

        /// <summary>
        /// Starts the controller loop with all the basic functionalities.
        /// </summary>
        public void Run()
        {
            bool isRunning = true;
            AuthenticatedUser user = this.authenticationService.LoggedInUser ?? throw new InvalidOperationException();
            while (isRunning && this.authenticationService.IsAuthenticated)
            {
                this.applicationView.ShowWelcome(user.UserName);
                if (!this.GetValidEnumChoice(this.applicationView.GetEnumOption<MainMenuOption>, OptionMessages.Option, out MainMenuOption menuOption))
                {
                    continue;
                }

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
                if (!this.GetValidEnumChoice(this.applicationView.GetEnumOption<AddMenuOption>, OptionMessages.Option, out AddMenuOption addOption))
                {
                    continue;
                }

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
                        this.applicationView.ClearScreen();
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
                this.applicationView.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return;
            }

            bool isRunning = true;
            while (isRunning)
            {
                if (!this.GetValidEnumChoice(this.applicationView.GetEnumOption<ViewMenuOption>, OptionMessages.Option, out ViewMenuOption viewOption))
                {
                    continue;
                }

                switch (viewOption)
                {
                    case ViewMenuOption.ViewAllRecords:
                        this.DisplayRecordsByType<FinancialRecord>();
                        this.applicationView.ClearScreenWithKey();
                        isRunning = false;
                        break;

                    case ViewMenuOption.ViewIncomes:
                        this.DisplayRecordsByType<Income>();
                        this.applicationView.ClearScreenWithKey();
                        isRunning = false;
                        break;

                    case ViewMenuOption.ViewExpenses:
                        this.DisplayRecordsByType<Expense>();
                        this.applicationView.ClearScreenWithKey();
                        isRunning = false;
                        break;

                    case ViewMenuOption.BackToMainMenu:
                        isRunning = false;
                        this.applicationView.ClearScreen();
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
                this.applicationView.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return;
            }

            IEnumerable<FinancialRecord> searchedRecords = this.GetMatchingRecords();
            if (searchedRecords.Count() == 0)
            {
                this.applicationView.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Info);
                this.applicationView.ClearScreenWithKey();
                return;
            }

            this.applicationView.DisplayRecords(searchedRecords);
            this.applicationView.ClearScreenWithKey();
        }

        /// <summary>
        /// Deletes a selected record after confirmation.
        /// </summary>
        private void DeleteRecord()
        {
            FinancialRecord? record = this.GetSelectedRecord(OptionMessages.DeleteOption);
            if (record == null)
            {
                return;
            }

            this.applicationView.DisplayRecords(new[] { record });

            if (!this.applicationView.ConfirmAction(OptionMessages.DeleteOption))
            {
                this.applicationView.ShowMessage(ConsoleMessages.DeleteOperationAbortedMessage, MessageType.Error);
                this.applicationView.ClearScreenWithKey();
                return;
            }

            bool isDeleted = this.financeService.DeleteRecord(record);
            this.applicationView.ShowOperationResult(isDeleted, ConsoleMessages.DeleteOperationSuccessMessage, ConsoleMessages.DeleteOperationFailedMessage);
            this.applicationView.ClearScreenWithKey();
        }

        /// <summary>
        /// Edits the selected financial record, allowing modification of date, amount, classification, and description.
        /// </summary>
        private void EditRecord()
        {
            FinancialRecord? record = this.GetSelectedRecord(OptionMessages.EditOption);
            if (record == null)
            {
                return;
            }

            bool isRunning = true;
            while (isRunning)
            {
                if (!this.GetValidEnumChoice(this.applicationView.GetEnumOption<EditMenuOption>, OptionMessages.Option, out EditMenuOption editOption))
                {
                    continue;
                }

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
                        this.applicationView.ShowMessage(ConsoleMessages.EditOperationSuccessMessage, MessageType.Success);
                        break;
                }
            }

            this.applicationView.ClearScreenWithKey();
        }

        /// <summary>
        /// Prompts user for month and year, then shows a financial summary for that period.
        /// </summary>
        private void GetFinancialSummary()
        {
            if (!this.HasRecords())
            {
                this.applicationView.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
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
             Expense? highestExpense) = this.financeService.GetFinancialSummary(startDate, endDate);
            this.applicationView.ShowFinancialSummary(startDate, endDate, netIncome, netExpense, netBalance, savingsRate, highestExpense);
            this.applicationView.ClearScreenWithKey();
        }

        /// <summary>
        /// Deletes the user account along with the transactions associated with the user.
        /// </summary>
        /// <param name="userId">The ID of the user to be deleted.</param>
        /// <returns>True if the account is deleted, else false.</returns>
        private bool DeleteAccount(Guid userId)
        {
            if (!this.applicationView.ConfirmAction(OptionMessages.DeleteOption))
            {
                this.applicationView.ShowMessage(ConsoleMessages.DeleteOperationAbortedMessage, MessageType.Error);
                return false;
            }

            this.financeService.DeleteCurrentUserRecords();
            this.authenticationService.DeleteAccount(userId);
            this.applicationView.ShowMessage(ConsoleMessages.AccountDeletedMessage, MessageType.Success);
            this.applicationView.ClearScreenWithKey();

            return true;
        }

        /// <summary>
        /// Logs out the user from the application.
        /// </summary>
        /// <returns>True if the user is logged out, else false.</returns>
        private bool Logout()
        {
            if (!this.applicationView.ConfirmAction(OptionMessages.LogoutOption))
            {
                return false;
            }

            this.authenticationService.Logout();
            this.applicationView.ShowMessage(ConsoleMessages.LogoutSuccessMessage, MessageType.Success);
            this.applicationView.ClearScreenWithKey();

            return true;
        }

        /// <summary>
        /// Determines whether there are any records available.
        /// </summary>
        /// <returns>True if records exist, otherwise false.</returns>
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

            if (!this.GetValidEnumChoice(this.applicationView.GetEnumOption<IncomeSource>, OptionMessages.Source, out IncomeSource source))
            {
                return;
            }

            string? description = this.applicationView.GetInput(PromptMessages.Description).Trim();

            this.financeService.AddIncome(date, amount, source, description);
            this.applicationView.ShowMessage(ConsoleMessages.IncomeAddedMessage, MessageType.Success);
            this.applicationView.ClearScreenWithKey();
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

            if (!this.GetValidEnumChoice(this.applicationView.GetEnumOption<ExpenseCategory>, OptionMessages.Category, out ExpenseCategory category))
            {
                return;
            }

            string? description = this.applicationView.GetInput(PromptMessages.Description).Trim();

            this.financeService.AddExpense(date, amount, category, description);
            this.applicationView.ShowMessage(ConsoleMessages.ExpenseAddedMessage, MessageType.Success);
            this.applicationView.ClearScreenWithKey();
        }

        /// <summary>
        /// Displays financial records of the specified type within a selected date range.
        /// </summary>
        /// <typeparam name="T">The type of financial record to display. Must inherit from <see cref="FinancialRecord"/>.</typeparam>
        private void DisplayRecordsByType<T>()
            where T : FinancialRecord
        {
            if (!this.GetValidDateRange(out DateOnly? startDate, out DateOnly? endDate))
            {
                return;
            }

            IEnumerable<FinancialRecord> records = this.financeService.GetByDateRange<T>(startDate, endDate);
            this.applicationView.DisplayRecords(records);
        }

        /// <summary>
        /// Produces a set of records that match the user's search input.
        /// </summary>
        /// <returns>A collection of <see cref="FinancialRecord"/> that match the search input.</returns>
        private IEnumerable<FinancialRecord> GetMatchingRecords()
        {
            string searchTerm = this.applicationView.GetInput(PromptMessages.SearchKey);
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
            this.applicationView.ShowOperationResult(isEdited, ConsoleMessages.EditOperationSuccessMessage, ConsoleMessages.EditOperationFailedMessage);
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
            this.applicationView.ShowOperationResult(isEdited, ConsoleMessages.EditOperationSuccessMessage, ConsoleMessages.EditOperationFailedMessage);
        }

        /// <summary>
        /// Prompts and updates the classification (source or category) for the provided record.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> whose classification will be edited.</param>
        private void EditClassification(FinancialRecord record)
        {
            if (record is Income income)
            {
                if (!this.GetValidEnumChoice(this.applicationView.GetEnumOption<IncomeSource>, OptionMessages.Source, out IncomeSource source))
                {
                    return;
                }

                bool isEdited = this.financeService.EditRecordSource(income, source);
                this.applicationView.ShowOperationResult(isEdited, ConsoleMessages.EditOperationSuccessMessage, ConsoleMessages.EditOperationFailedMessage);
                return;
            }
            else if (record is Expense expense)
            {
                if (!this.GetValidEnumChoice(this.applicationView.GetEnumOption<ExpenseCategory>, OptionMessages.Category, out ExpenseCategory category))
                {
                    return;
                }

                bool isEdited = this.financeService.EditRecordCategory(expense, category);
                this.applicationView.ShowOperationResult(isEdited, ConsoleMessages.EditOperationSuccessMessage, ConsoleMessages.EditOperationFailedMessage);
                return;
            }
        }

        /// <summary>
        /// Prompts for and updates the description for the provided record.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        private void EditDescription(FinancialRecord record)
        {
            string? description = this.applicationView.GetInput(PromptMessages.Description);

            bool isEdited = this.financeService.EditRecordDescription(record, description);
            this.applicationView.ShowOperationResult(isEdited, ConsoleMessages.EditOperationSuccessMessage, ConsoleMessages.EditOperationFailedMessage);
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
                input = this.applicationView.GetInput(PromptMessages.RecordDate);
                if (string.IsNullOrEmpty(input))
                {
                    date = DateOnly.FromDateTime(DateTime.Now);
                    return true;
                }

                if (this.financeHelper.IsValidDate(input, out date))
                {
                    return true;
                }

                shouldContinue = this.CanRetry(OptionMessages.Date);
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
                input = this.applicationView.GetInput(PromptMessages.Amount);
                if (this.financeHelper.IsValidAmount(input, out amount))
                {
                    return true;
                }

                shouldContinue = this.CanRetry(OptionMessages.Amount);
            }

            return false;
        }

        /// <summary>
        /// Validates user input and retrieves the corresponding value from the specified enum type.
        /// </summary>
        /// <typeparam name="T">The enum type to validate and retrieve.</typeparam>
        /// <param name="inputGetter">A function that obtains user input as a string.</param>
        /// <param name="field">The name of the field being validated.</param>
        /// <param name="value">When this method returns, contains the valid enum value if successful.</param>
        /// <returns>True if a valid enum value is retrieved, otherwise false.</returns>
        private bool GetValidEnumChoice<T>(Func<string> inputGetter, string field, out T value)
            where T : struct, Enum
        {
            value = default;
            T[] values = Enum.GetValues<T>();

            bool shouldContinue = true;
            while (shouldContinue)
            {
                string input = inputGetter();
                if (this.financeHelper.IsValidChoice(input, values.Length, out int choice))
                {
                    value = values[choice - 1];
                    return true;
                }

                shouldContinue = this.CanRetry(field);
            }

            return false;
        }

        /// <summary>
        /// Prompts for a record id and validates the record id that must exist within the provided search results.
        /// </summary>
        /// <param name="recordId">When this method returns, contains the validated identifier if successful.</param>
        /// <param name="action">The action being performed (e.g., edit or delete).</param>
        /// <param name="searchedRecords">The set of records displayed to the user to pick from.</param>
        /// <returns>True if a valid record id was obtained, otherwise false.</returns>
        private bool GetValidRecordId(out string recordId, string action, IEnumerable<FinancialRecord> searchedRecords)
        {
            recordId = string.Empty;
            bool shouldContinue = true;
            while (shouldContinue)
            {
                recordId = this.applicationView.GetInput(string.Format(PromptMessages.RecordId, action));
                if (!this.financeHelper.IsValidRecordId(recordId))
                {
                    shouldContinue = this.CanRetry(OptionMessages.Id);
                    continue;
                }

                if (this.financeService.IsDisplayedRecord(recordId, searchedRecords))
                {
                    return true;
                }

                this.applicationView.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                shouldContinue = this.CanRetry(OptionMessages.Id);
            }

            return false;
        }

        /// <summary>
        /// Validates an optional date input and assigns the parsed date if valid.
        /// </summary>
        /// <param name="prompt">A prompt given to the user while asking for the date.</param>
        /// <param name="field">The name of the field being validated, used for retry logic.</param>
        /// <param name="date">When this method returns, contains the parsed date if the input is valid, otherwise null.</param>
        /// <returns>True if a valid date is provided or if the input is empty, otherwise false.</returns>
        private bool GetValidOptionalDate(string prompt, string field, out DateOnly? date)
        {
            date = null;
            bool shouldContinue = true;
            while (shouldContinue)
            {
                string input = this.applicationView.GetInput(prompt);
                if (string.IsNullOrEmpty(input))
                {
                    return true;
                }

                if (this.financeHelper.IsValidDate(input, out DateOnly validDate))
                {
                    date = validDate;
                    return true;
                }

                shouldContinue = this.CanRetry(field);
            }

            return false;
        }

        /// <summary>
        /// Gets a valid date range from the user.
        /// </summary>
        /// <param name="startDate">The start date entered by the user, or null if not provided.</param>
        /// <param name="endDate">The end date entered by the user, or null if not provided.</param>
        /// <returns>True if the user entered the right dates for date range, else false.</returns>
        private bool GetValidDateRange(out DateOnly? startDate, out DateOnly? endDate)
        {
            if (!this.GetValidOptionalDate(PromptMessages.StartDate, OptionMessages.StartDate, out startDate))
            {
                endDate = null;
                return false;
            }

            if (!this.GetValidOptionalDate(PromptMessages.EndDate, OptionMessages.EndDate, out endDate))
            {
                return false;
            }

            if (startDate.HasValue && endDate.HasValue && startDate.Value > endDate.Value)
            {
                this.applicationView.ShowMessage(ConsoleMessages.InvalidDateRangeMessage, MessageType.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Retrieves a <see cref="FinancialRecord"/> matching the specified action, or null if no suitable record is found.
        /// </summary>
        /// <param name="action">The action to perform when selecting the record.</param>
        /// <returns>A matching <see cref="FinancialRecord"/>, or null if no record is found.</returns>
        private FinancialRecord? GetSelectedRecord(string action)
        {
            if (!this.HasRecords())
            {
                this.applicationView.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return null;
            }

            IEnumerable<FinancialRecord> searchedRecords = this.GetMatchingRecords();

            if (searchedRecords.Count() == 0)
            {
                this.applicationView.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Info);
                return null;
            }

            this.applicationView.DisplayRecords(searchedRecords);

            if (!this.GetValidRecordId(out string recordId, action, searchedRecords))
            {
                return null;
            }

            FinancialRecord? record = this.financeService.GetRecordById(recordId);

            if (record == null)
            {
                this.applicationView.ShowMessage(ConsoleMessages.NoRecordFoundMessage, MessageType.Error);
                return null;
            }

            return record;
        }

        /// <summary>
        /// Shows an invalid-input message for the specified field and asks the user whether to retry.
        /// </summary>
        /// <param name="field">The name of the field with invalid input.</param>
        /// <returns>True if the user chooses to retry, otherwise false.</returns>
        private bool CanRetry(string field)
        {
            this.applicationView.ShowInvalidMessage(field);
            bool shouldRetry = this.applicationView.ConfirmAction();
            if (!shouldRetry)
            {
                this.applicationView.ClearScreen();
            }

            return shouldRetry;
        }
    }
}