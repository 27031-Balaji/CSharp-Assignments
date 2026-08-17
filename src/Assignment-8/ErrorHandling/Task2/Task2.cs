namespace ErrorHandling.Task2
{
    internal class Task2
    {
        public void Run()
        {
            Console.Write("\nTask 2 - Array access operation\n\n");
            Console.Write("Enter the array length: ");
            int lengthOfArray = this.GetValidNumber();

            int[] arr = new int[lengthOfArray];
            for (int i = 0; i < lengthOfArray; i++)
            {
                Console.Write($"Enter number {i + 1} for the array: ");
                arr[i] = this.GetValidNumber();
            }

            try
            {
                Console.Write("Enter the index of the element you want to access: ");
                int indexToAccess = this.GetValidNumber();
                int elementInArray = arr[indexToAccess];
                Console.WriteLine($"The array element found in {indexToAccess} is {elementInArray}");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("The index is out of bounds!\n");
            }
            catch (SystemException)
            {
                Console.WriteLine("Invalid system operation.\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Execution error.\n");
            }
            finally
            {
                Console.WriteLine("Array access operation ended successfully.\n");
            }
        }

        public int GetValidNumber()
        {
            int number = 0;
            bool isValidNumber = false;
            while (!isValidNumber)
            {
                string input = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
                if (int.TryParse(input, out number))
                {
                    isValidNumber = true;
                }
                else
                {
                    Console.WriteLine("Invalid number. Enter again below.");
                }
            }

            return number;
        }
    }
}
