using ConsoleTables;
using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.View
{
    internal class ConsoleOperation
    {
        /// <summary>
        /// Displays the main menu and reads the user's choice.
        /// </summary>
        /// <returns>The selected menu option.</returns>
        public MainMenuOption ShowMainMenu()
        {
            Console.WriteLine("========================================================");
            Console.WriteLine("Expense Tracker Application");
            Console.WriteLine("Track Your Spending, Empower Your Savings!");
            Console.WriteLine("========================================================");
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("[A] Add Record");
            Console.WriteLine("[B] View Record");
            Console.WriteLine("[C] Search Record");
            Console.WriteLine("[D] Delete Record");
            Console.WriteLine("[E] Edit Record");
            Console.WriteLine("[F] Financial Summary");
            Console.WriteLine("[G] Exit");
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

        public AddMenuOption ShowAddMenu()
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("[A] Add Income");
            Console.WriteLine("[B] Add Expense");
            Console.WriteLine("[C] Back");

            string choice = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
            return choice switch
            {
                "A" => AddMenuOption.AddIncome,
                "B" => AddMenuOption.AddExpense,
                "C" => AddMenuOption.BackToMainMenu,
                _ => AddMenuOption.Invalid
            };
        }

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

        public string ReadRecordDate()
        {
            Console.Write($"Enter the date of the record in (DD/MM/YYYY): ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        public string ReadRecordAmount()
        {
            Console.Write($"Enter the amount of the record: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

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

        public string ReadRecordId(string action)
        {
            Console.Write($"Enter the record ID to {action}: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        public string ReadRecordDescription()
        {
            Console.Write($"Enter the description of the record (Optional): ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        public string ReadSearchTerm()
        {
            Console.Write($"Enter the date or amount or source/category: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// This method is used to show success message with Green color.
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        /// <param name="type">The message type (Success, Error or Info).</param>
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
        /// Displays the invalid error message with different fields.
        /// </summary>
        /// <param name="fieldName">The field name to be printed as an invalid message.</param>
        public void ShowInvalidMessage(string fieldName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Enter a valid {fieldName}.");
            Console.ResetColor();
        }

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

        public void DisplaySingleRecord(FinancialRecord record)
        {
            Console.WriteLine();
            var table = new ConsoleTable("Id", "Date", "Type", "Classification", "Amount", "Description");
            table.AddRow(record.Id, record.Date, record.Type, record.Classification, record.Amount, record.Description);
            table.Write(Format.MarkDown);
        }

        /// <summary>
        /// Clears the console.
        /// </summary>
        public void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Waits for a key press before clearing the console.
        /// </summary>
        public void ClearScreenWithKey()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.ResetColor();
        }

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
        /// <returns>True if the user wants to retry, otherwise false.</returns>
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
