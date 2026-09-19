using ConsoleTables;
using ExpenseTracker.Enums;
using ExpenseTracker.Helper;
using ExpenseTracker.Model;
using System.Text;

namespace ExpenseTracker.View
{
    /// <summary>
    /// Handles console input and output for the application.
    /// </summary>
    internal class ApplicationView : ConsoleView
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
        /// Shows a financial summary for the given date range.
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
    }
}