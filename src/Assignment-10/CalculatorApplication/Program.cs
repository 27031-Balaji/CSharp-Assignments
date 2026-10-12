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
                int firstNumber = GetNumber("first");
                int secondNumber = GetNumber("second");
                AdditionOperation(firstNumber, secondNumber);
                SubtractionOperation(firstNumber, secondNumber);
                MultiplicationOperation(firstNumber, secondNumber);
                DivisionOperation(firstNumber, secondNumber);
                ClearScreenWithKey();
            }
        }

        /// <summary>
        /// Handles the addition operation of the calculator.
        /// <param name="firstNumber">The first number.</param>
        /// <param name="secondNumber">The second number.</param>
        /// </summary>
        private static void AdditionOperation(int firstNumber, int secondNumber)
        {
            double result = MathUtils.Add(firstNumber, secondNumber);
            Console.Write($"\nAddition of {firstNumber} and {secondNumber} is {result:F2}\n");
        }

        /// <summary>
        /// Handles the subtraction operation of the calculator.
        /// <param name="firstNumber">The first number.</param>
        /// <param name="secondNumber">The second number.</param>
        /// </summary>
        private static void SubtractionOperation(int firstNumber, int secondNumber)
        {
            double result = MathUtils.Subtract(firstNumber, secondNumber);
            Console.Write($"Subtraction of {firstNumber} and {secondNumber} is {result:F2}\n");
        }

        /// <summary>
        /// Handles the multiplication operation of the calculator.
        /// <param name="firstNumber">The first number.</param>
        /// <param name="secondNumber">The second number.</param>
        /// </summary>
        private static void MultiplicationOperation(int firstNumber, int secondNumber)
        {
            double result = MathUtils.Multiply(firstNumber, secondNumber);
            Console.Write($"Product of {firstNumber} and {secondNumber} is {result:F2}\n");
        }

        /// <summary>
        /// Handles the division operation of the calculator.
        /// </summary>
        /// <param name="firstNumber">The first number.</param>
        /// <param name="secondNumber">The second number.</param>
        /// <exception cref="DivideByZeroException">Exception that arises when the second number is given as zero by the user.</exception>
        private static void DivisionOperation(int firstNumber, int secondNumber)
        {
            try
            {
                double result = MathUtils.Divide(firstNumber, secondNumber);
                Console.Write($"Quotient of {firstNumber} and {secondNumber} is {result:F2}\n");
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