using ConsoleTables;
using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.View
{
    /// <summary>
    /// Handles console input and output for the application.
    /// </summary>
    internal class ConsoleOperation
    {
        /// <summary>
        /// Displays the main menu and reads the user's selected option.
        /// </summary>
        /// <returns>The selected <see cref="MainMenuOption"/>.</returns>
        public MainMenuOption ShowMainMenu()
        {
            Console.Write("========================================================\n");
            Console.Write("Expense Tracker Application\n");
            Console.Write("Track Your Spending, Empower Your Savings!\n");
            Console.Write("========================================================\n");
            Console.Write("\nSelect an option:\n");
            Console.Write("[A] Add Record\n");
            Console.Write("[B] View Record\n");
            Console.Write("[C] Search Record\n");
            Console.Write("[D] Delete Record\n");
            Console.Write("[E] Edit Record\n");
            Console.Write("[F] Financial Summary\n");
            Console.Write("[G] Exit\n");
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
                "G" => MainMenuOption.Exit,
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
            Console.Write($"Enter the date of the record in (DD/MM/YYYY): ");

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
            Console.Write($"Enter the date or amount or source/category: ");

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
        /// <param name="month">The month being summarized.</param>
        /// <param name="year">The year being summarized.</param>
        /// <param name="netIncome">Total income for the period.</param>
        /// <param name="netExpense">Total expense for the period.</param>
        /// <param name="netBalance">Net balance for the period.</param>
        /// <param name="netSavings">Savings rate percentage for the period.</param>
        /// <param name="highestExpense">The highest <see cref="Expense"/> for the period, if any.</param>
        public void ShowFinancialSummary(int month, int year, decimal netIncome, decimal netExpense, decimal netBalance, decimal netSavings, Expense? highestExpense)
        {
            Console.Write("\n========================================================\n");
            Console.Write($"Financial Summary for {month}/{year}\n");
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
        /// Prompts the user to confirm deletion of a record.
        /// </summary>
        /// <returns>True if the user confirms deletion, otherwise False.</returns>
        public bool ConfirmDelete()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Are you sure you want to delete this record? (Y/N): ");
                Console.ResetColor();
                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice.Trim().ToUpper())
                {
                    case "Y":
                        return true;
                    case "N":
                        return false;
                    default:
                        Console.WriteLine("Enter Y or N.");
                        break;
                }
            }
        }

        /// <summary>
        /// Asks the user whether to retry the current operation.
        /// </summary>
        /// <returns>True if the user wants to retry, otherwise False.</returns>
        public bool AskRetry()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nTry again? (Y/N): ");
                Console.ResetColor();
                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice.Trim().ToUpper())
                {
                    case "Y":
                        return true;

                    case "N":
                        return false;

                    default:
                        Console.WriteLine("Please enter Y or N.");
                        break;
                }
            }
        }
    }
}