namespace Assignment9.Tasks
{
    /// <summary>
    /// Implements the Task 3 of the LINQ assignment.
    /// </summary>
    public static class Task3
    {
        /// <summary>
        /// The Run method runs the program with the LINQ query.
        /// </summary>
        public static void Run()
        {
            Console.WriteLine("\n========== TASK 3 ==========\n");

            int[] numbers = { 10, 15, 20, 30, 40, 50, 10, 30, 20, 50, 10, 40 };
            Console.WriteLine("Numbers:");
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                Console.Write(numbers[i] + ", ");
            }

            Console.WriteLine(numbers[numbers.Length - 1]);

            IEnumerable<int> distinctNumbers = numbers.Distinct();

            if (distinctNumbers.Count() >= 2)
            {
                int secondHighestNumber = distinctNumbers
                                            .OrderByDescending(number => number)
                                            .Skip(1)
                                            .First();
                Console.WriteLine($"\nSecond Highest Number: {secondHighestNumber}");
            }
            else
            {
                Console.WriteLine("\nSecond Highest Number: Not available");
            }

            int target = 60;
            IEnumerable<(int FirstNumber, int SecondNumber)> pairs = numbers
                .SelectMany(
                    (firstNumber, index) => numbers
                        .Skip(index + 1)
                        .Where(secondNumber => firstNumber + secondNumber == target)
                        .Select(secondNumber => (firstNumber, secondNumber)))
                .Distinct();

            Console.WriteLine($"\nPairs That Add Up To {target}:\n");

            if (pairs.Any())
            {
                foreach ((int firstNumber, int secondNumber) in pairs)
                {
                    Console.WriteLine($"{firstNumber} + {secondNumber} = {target}");
                }
            }
            else
            {
                Console.WriteLine("No pairs found.");
            }
        }
    }
}