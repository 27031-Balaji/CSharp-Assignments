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
        public string ShowMainMenu()
        {
            Console.Write("Inventory Management System\n");
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

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        public string AddMenu()
        {
            Console.Write("\nSelect an option to add income or expense: \n");
            Console.Write("[A] Add Income\n");
            Console.Write("[B] Add Expense\n");
            Console.Write("[C] Back to Main Menu\n");
            Console.Write("\nEnter your choice: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        public string ViewMenu()
        {
            Console.Write("\nSelect an option to view: \n");
            Console.Write("[A] View All Records\n");
            Console.Write("[B] View All Incomes\n");
            Console.Write("[C] View All Expenses\n");
            Console.Write("[D] Back to Main Menu\n");
            Console.Write("\nEnter your choice: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        public string ReadRecordDate(string action)
        {
            Console.Write($"Enter the date of the record in (DD/MM/YYYY) to {action}: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        public string ReadRecordAmount(string action)
        {
            Console.Write($"Enter the amount of the record to {action}: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        public string ReadRecordSource(string action)
        {
            Console.Write($"Enter the source of the income to {action}: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        public string ReadRecordCategory(string action)
        {
            Console.Write($"Enter the category of the expense to {action}: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        public string ReadRecordDescription(string action)
        {
            Console.Write($"Enter the description of the record to {action} (Optional): ");
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

        public void DisplayRecords(List<FinancialRecord> records)
        {
            var table = new ConsoleTable("Id", "Date", "Type", "Classification", "Amount", "Description");
            foreach (FinancialRecord record in records)
            {
                table.AddRow(record.Id, record.Date, record.Type, record.Classification, record.Amount, record.Description);
            }

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
