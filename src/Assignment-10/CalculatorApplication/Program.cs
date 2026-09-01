using MathLibrary;

namespace CalculatorApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MathUtils mathUtils = new MathUtils();
            bool isRunning = true;
            while (isRunning)
            {
                Console.Write("Basic Calculator\n\n");
                Console.Write("[A] Add\n");
                Console.Write("[B] Subtract\n");
                Console.Write("[C] Multiply\n");
                Console.Write("[D] Divide\n");
                Console.Write("[E] Exit\n");
                Console.Write("\nEnter your choice: ");
                string choice = (Console.ReadLine() ?? string.Empty).Trim();

                switch (choice)
                {
                    case "A":
                        AdditionOperation(mathUtils);
                        ClearScreenWithKey();
                        break;

                    case "B":
                        SubtractionOperation(mathUtils);
                        ClearScreenWithKey();
                        break;

                    case "C":
                        MultiplicationOperation(mathUtils);
                        ClearScreenWithKey();
                        break;

                    case "D":
                        DivisionOperation(mathUtils);
                        ClearScreenWithKey();
                        break;

                    case "E":
                        isRunning = false;
                        break;

                    default:
                        Console.Write("Invalid option. Please enter a valid option.\n\n");
                        break;
                }
            }
        }

        private static void AdditionOperation(MathUtils mathUtils)
        {
            int firstNumber = GetNumber("first");
            int secondNumber = GetNumber("second");
            int result = mathUtils.Add(firstNumber, secondNumber);
            Console.Write($"\nAddition of {firstNumber} and {secondNumber} is {result}.\n");
        }

        private static void SubtractionOperation(MathUtils mathUtils)
        {
            int firstNumber = GetNumber("first");
            int secondNumber = GetNumber("second");
            int result = mathUtils.Subtract(firstNumber, secondNumber);
            Console.Write($"\nSubtraction of {firstNumber} and {secondNumber} is {result}.\n");
        }

        private static void MultiplicationOperation(MathUtils mathUtils)
        {
            int firstNumber = GetNumber("first");
            int secondNumber = GetNumber("second");
            int result = mathUtils.Multiply(firstNumber, secondNumber);
            Console.Write($"\nProduct of {firstNumber} and {secondNumber} is {result}.\n");
        }

        private static void DivisionOperation(MathUtils mathUtils)
        {
            int firstNumber = GetNumber("first");
            int secondNumber = GetNumber("second");
            try
            {
                double result = mathUtils.Divide(firstNumber, secondNumber);
                Console.Write($"\nQuotient of {firstNumber} and {secondNumber} is {result}.\n");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static int GetNumber(string order)
        {
            bool isValidNumber = false;
            int value = 0;

            while (!isValidNumber)
            {
                Console.Write($"Enter the {order} number: ");
                isValidNumber = int.TryParse(Console.ReadLine() !.Trim(), out value);
                if (!isValidNumber)
                {
                    Console.Write($"Invalid {order} number. Please enter a valid number.\n");
                }
            }

            return value;
        }

        private static void ClearScreenWithKey()
        {
            Console.Write("\nPress any key to go back to the main menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}