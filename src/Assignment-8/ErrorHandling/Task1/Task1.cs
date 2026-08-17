namespace ErrorHandling.Task1
{
    internal class Task1
    {
        public void Run()
        {
            Console.Write("\nTask 1 - Division operation\n\n");
            int firstNumber = this.GetValidNumber(1);
            int secondNumber = this.GetValidNumber(2);
            try
            {
                int result = firstNumber / secondNumber;
                Console.WriteLine($"The result is {result}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Division operation is impossible with the second number being zero!\n");
            }
            finally
            {
                Console.WriteLine("Division operation ended successfully.\n");
            }
        }

        public int GetValidNumber(int order)
        {
            int number = 0;
            bool isValidNumber = false;
            while (!isValidNumber)
            {
                Console.Write($"Enter number {order}: ");
                string input = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
                if (int.TryParse(input, out number))
                {
                    isValidNumber = true;
                }
                else
                {
                    Console.WriteLine("Enter a valid number.");
                }
            }

            return number;
        }
    }
}
