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

            int[] numbers = { 10, 20, 30, 40, 50, 10, 30, 20, 50, 10, 40 };
            Console.WriteLine("Numbers:");
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                Console.Write(numbers[i] + ", ");
            }

            Console.WriteLine(numbers[numbers.Length - 1]);

            var distinctNumbers = numbers
                .Distinct()
                .OrderByDescending(number => number)
                .ToArray();

            if (distinctNumbers.Length >= 2)
            {
                Console.WriteLine($"\nSecond Highest Number: {distinctNumbers[1]}");
            }
            else
            {
                Console.WriteLine("\nSecond Highest Number: Not available");
            }

            int target = 60;
            var pairs = numbers
                .SelectMany(
                    (number, index) => numbers
                        .Skip(index + 1)
                        .Where(otherNumber => number + otherNumber == target)
                        .Select(otherNumber => new
                        {
                            Number1 = number,
                            Number2 = otherNumber,
                        }))
                .Distinct()
                .ToList();

            Console.WriteLine($"\nPairs That Add Up To {target}:\n");

            if (pairs.Any())
            {
                foreach (var pair in pairs)
                {
                    Console.WriteLine($"{pair.Number1} + {pair.Number2} = {target}");
                }
            }
            else
            {
                Console.WriteLine("No pairs found.");
            }
        }
    }
}