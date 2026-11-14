using System.Reflection;

namespace InspectAssemblyData
{
    /// <summary>
    /// The entry point of the application.
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
            Assembly assembly = Assembly.LoadFile(filepath);
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                Console.WriteLine($"Type: {type.FullName}");
                Console.WriteLine(new string('-', 50));
                PrintMethods(type);
                PrintProperties(type);
                PrintFields(type);
                PrintEvents(type);
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Prints the methods that are in the assembly.
        /// </summary>
        /// <param name="type">The type of the assembly used to find the </param>
        private static void PrintMethods(Type type)
        {
            Console.WriteLine("\nMethods:");
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
            if (methods.Length == 0)
            {
                Console.WriteLine("  No methods found.");
                return;
            }

            foreach (MethodInfo method in methods)
            {
                Console.WriteLine($"  {method.Name}");
            }
        }

        private static void PrintProperties(Type type)
        {
            Console.WriteLine("\nProperties:");
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
            if (properties.Length == 0)
            {
                Console.WriteLine("  No properties found.");
                return;
            }

            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine($"  {property.Name}");
            }
        }

        private static void PrintFields(Type type)
        {
            Console.WriteLine("\nFields:");
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
            if (fields.Length == 0)
            {
                Console.WriteLine("  No fields found.");
                return;
            }

            foreach (FieldInfo field in fields)
            {
                Console.WriteLine($"  {field.Name}");
            }
        }

        private static void PrintEvents(Type type)
        {
            Console.WriteLine("\nEvents:");
            EventInfo[] events = type.GetEvents(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
            if (events.Length == 0)
            {
                Console.WriteLine("  No events found.");
                return;
            }

            foreach (EventInfo eventInfo in events)
            {
                Console.WriteLine($"  {eventInfo.Name}");
            }
        }

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

        private static bool IsValidFilepath(string filepath)
        {
            return !string.IsNullOrWhiteSpace(filepath) && File.Exists(filepath);
        }
    }
}