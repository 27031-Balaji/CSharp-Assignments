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
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UniversalHandler);
            try
            {
                try
                {
                    Console.Write("\nTask 4 - Array access operation with division (Custom invalid input exception with global handler)\n\n");
                    Console.Write("Enter the array length: ");
                    int lengthOfArray = GetValidNumber();

                    int[] arr = new int[lengthOfArray];
                    for (int i = 0; i < lengthOfArray; i++)
                    {
                        Console.Write($"Enter number {i + 1} for the array: ");
                        arr[i] = GetValidNumber();
                    }

                    Console.Write("Enter the position of the element to be the dividend: ");
                    int dividendIndex = GetValidNumber();
                    int elementInDividendIndex = arr[dividendIndex - 1];
                    Console.WriteLine($"The array element found in position {dividendIndex} is {elementInDividendIndex}");

                    Console.Write("Enter the position of the element to be the divisor: ");
                    int divisorIndex = GetValidNumber();
                    int elementInDivisorIndex = arr[divisorIndex - 1];
                    Console.WriteLine($"The array element found in position {divisorIndex} is {elementInDivisorIndex}");

                    int result = elementInDividendIndex / elementInDivisorIndex;
                    Console.WriteLine($"The division of {elementInDividendIndex} and {elementInDivisorIndex} is {result}\n");
                }
                catch (InvalidUserInputException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException("Cannot access the element outside the array bounds.\n");
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Division operation is impossible with the second number being zero!\n");
                }
            }
            finally
            {
                Console.WriteLine("Array access with custom exception and global handler completed.\n");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Prompts the user to enter a valid number as the input.
        /// </summary>
        /// <returns>The valid number if they enter properly.</returns>
        /// <exception cref="InvalidUserInputException">Custom exception thrown when the user enters invalid number.</exception>
        public static int GetValidNumber()
        {
            int number = 0;
            string input = (Console.ReadLine() ?? string.Empty).Trim();
            try
            {
                number = int.Parse(input);
                return number;
            }
            catch
            {
                throw new InvalidUserInputException("Enter a valid number.\n");
            }
        }

        /// <summary>
        /// The UniversalHandler method is used to handle the event raised when the exception is not handled in the application domain.
        /// </summary>
        /// <param name="sender">The source of the unhandled exception event.</param>
        /// <param name="args">The arguments which contains the event data.</param>
        private static void UniversalHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("An unhandled exception occurred.");
            Console.WriteLine($"Type: {e.GetType().Name}");
            Console.WriteLine("Universal handler caught with message: " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
        }
    }
}