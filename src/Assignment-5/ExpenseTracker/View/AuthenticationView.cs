using ExpenseTracker.Enums;
using ExpenseTracker.Helper;
using System.Text;

namespace ExpenseTracker.View
{
    /// <summary>
    /// Console-based view responsible for displaying authentication-related UI and reading authentication input from the user.
    /// </summary>
    internal class AuthenticationView
    {
        /// <summary>
        /// Displays the header for the application.
        /// </summary>
        public void ShowHeader()
        {
            Console.Write("========================================================\n");
            Console.Write("Expense Tracker Application\n");
            Console.Write("Track Your Spending, Empower Your Savings!\n");
            Console.Write("========================================================\n");
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
                Console.WriteLine($"{i + 1}. {this.FormatEnumDisplayName(values[i].ToString())}");
            }

            Console.Write("\nEnter choice: ");
            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Prompts the user to enter an input.
        /// </summary>
        /// <param name="prompt">The prompt displayed to the user.</param>
        /// <returns>The trimmed user input.</returns>
        public string GetInput(string prompt)
        {
            Console.Write(prompt);
            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Prompts the user to enter a password.
        /// </summary>
        /// <returns>The entered password.</returns>
        public string GetUserPassword()
        {
            Console.Write("Password: ");
            return this.MaskPassword();
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

        /// <summary>
        /// Reads a password from the console while masking the entered characters.
        /// </summary>
        /// <returns>The password entered by the user as a string.</returns>
        private string MaskPassword()
        {
            // Stores the actual password entered by the user.
            StringBuilder originalPassword = new StringBuilder();

            while (true)
            {
                // Read a key without displaying it on the console.
                ConsoleKeyInfo key = Console.ReadKey(true);

                /**
                 * If the Enter key is pressed, the user has finished
                 * entering the password, so exit the loop.
                 */
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }

                /**
                 * If Backspace is pressed and the password contains
                 * characters, remove the last character from the
                 * password and erase the corresponding '*' from
                 * the console.
                 */
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (originalPassword.Length > 0)
                    {
                        originalPassword.Length--;
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    /**
                     * For any other key, append the character to the
                     * password and display '*' instead of the actual
                     * character to mask the input.
                     */
                    originalPassword.Append(key.KeyChar);
                    Console.Write("*");
                }
            }

            // Move the cursor to the next line after password entry.
            Console.WriteLine();

            // Return the actual password entered by the user.
            return originalPassword.ToString();
        }

        /// <summary>
        /// Converts a PascalCase enum value into a human-readable string.
        /// </summary>
        /// <param name="value">The enum value to format.</param>
        /// <returns>A string with spaces inserted before uppercase letters.</returns>
        private string FormatEnumDisplayName(string value)
        {
            StringBuilder formattedValue = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                /**
                 * Insert a space before an uppercase letter
                 * when it is not the first character.
                 */
                if (i > 0 && char.IsUpper(value[i]))
                {
                    formattedValue.Append(' ');
                }

                formattedValue.Append(value[i]);
            }

            return formattedValue.ToString();
        }
    }
}