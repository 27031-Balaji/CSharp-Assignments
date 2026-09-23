namespace Task2
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
            // 2.1 - Create a stack of characters
            Stack<char> stack = new Stack<char>();

            // 2.2 - Push each character of a given string onto the stack
            Console.Write("Enter a string to be reversed: ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            foreach (char ch in input)
            {
                stack.Push(ch);
            }

            // 2.3 - Pop each character and append it to a new string
            char[] reversedInput = new char[input.Length];
            int count = 0;
            while (stack.Count > 0)
            {
                reversedInput[count] = stack.Pop();
                count++;
            }

            // 2.4 - Display the original and the reversed string
            Console.WriteLine($"The original string: {input}");
            Console.WriteLine($"The reversed string using the stack: {new string(reversedInput)}");

            Console.ReadKey();
        }
    }
}