using ExpenseTracker.Enums;
using ExpenseTracker.Helper;
using System.Text;

namespace ExpenseTracker.View
{
    /// <summary>
    /// Provides a base class for console-based user interaction.
    /// </summary>
    internal abstract class ConsoleView
    {
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
        /// <returns>The entered choice string.</returns>
        public string GetEnumOption<T>()
            where T : struct, Enum
        {
            Console.WriteLine("Select an option:");
            T[] values = Enum.GetValues<T>();
            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {this.FormatEnumDisplayName(values[i].ToString())}");
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
        /// Prompts the user to confirm any action from the user.
        /// </summary>
        /// <param name="action">The action to be asked to perform.</param>
        /// <returns>True if the user confirms the action, otherwise False.</returns>
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
        /// Converts a PascalCase enum value into a human-readable string.
        /// </summary>
        /// <param name="value">The enum value to format.</param>
        /// <returns>A string with spaces inserted before uppercase letters.</returns>
        protected string FormatEnumDisplayName(string value)
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