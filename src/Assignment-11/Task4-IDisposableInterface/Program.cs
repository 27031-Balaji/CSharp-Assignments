using IDisposableInterface.FileOperations;

namespace IDisposableInterface
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        private const string WithoutDisposeFilePath = "sample1.txt";
        private const string WithDisposeFilePath = "sample2.txt";

        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            WithoutDispose();
            WithDispose();
            Console.ReadKey();
        }

        /// <summary>
        /// This method is used to implement the file operations without calling dispose, causing an IO exception.
        /// </summary>
        public static void WithoutDispose()
        {
            Console.WriteLine("Without dispose\n");
            FileWriter fileWriter = new FileWriter(WithoutDisposeFilePath);
            Console.Write("Enter contents to be written onto the file: ");
            string contents = (Console.ReadLine() ?? string.Empty).Trim();
            fileWriter.WriteIntoFile(contents);
            Console.WriteLine("Contents written successfully.\n");

            try
            {
                FileReader fileReader = new FileReader(WithoutDisposeFilePath);
                Console.WriteLine("\nThe contents from the file:\n");
                List<string> lines = fileReader.ReadFromFile();
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }

                Console.WriteLine("Contents read successfully.\n");
            }
            catch (IOException)
            {
                Console.WriteLine("File is currently in use and is blocked.\n");
            }
        }

        /// <summary>
        /// This method is used to implement the file operations with the "using" keyword, which automatically called Dispose().
        /// </summary>
        public static void WithDispose()
        {
            Console.WriteLine("With dispose\n");
            Console.Write("Enter contents to be written onto the file: ");
            using (FileWriter fileWriter = new FileWriter(WithDisposeFilePath))
            {
                string contents = (Console.ReadLine() ?? string.Empty).Trim();
                fileWriter.WriteIntoFile(contents);
                Console.WriteLine("Contents written successfully.\n");
            }

            using (FileReader fileReader = new FileReader(WithDisposeFilePath))
            {
                Console.WriteLine("\nThe contents from the file:\n");
                List<string> lines = fileReader.ReadFromFile();
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }

                Console.WriteLine("Contents read successfully.\n");
            }
        }
    }
}