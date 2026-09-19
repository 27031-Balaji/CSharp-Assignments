using ExpenseTracker.Enums;
using ExpenseTracker.Helper;
using System.Text;

namespace ExpenseTracker.View
{
    /// <summary>
    /// Console-based view responsible for displaying authentication-related UI and reading authentication input from the user.
    /// </summary>
    internal class AuthenticationView : ConsoleView
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
        /// Prompts the user to enter a password and returns the entered value.
        /// </summary>
        /// <returns>The entered password.</returns>
        public string GetUserPassword()
        {
            Console.Write("Password: ");
            return this.MaskPassword();
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
    }
}