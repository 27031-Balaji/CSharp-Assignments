using ConsoleTables;
using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.View
{
    /// <summary>
    /// Handles console input and output for the application.
    /// </summary>
    internal class ApplicationView
    {
        /// <summary>
        /// Shows the welcome message for the specific user.
        /// </summary>
        /// <param name="userName">The username to be shown for greetings.</param>
        public void ShowWelcome(string userName)
        {
            Console.Write("========================================================\n");
            Console.Write($"Welcome, {userName}!\n");
            Console.Write("========================================================\n");
        }

        /// <summary>
        /// Displays the main menu and reads the user's selected option.
        /// </summary>
        /// <returns>The selected <see cref="MainMenuOption"/>.</returns>
        public MainMenuOption ShowMainMenu()
        {
            Console.Write("\nSelect an option:\n");
            Console.Write("[A] Add Record\n");
            Console.Write("[B] View Record\n");
            Console.Write("[C] Search Record\n");
            Console.Write("[D] Delete Record\n");
            Console.Write("[E] Edit Record\n");
            Console.Write("[F] Financial Summary\n");
            Console.Write("[G] Delete Account\n");
            Console.Write("[H] Logout\n");
            Console.Write("\nEnter your choice: ");

            string input = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
            return input switch
            {
                "A" => MainMenuOption.AddRecord,
                "B" => MainMenuOption.ViewRecord,
                "C" => MainMenuOption.SearchRecord,
                "D" => MainMenuOption.DeleteRecord,
                "E" => MainMenuOption.EditRecord,
                "F" => MainMenuOption.FinancialSummary,
                "G" => MainMenuOption.DeleteAccount,
                "H" => MainMenuOption.Logout,
                _ => MainMenuOption.Invalid
            };
        }

        /// <summary>
        /// Displays the add menu and reads the user's choice.
        /// </summary>
        /// <returns>The selected <see cref="AddMenuOption"/>.</returns>
        public AddMenuOption ShowAddMenu()
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("[A] Add Income");
            Console.WriteLine("[B] Add Expense");
            Console.WriteLine("[C] Back");
            Console.Write("\nEnter your choice: ");

            string choice = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
            return choice switch
            {
                "A" => AddMenuOption.AddIncome,
                "B" => AddMenuOption.AddExpense,
                "C" => AddMenuOption.BackToMainMenu,
                _ => AddMenuOption.Invalid
            };
        }

        /// <summary>
        /// Displays the view menu and reads the user's choice.
        /// </summary>
        /// <returns>The selected <see cref="ViewMenuOption"/>.</returns>
        public ViewMenuOption ShowViewMenu()
        {
            Console.Write("\nSelect an option to view: \n");
            Console.Write("[A] View All Records\n");
            Console.Write("[B] View All Incomes\n");
            Console.Write("[C] View All Expenses\n");
            Console.Write("[D] Back to Main Menu\n");
            Console.Write("\nEnter your choice: ");

            string choice = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
            return choice switch
            {
                "A" => ViewMenuOption.ViewAll,
                "B" => ViewMenuOption.ViewIncomes,
                "C" => ViewMenuOption.ViewExpenses,
                "D" => ViewMenuOption.BackToMainMenu,
                _ => ViewMenuOption.Invalid
            };
        }

        /// <summary>
        /// Displays the edit menu and reads the user's choice.
        /// </summary>
        /// <returns>The selected <see cref="EditMenuOption"/>.</returns>
        public EditMenuOption ShowEditMenu()
        {
            Console.Write("\nSelect an option to edit data: \n");
            Console.Write("[A] Edit Date\n");
            Console.Write("[B] Edit Amount\n");
            Console.Write("[C] Edit Classification\n");
            Console.Write("[D] Edit Description\n");
            Console.Write("[E] Back to Main Menu\n");
            Console.Write("\nEnter your choice: ");

            string choice = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
            return choice switch
            {
                "A" => EditMenuOption.Date,
                "B" => EditMenuOption.Amount,
                "C" => EditMenuOption.Classification,
                "D" => EditMenuOption.Description,
                "E" => EditMenuOption.SaveAndExit,
                _ => EditMenuOption.Invalid
            };
        }

        /// <summary>
        /// Prompts the user for a record date string.
        /// </summary>
        /// <returns>The entered date string.</returns>
        public string ReadRecordDate()
        {
            Console.Write($"Enter the date of the record in (DD/MM/YYYY) or press Enter for today's date: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Prompts the user for a start date string.
        /// </summary>
        /// <returns>The entered date string.</returns>
        public string ReadStartDate()
        {
            Console.Write($"Enter the start date in (DD/MM/YYYY) or press Enter to skip: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Prompts the user for an end date string.
        /// </summary>
        /// <returns>The entered date string.</returns>
        public string ReadEndDate()
        {
            Console.Write($"Enter the end date in (DD/MM/YYYY) or press Enter to skip: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Prompts the user for a record amount string.
        /// </summary>
        /// <returns>The entered amount string.</returns>
        public string ReadRecordAmount()
        {
            Console.Write($"Enter the amount of the record: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Prompts the user to select an income source and returns the user's input.
        /// </summary>
        /// <returns>The entered choice string for income source selection.</returns>
        public string ReadRecordSource()
        {
            Console.Write("Select Income Source:\n");
            IncomeSource[] sources = Enum.GetValues<IncomeSource>();
            for (int i = 0; i < sources.Length; i++)
            {
                Console.Write($"{i + 1}. {sources[i]}\n");
            }

            Console.Write("\nEnter choice: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Prompts the user to select an expense category and returns the user's input.
        /// </summary>
        /// <returns>The entered choice string for expense category selection.</returns>
        public string ReadRecordCategory()
        {
            Console.Write("Select Expense Category:\n");
            ExpenseCategory[] categories = Enum.GetValues<ExpenseCategory>();
            for (int i = 0; i < categories.Length; i++)
            {
                Console.Write($"{i + 1}. {categories[i]}\n");
            }

            Console.Write("\nEnter choice: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Prompts for a record identifier for the given action.
        /// </summary>
        /// <param name="action">The action being performed. (Eg: edit, delete).</param>
        /// <returns>The entered record identifier string.</returns>
        public string ReadRecordId(string action)
        {
            Console.Write($"Enter the record ID to {action}: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Prompts for an optional description for a record.
        /// </summary>
        /// <returns>The entered description string.</returns>
        public string ReadRecordDescription()
        {
            Console.Write($"Enter the description of the record (Optional): ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Prompts for a search term that may represent a date, amount, source or category.
        /// </summary>
        /// <returns>The entered search term string.</returns>
        public string ReadSearchTerm()
        {
            Console.Write($"Enter the date (DD/MM/YYYY) or amount or source/category: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Prompts for a month and year string.
        /// </summary>
        /// <returns>The entered month and year string in MM/YYYY format.</returns>
        public string ReadMonthAndYear()
        {
            Console.Write($"Enter the month and year in (MM/YYYY): ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Writes a message to the console using color based on the <see cref="MessageType"/>.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="type">The <see cref="MessageType"/> that controls the color.</param>
        public void ShowMessage(string message, MessageType type)
        {
            Console.ForegroundColor = type switch
            {
                MessageType.Success => ConsoleColor.Green,
                MessageType.Error => ConsoleColor.Red,
                MessageType.Info => ConsoleColor.Cyan,
                _ => ConsoleColor.White
            };
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays an invalid input message for the specified field.
        /// </summary>
        /// <param name="fieldName">The field name to include in the invalid message.</param>
        public void ShowInvalidMessage(string fieldName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Enter a valid {fieldName}.");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays a collection of <see cref="FinancialRecord"/> in a table.
        /// </summary>
        /// <param name="records">The collection of <see cref="FinancialRecord"/> to display.</param>
        public void DisplayRecords(IEnumerable<FinancialRecord> records)
        {
            Console.Write("\nRecord List: \n\n");
            var table = new ConsoleTable("Id", "Date", "Type", "Classification", "Amount", "Description");
            foreach (FinancialRecord record in records)
            {
                table.AddRow(record.Id, record.Date, record.Type, record.Classification, record.Amount, record.Description);
            }

            table.Write(Format.MarkDown);
        }

        /// <summary>
        /// Displays a single <see cref="FinancialRecord"/> in a table.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to display.</param>
        public void DisplaySingleRecord(FinancialRecord record)
        {
            Console.WriteLine();
            var table = new ConsoleTable("Id", "Date", "Type", "Classification", "Amount", "Description");
            table.AddRow(record.Id, record.Date, record.Type, record.Classification, record.Amount, record.Description);
            table.Write(Format.MarkDown);
        }

        /// <summary>
        /// Shows a financial summary for the given month and year.
        /// </summary>
        /// <param name="startDate">The start date for the summary.</param>
        /// <param name="endDate">The end date for the summary.</param>
        /// <param name="netIncome">Total income for the period.</param>
        /// <param name="netExpense">Total expense for the period.</param>
        /// <param name="netBalance">Net balance for the period.</param>
        /// <param name="netSavings">Savings rate percentage for the period.</param>
        /// <param name="highestExpense">The highest <see cref="Expense"/> for the period, if any.</param>
        public void ShowFinancialSummary(
             DateOnly? startDate,
             DateOnly? endDate,
             decimal netIncome,
             decimal netExpense,
             decimal netBalance,
             decimal netSavings,
             Expense? highestExpense)
        {
            Console.Write("\n========================================================\n");
            if (startDate.HasValue && endDate.HasValue)
            {
                Console.WriteLine($"Financial summary from {startDate.Value:dd/MM/yyyy} to {endDate.Value:dd/MM/yyyy}");
            }
            else if (startDate.HasValue && !endDate.HasValue)
            {
                Console.WriteLine($"Financial summary from {startDate.Value:dd/MM/yyyy} to Today");
            }
            else if (!startDate.HasValue && endDate.HasValue)
            {
                Console.WriteLine($"Financial summary until {endDate.Value:dd/MM/yyyy}");
            }
            else
            {
                Console.WriteLine($"Overall financial summary");
            }

            Console.Write("========================================================\n");
            Console.Write($"\nYour Net Income: INR {netIncome:F2}\n");
            Console.Write($"Your Net Expense: INR {netExpense:F2}\n");
            Console.Write($"Your Net Balance: INR {netBalance:F2}\n");
            Console.Write($"Your Savings Rate: {netSavings:F2}%\n\n");
            Console.WriteLine($"Highest Expense: {highestExpense?.Amount.ToString("F2") ?? "None"}");
            Console.WriteLine($"Category: {highestExpense?.Classification ?? "None"}");
        }

        /// <summary>
        /// Clears the console display.
        /// </summary>
        public void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Waits for a key press and then clears the console.
        /// </summary>
        public void ClearScreenWithKey()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.ResetColor();
        }

        /// <summary>
        /// Prompts the user to confirm any action from the user.
        /// </summary>
        /// <param name="action">The action to be asked to perform.</param>
        /// <returns>True if the user confirms deletion, otherwise False.</returns>
        public bool ConfirmAction(string action = "retry")
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"Are you sure you want to {action}? (Y/N): ");
                Console.ResetColor();

                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice.Trim().ToUpper())
                {
                    case "Y":
                        return true;

                    case "N":
                        return false;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter Y or N.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}