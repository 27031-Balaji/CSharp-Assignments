namespace IEnumerableAndIReadOnlyDictionary
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            // 6.1 - Implement a method that uses IEnumerable<int> as a parameter
            try
            {
                List<int> integerList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                Console.WriteLine($"List sum is: {SumOfElements(integerList)}");

                int[] arrayList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                Console.WriteLine($"Array sum is: {SumOfElements(arrayList)}");

                Queue<int> queueList = new Queue<int>();
                queueList.Enqueue(1);
                queueList.Enqueue(2);
                queueList.Enqueue(3);
                queueList.Enqueue(4);
                queueList.Enqueue(5);
                Console.WriteLine($"Queue sum is: {SumOfElements(queueList)}{Environment.NewLine}");
            }
            catch (OverflowException)
            {
                Console.WriteLine("The calculated sum exceeded the maximum value of integer.");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("The list/array/queue is null.");
            }

            // 6.2 - Understanding IReadOnlyDictionary
            IReadOnlyDictionary<string, int> studentGrades = GenerateDictionary();
            Console.WriteLine($"The students and their grades: {Environment.NewLine}");
            PrintDictionary(studentGrades);

            Console.ReadKey();
        }

        /// <summary>
        /// Takes a list of elements and computes their sum.
        /// </summary>
        /// <param name="list">The list of elements to be computed.</param>
        /// <returns>The sum of elements in the list.</returns>
        public static int SumOfElements(IEnumerable<int> list)
        {
            return list.Sum();
        }

        /// <summary>
        /// Creates a read-only dictionary mapping student names to their grades.
        /// </summary>
        /// <returns>An IReadOnlyDictionary containing the student-grade dictionary.</returns>
        public static IReadOnlyDictionary<string, int> GenerateDictionary()
        {
            Dictionary<string, int> studentGrades = new Dictionary<string, int>();
            studentGrades.Add("Lakshmi", 99);
            studentGrades.Add("Vishnu", 89);
            studentGrades.Add("Tarrun", 69);
            studentGrades.Add("Sowndhar", 80);
            studentGrades.Add("Dineshbalaji", 50);

            return studentGrades;
        }

        /// <summary>
        /// Prints each key and value in the specified dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary whose key-value pairs are to be printed.</param>
        public static void PrintDictionary(IReadOnlyDictionary<string, int> dictionary)
        {
            foreach (KeyValuePair<string, int> pair in dictionary)
            {
                Console.WriteLine($"{pair.Key} : {pair.Value}");
            }
        }

        /// <summary>
        /// Modifies the contents of the read-only dictionary.
        /// Throws an error because IReadOnlyDictionary is immutable.
        /// </summary>
        public static void ModifyDictionary()
        {
            IReadOnlyDictionary<string, int> dictionary = GenerateDictionary();

            // dictionary["Apple"] = 10; - Throws an error specifying that the dictionary is read-only.
        }
    }
}