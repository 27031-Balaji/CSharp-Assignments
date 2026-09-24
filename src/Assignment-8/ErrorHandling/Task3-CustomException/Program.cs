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
            try
            {
                RunDivisionOperation();
            }
            catch (InvalidUserInputException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Division operation is impossible with the second number being zero!\n", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Base execution error: {ex.Message}\n");
            }
            finally
            {
                Console.WriteLine("Array access operation with custom exception ended.\n");
                Console.ReadKey();
            }
        }

        private static void RunDivisionOperation()
        {
            Console.Write("\nTask 3 - Array access operation with division (Custom invalid input exception)\n\n");
            int[] array = ReadArray();
            int dividend = GetArrayElement(array, "Enter the position of the element to be the dividend: ");
            int divisor = GetArrayElement(array, "Enter the position of the element to be the divisor: ");
            int result = Divide(dividend, divisor);
            Console.WriteLine($"The division of {dividend} and {divisor} is {result}\n");
        }

        private static int[] ReadArray()
        {
            Console.Write("Enter the array length: ");
            int lengthOfArray = GetValidNumber();

            if (lengthOfArray <= 0)
            {
                throw new InvalidUserInputException("Array length must be greater than zero.");
            }

            int[] array = new int[lengthOfArray];
            for (int i = 0; i < lengthOfArray; i++)
            {
                Console.Write($"Enter number ({i + 1}/{lengthOfArray}): ");
                array[i] = GetValidNumber();
            }

            return array;
        }

        private static int GetArrayElement(int[] array, string message)
        {
            Console.Write(message);
            int position = GetValidNumber();
            if (position < 1 || position > array.Length)
            {
                throw new IndexOutOfRangeException("Cannot access the element outside the array bounds.");
            }

            int value = array[position - 1];
            Console.WriteLine($"The array element found in position {position} is {value}");
            return value;
        }

        private static int Divide(int dividend, int divisor)
        {
            return dividend / divisor;
        }

        /// <summary>
        /// Prompts the user to enter a valid number as the input.
        /// </summary>
        /// <returns>The valid number if they enter properly.</returns>
        /// <exception cref="InvalidUserInputException">Custom exception thrown when the user enters an invalid number.</exception>
        private static int GetValidNumber()
        {
            string input = (Console.ReadLine() ?? string.Empty).Trim();

            if (!int.TryParse(input, out int number))
            {
                throw new InvalidUserInputException($"'{input}' is not a valid integer.");
            }

            return number;
        }
    }
}