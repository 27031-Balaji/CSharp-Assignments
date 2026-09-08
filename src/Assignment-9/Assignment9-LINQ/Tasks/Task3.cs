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
            Console.WriteLine(string.Join(", ", numbers));

            IEnumerable<int> distinctNumbers = numbers.Distinct();

            if (distinctNumbers.Count() >= 2)
            {
                int highest = distinctNumbers.Max();

                int secondHighest = distinctNumbers
                    .Where(number => number < highest)
                    .Max();

                Console.WriteLine($"\nSecond Highest Number: {secondHighest}");
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