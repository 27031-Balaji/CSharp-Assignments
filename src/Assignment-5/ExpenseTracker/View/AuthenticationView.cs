using ExpenseTracker.Enums;

namespace ExpenseTracker.View
{
    /// <summary>
    /// Console-based view responsible for displaying authentication-related UI and reading authentication input from the user.
    /// </summary>
    internal class AuthenticationView
    {
        /// <summary>
        /// Shows the authentication option menu and reads the user's choice.
        /// </summary>
        /// <returns>An enum value representing the user's selection.
        /// </returns>
        public AuthenticationOptionMenu ShowMenu()
        {
            Console.Write("========================================================\n");
            Console.Write("Expense Tracker Application\n");
            Console.Write("Track Your Spending, Empower Your Savings!\n");
            Console.Write("========================================================\n");
            Console.Write("[A] Login\n");
            Console.Write("[B] Sign Up\n");
            Console.Write("[C] Exit\n");
            Console.Write("\nEnter your choice: ");

            string input = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
            return input switch
            {
                "A" => AuthenticationOptionMenu.Login,
                "B" => AuthenticationOptionMenu.Signup,
                "C" => AuthenticationOptionMenu.Exit,
                _ => AuthenticationOptionMenu.Invalid,
            };
        }

        /// <summary>
        /// Prompts the user to enter a username.
        /// </summary>
        /// <returns>The entered username.</returns>
        public string ReadUserName()
        {
            Console.Write("Username: ");
            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Prompts the user to enter a password.
        /// </summary>
        /// <returns>The entered password.</returns>
        public string ReadUserPassword()
        {
            Console.Write("Password: ");
            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Displays a message to the user using the color that corresponds to the message type.
        /// </summary>
        /// <param name="message">The message text to display.</param>
        /// <param name="type">The message type that determines the display color.
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
        /// Shows a standard invalid input message for the specified field.
        /// </summary>
        /// <param name="fieldName">The invalid fieldname.</param>
        public void ShowInvalidMessage(string fieldName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Enter a valid {fieldName}.");
            Console.ResetColor();
        }

        /// <summary>
        /// Asks the user whether they want to retry the current operation.
        /// </summary>
        /// <returns>True if the user chooses to retry, otherwise false.</returns>
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

        /// <summary>
        /// Clears the console.
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
    }
}