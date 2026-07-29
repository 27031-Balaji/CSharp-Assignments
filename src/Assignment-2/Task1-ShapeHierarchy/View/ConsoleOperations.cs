namespace Task1.View
{
    /// <summary>
    /// Provides console input and output operations.
    /// </summary>
    internal class ConsoleOperations
    {
        /// <summary>
        /// Displays the welcome message.
        /// </summary>
        public void ShowWelcomeMessage()
        {
            Console.WriteLine("Welcome to Shape Calculator.");
        }

        /// <summary>
        /// Displays the shape menu and returns the user's choice.
        /// </summary>
        /// <returns>The selected option.</returns>
        public char ShowShapeMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Choose a Shape:");
            Console.WriteLine("[A] Rectangle");
            Console.WriteLine("[B] Circle");
            Console.Write("Enter your choice: ");

            return char.ToUpper(Console.ReadKey().KeyChar);
        }

        /// <summary>
        /// Reads the color from the user.
        /// </summary>
        /// <returns>The entered color.</returns>
        public string ReadColor()
        {
            Console.Write("\nEnter Color: ");
            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Reads a positive number from the user.
        /// </summary>
        /// <param name="dimension">The dimension name.</param>
        /// <returns>The entered value.</returns>
        public string ReadPositiveNumber(string dimension)
        {
            Console.Write($"Enter {dimension}: ");
            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Displays the operations menu and returns the user's choice.
        /// </summary>
        /// <returns>The selected option.</returns>
        public char ShowOperationMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Choose an Operation:");
            Console.WriteLine("[A] Calculate Area");
            Console.WriteLine("[B] Print Details");
            Console.WriteLine("[C] Exit");
            Console.Write("Enter your choice: ");

            char choice = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            return choice;
        }

        /// <summary>
        /// Displays the calculated area.
        /// </summary>
        /// <param name="area">The area.</param>
        public void ShowArea(double area)
        {
            Console.WriteLine($"Area: {area:F2}");
        }

        /// <summary>
        /// Displays the shape details.
        /// </summary>
        /// <param name="details">The shape details.</param>
        public void ShowDetails(string details)
        {
            Console.WriteLine(details);
        }

        /// <summary>
        /// Displays a message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Waits for a key before closing.
        /// </summary>
        public void FlushScreenWithKey()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}