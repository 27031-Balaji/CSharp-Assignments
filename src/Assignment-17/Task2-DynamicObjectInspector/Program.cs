using DynamicObjectInspector.Class;

namespace DynamicObjectInspector
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The method that executes when the application is run.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            Person person = new Person();
            ObjectInspector inspector = new ObjectInspector();

            Console.WriteLine("Current Properties:");
            inspector.DisplayProperties(person);
            Console.WriteLine();

            Console.Write("Enter property name to update: ");
            string propertyName = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter new value: ");
            string newValue = Console.ReadLine() ?? string.Empty;

            inspector.UpdateProperty(person, propertyName, newValue);

            Console.WriteLine("\nUpdated Properties:");
            inspector.DisplayProperties(person);

            Console.ReadKey();
        }
    }
}