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
            Console.Write("\nTask 2 - Array access operation with division\n\n");
            Console.Write("Enter the array length: ");
            int lengthOfArray = GetValidPositiveNumber();

            int[] arr = new int[lengthOfArray];
            for (int i = 0; i < lengthOfArray; i++)
            {
                Console.Write($"Enter number ({i + 1}/{lengthOfArray}): ");
                arr[i] = GetValidNumber();
            }

            try
            {
                try
                {
                    Console.Write("Enter the position of the element to be the dividend: ");
                    int dividendIndex = GetValidNumber();
                    int elementInDividendIndex = arr[dividendIndex - 1];

                    Console.Write("Enter the position of the element to be the divisor: ");
                    int divisorIndex = GetValidNumber();
                    int elementInDivisorIndex = arr[divisorIndex - 1];

                    int result = elementInDividendIndex / elementInDivisorIndex;
                    Console.WriteLine($"The division of {elementInDividendIndex} and {elementInDivisorIndex} is {result}");
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw new IndexOutOfRangeException("Cannot access the element outside the array bounds.\n", ex);
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Division operation is impossible with the second number being zero!\n");
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Base execution error: {ex.Message}\n");
            }
            finally
            {
                Console.WriteLine("Array access operation finished.");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Prompts the user until they enter the right valid number.
        /// </summary>
        /// <returns>The number in the right format.</returns>
        public static int GetValidNumber()
        {
            int number = 0;
            bool isValidNumber = false;
            while (!isValidNumber)
            {
                string input = (Console.ReadLine() ?? string.Empty).Trim();
                if (!int.TryParse(input, out number))
                {
                    Console.WriteLine("Input must be a valid integer.");
                    Console.WriteLine("Enter again below.\n");
                    continue;
                }

                isValidNumber = true;
            }

            return number;
        }

        /// <summary>
        /// Prompts the user until they enter the right valid positive number greater than 0.
        /// </summary>
        /// <returns>The number in the right format.</returns>
        public static int GetValidPositiveNumber()
        {
            int number = 0;
            bool isValidNumber = false;
            while (!isValidNumber)
            {
                string input = (Console.ReadLine() ?? string.Empty).Trim();

                if (!int.TryParse(input, out number) || number <= 0)
                {
                    Console.WriteLine("Input must be a valid integer and greater than 0.");
                    Console.WriteLine("Enter again below.\n");
                    continue;
                }

                isValidNumber = true;
            }

            return number;
        }
    }
}