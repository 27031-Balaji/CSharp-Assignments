using System.Reflection;

namespace InspectAssemblyData
{
    /// <summary>
    /// Entry point of the Application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The method that executes when the application is run.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            string filepath = GetFilepath();
            Assembly file = Assembly.LoadFile(filepath);
            Type[] types = file.GetTypes();
            foreach (Type type in types)
            {
                Console.WriteLine($"Type: {type.FullName}");
                MethodInfo[] methods = type.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    Console.WriteLine($"Method: {method.Name}");
                }

                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    Console.WriteLine($"Property: {property.Name}");
                }

                FieldInfo[] fields = type.GetFields();
                foreach (FieldInfo field in fields)
                {
                    Console.WriteLine($"Field: {field.Name}");
                }

                EventInfo[] events = type.GetEvents();
                foreach (EventInfo eventInfo in events)
                {
                    Console.WriteLine($"Event: {eventInfo.Name}");
                }

                Console.WriteLine();
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Gets a valid filepath from the user input. It prompts the user until a valid filepath is provided.
        /// </summary>
        /// <returns>The valid file path.</returns>
        private static string GetFilepath()
        {
            Console.Write("Enter file path: ");
            string filepath = Console.ReadLine() ?? string.Empty;
            while (!IsValidFilepath(filepath))
            {
                Console.Write("Invalid filepath. Please enter a valid filepath: ");
                filepath = Console.ReadLine() ?? string.Empty;
            }

            return filepath;
        }

        /// <summary>
        /// Checks if the provided filepath is valid (not null, not empty, and the file exists).
        /// </summary>
        /// <param name="filepath">Filepath input provided by user.</param>
        /// <returns>True, if filepath is valid. else false.</returns>
        private static bool IsValidFilepath(string filepath)
        {
            return !string.IsNullOrWhiteSpace(filepath) && File.Exists(filepath);
        }
    }
}