using MathLibrary;

namespace CalculatorApplication
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Write("Basic Calculator\n\n");
                Console.Write("1. Add\n");
                Console.Write("2. Subtract\n");
                Console.Write("3. Multiply\n");
                Console.Write("4. Divide\n");
                Console.Write("5. Exit\n");

                Console.Write("\nEnter your choice: ");
                string choice = (Console.ReadLine() ?? string.Empty).Trim();
                if (!int.TryParse(choice, out int menuChoice) || (menuChoice < 0 || menuChoice > 5))
                {
                    Console.WriteLine("Please enter a valid choice.\n");
                }

                switch (menuChoice)
                {
                    case 1:
                        AdditionOperation();
                        break;

                    case 2:
                        SubtractionOperation();
                        break;

                    case 3:
                        MultiplicationOperation();
                        break;

                    case 4:
                        DivisionOperation();
                        break;

                    case 5:
                        isRunning = false;
                        break;
                }

                if (isRunning)
                {
                    ClearScreenWithKey();
                }
            }
        }

        /// <summary>
        /// Handles the addition operation of the calculator.
        /// </summary>
        private static void AdditionOperation()
        {
            int firstNumber = GetNumber("first");
            int secondNumber = GetNumber("second");
            int result = MathUtils.Add(firstNumber, secondNumber);
            Console.Write($"\nAddition of {firstNumber} and {secondNumber} is {result}.\n");
        }

        /// <summary>
        /// Handles the subtraction operation of the calculator.
        /// </summary>
        private static void SubtractionOperation()
        {
            int firstNumber = GetNumber("first");
            int secondNumber = GetNumber("second");
            int result = MathUtils.Subtract(firstNumber, secondNumber);
            Console.Write($"\nSubtraction of {firstNumber} and {secondNumber} is {result}.\n");
        }

        /// <summary>
        /// Handles the multiplication operation of the calculator.
        /// </summary>
        private static void MultiplicationOperation()
        {
            int firstNumber = GetNumber("first");
            int secondNumber = GetNumber("second");
            int result = MathUtils.Multiply(firstNumber, secondNumber);
            Console.Write($"\nProduct of {firstNumber} and {secondNumber} is {result}.\n");
        }

        /// <summary>
        /// Handles the division operation of the calculator.
        /// </summary>
        /// <exception cref="DivideByZeroException">Exception that arises when the second number is given as zero by the user.</exception>
        private static void DivisionOperation()
        {
            int firstNumber = GetNumber("first");
            int secondNumber = GetNumber("second");
            try
            {
                double result = MathUtils.Divide(firstNumber, secondNumber);
                Console.Write($"\nQuotient of {firstNumber} and {secondNumber} is {result}.\n");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Helper used to get valid number input from the user.
        /// </summary>
        /// <param name="order">The order of the number. (Eg: First, Second).</param>
        /// <returns>The input number given by the user.</returns>
        private static int GetNumber(string order)
        {
            bool isValidNumber = false;
            int value = 0;

            while (!isValidNumber)
            {
                Console.Write($"Enter the {order} number: ");
                isValidNumber = int.TryParse((Console.ReadLine() ?? string.Empty).Trim(), out value);
                if (!isValidNumber)
                {
                    Console.Write($"Invalid {order} number. Please enter a valid number.\n");
                }
            }

            return value;
        }

        /// <summary>
        /// Waits for a keypress and clears the console.
        /// </summary>
        private static void ClearScreenWithKey()
        {
            Console.Write("\nPress any key to go back to the main menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}