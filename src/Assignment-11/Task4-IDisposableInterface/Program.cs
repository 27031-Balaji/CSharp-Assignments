using IDisposableInterface.FileOperations;

namespace IDisposableInterface
{
    internal class Program
    {
        private const string FilePath = "sample.txt";

        public static void Main(string[] args)
        {
            Console.Write("Enter contents to be written onto the file: ");
            using (FileWriter fileWriter = new FileWriter(FilePath))
            {
                string contents = (Console.ReadLine() ?? string.Empty).Trim();
                fileWriter.WriteIntoFile(contents);
                Console.WriteLine("Contents written successfully.\n");
            }

            using (FileReader fileReader = new FileReader(FilePath))
            {
                Console.WriteLine("\nThe contents from the file:\n");
                List<string> lines = fileReader.ReadFromFile();
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }

                Console.WriteLine("Contents read successfully.\n");
            }

            Console.ReadKey();
        }
    }
}