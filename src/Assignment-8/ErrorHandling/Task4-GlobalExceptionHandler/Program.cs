using ErrorHandling.CustomException;

namespace ErrorHandling
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UniversalHandler;

            try
            {
                RunArrayDivisionOperation();
            }
            catch (InvalidUserInputException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Division operation is impossible with the second number being zero!\n");
            }
            finally
            {
                Console.WriteLine("Array access with custom exception and global handler completed.\n");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Runs the complete array division workflow.
        /// </summary>
        private static void RunArrayDivisionOperation()
        {
            Console.Write("\nTask 4 - Array access operation with division (Custom invalid input exception with global handler)\n\n");
            int[] numbers = ReadArray();
            int dividend = GetArrayElement(numbers, "Enter the position of the element to be the dividend: ");
            int divisor = GetArrayElement(numbers, "Enter the position of the element to be the divisor: ");
            int result = Divide(dividend, divisor);
            Console.WriteLine($"The division of {dividend} and {divisor} is {result}\n");

            CauseUnhandledException();
        }

        /// <summary>
        /// Reads the array from the user.
        /// </summary>
        /// <returns>The populated array.</returns>
        private static int[] ReadArray()
        {
            Console.Write("Enter the array length: ");
            int length = GetValidNumber();
            if (length <= 0)
            {
                throw new InvalidUserInputException("Array length must be greater than zero.");
            }

            int[] numbers = new int[length];
            for (int i = 0; i < length; i++)
            {
                Console.Write($"Enter number ({i + 1}/{length}): ");
                numbers[i] = GetValidNumber();
            }

            return numbers;
        }

        /// <summary>
        /// Reads an array position and returns the corresponding element.
        /// </summary>
        /// <param name="numbers">The array.</param>
        /// <param name="message">The prompt message.</param>
        /// <returns>The element at the chosen position.</returns>
        private static int GetArrayElement(int[] numbers, string message)
        {
            Console.Write(message);
            int position = GetValidNumber();
            if (position < 1 || position > numbers.Length)
            {
                throw new InvalidUserInputException($"Position must be between 1 and {numbers.Length}.");
            }

            int value = numbers[position - 1];
            Console.WriteLine($"The array element found in position {position} is {value}");
            return value;
        }

        /// <summary>
        /// Divides two numbers.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The division result.</returns>
        private static int Divide(int dividend, int divisor)
        {
            return dividend / divisor;
        }

        /// <summary>
        /// Prompts the user to enter a valid integer.
        /// </summary>
        /// <returns>The valid integer.</returns>
        /// <exception cref="InvalidUserInputException">Thrown when the input is not a valid integer.</exception>
        private static int GetValidNumber()
        {
            string input = (Console.ReadLine() ?? string.Empty).Trim();
            if (!int.TryParse(input, out int number))
            {
                throw new InvalidUserInputException($"'{input}' is not a valid integer.");
            }

            return number;
        }

        /// <summary>
        /// Handles unhandled exceptions in the application domain.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">Event data.</param>
        private static void UniversalHandler(object sender, UnhandledExceptionEventArgs args)
        {
            if (args.ExceptionObject is Exception exception)
            {
                Console.WriteLine("\n--- GLOBAL EXCEPTION HANDLER ---");
                Console.WriteLine($"Type: {exception.GetType().Name}");
                Console.WriteLine($"Message: {exception.Message}");
                Console.WriteLine($"\nRuntime terminating: {args.IsTerminating}");
            }
            else
            {
                Console.WriteLine("\n--- GLOBAL EXCEPTION HANDLER ---");
                Console.WriteLine("An unknown non-exception object was thrown.");
                Console.WriteLine($"\nRuntime terminating: {args.IsTerminating}");
            }
        }

        /// <summary>
        /// Intentionally throws an unhandled exception.
        /// Used to demonstrate the global exception handler.
        /// </summary>
        private static void CauseUnhandledException()
        {
            MethodA();
        }

        private static void MethodA()
        {
            MethodB();
        }

        private static void MethodB()
        {
            MethodC();
        }

        private static void MethodC()
        {
            throw new InvalidOperationException("This exception was intentionally left unhandled.");
        }
    }
}