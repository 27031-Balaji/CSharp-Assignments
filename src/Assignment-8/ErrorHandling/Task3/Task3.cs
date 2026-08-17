namespace ErrorHandling.Task3
{
    internal class Task3
    {
        public void Run()
        {
            Console.Write("\nTask 3 - Array access operation with custom exception\n\n");
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
                if (indexToAccess < 0 || indexToAccess >= arr.Length)
                {
                    throw new InvalidUserInputException("Can't access the element out of the array range.\n");
                }

                int elementInArray = arr[indexToAccess];
                Console.WriteLine($"The array element found in {indexToAccess} is {elementInArray}");
            }
            catch (InvalidUserInputException ex)
            {
                Console.WriteLine(ex.Message);
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
                Console.WriteLine("Array access operation ended successfully.\n");
            }
        }

        public int GetValidNumber()
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