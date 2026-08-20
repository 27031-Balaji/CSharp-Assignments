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
            Console.Write("\nTask 1 - Division operation\n\n");
            Console.Write("Enter the first number: ");
            int firstNumber = GetValidNumber();
            Console.Write("Enter the second number: ");
            int secondNumber = GetValidNumber();
            try
            {
                int result = firstNumber / secondNumber;
                Console.WriteLine($"The result is {result}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Division operation is impossible with the second number being zero!\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Base execution error.\n");
            }
            finally
            {
                Console.WriteLine("Division operation ended successfully.\n");
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
                if (int.TryParse(input, out number))
                {
                    isValidNumber = true;
                }
                else
                {
                    Console.WriteLine("Input must be a valid integer.");
                    Console.WriteLine("Enter again below.");
                }
            }

            return number;
        }
    }
}