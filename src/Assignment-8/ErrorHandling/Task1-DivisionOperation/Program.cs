namespace ErrorHandling
{
    internal class Program
    {
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
            catch (ArithmeticException)
            {
                Console.WriteLine("Invalid arithmetic operation.\n");
            }
            catch (SystemException)
            {
                Console.WriteLine("Invalid system operation.\n");
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