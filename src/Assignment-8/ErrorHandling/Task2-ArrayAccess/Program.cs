namespace ErrorHandling
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("\nTask 2 - Array access operation with division\n\n");
            Console.Write("Enter the array length: ");
            int lengthOfArray = GetValidNumber();

            int[] arr = new int[lengthOfArray];
            for (int i = 0; i < lengthOfArray; i++)
            {
                Console.Write($"Enter number {i + 1} for the array: ");
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
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException("Cannot access the element outside the array bounds.\n");
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Division operation is impossible with the second number being zero!\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Array access operation ended successfully.");
                Console.ReadKey();
            }
        }

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