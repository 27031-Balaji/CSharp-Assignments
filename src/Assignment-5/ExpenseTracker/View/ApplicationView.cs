using ConsoleTables;
using ExpenseTracker.Enums;
using ExpenseTracker.Helper;
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
        /// Prompts the user to enter an input.
        /// </summary>
        /// <param name="prompt">The prompt displayed to the console.</param>
        /// <returns>The trimmed input entered by the user.</returns>
        public string GetInput(string prompt)
        {
            Console.Write(prompt);
            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Displays all values of the specified enum type and returns the user's choice.
        /// </summary>
        /// <typeparam name="T">The enum type to display.</typeparam>
        /// <param name="title">The title to display before the options.</param>
        /// <returns>The entered choice string.</returns>
        public string GetEnumOption<T>()
            where T : struct, Enum
        {
            Console.WriteLine("Select an option: ");
            T[] values = Enum.GetValues<T>();

            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {values[i]}");
            }

            Console.Write("\nEnter choice: ");
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
        /// Displays a message indicating the result of an operation based on success or failure.
        /// </summary>
        /// <param name="isSuccessful">True if the operation was successful, otherwise false.</param>
        /// <param name="successMessage">The message to display when the operation is successful.</param>
        /// <param name="failureMessage">The message to display when the operation fails.</param>
        public void ShowOperationResult(bool isSuccessful, string successMessage, string failureMessage)
        {
            this.ShowMessage(
                isSuccessful ? successMessage : failureMessage,
                isSuccessful ? MessageType.Success : MessageType.Error);
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
                Console.Write(string.Format(PromptMessages.ConfirmAction, action));
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