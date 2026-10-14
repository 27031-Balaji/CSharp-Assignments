using System.Reflection;

namespace DynamicObjectInspector.Class
{
    /// <summary>
    /// Used to analyze and inspect the properteies of the object.
    /// </summary>
    internal class ObjectInspector
    {
        /// <summary>
        /// Displays the properties of the object.
        /// </summary>
        /// <param name="obj">The object to be inspected.</param>
        public void DisplayProperties(object obj)
        {
            Type type = obj.GetType();
            Console.WriteLine($"Object Type: {type.Name}");
            Console.WriteLine();

            PropertyInfo[] properties = type.GetProperties();
            if (properties.Length == 0)
            {
                Console.WriteLine("No properties found.");
                return;
            }

            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine($"{property.Name}: {property.GetValue(obj)}");
            }
        }

        /// <summary>
        /// Updates the specific property to a new value.
        /// </summary>
        /// <param name="obj">The object used for inspection.</param>
        /// <param name="propertyName">The property name of the property in which the value is to be changed.</param>
        /// <param name="newValue">The new value to be used for changing.</param>
        public void UpdateProperty(object obj, string propertyName, string newValue)
        {
            Type type = obj.GetType();
            PropertyInfo? property = type.GetProperty(propertyName);
            if (property == null)
            {
                Console.WriteLine($"Property '{propertyName}' not found.");
                return;
            }

            if (!property.CanWrite)
            {
                Console.WriteLine($"Property '{propertyName}' is read-only.");
                return;
            }

            try
            {
                object convertedValue = Convert.ChangeType(newValue, property.PropertyType);
                property.SetValue(obj, convertedValue);
                Console.WriteLine($"Property '{propertyName}' updated successfully.");
            }
            catch
            {
                Console.WriteLine($"Invalid value for type {property.PropertyType.Name}.");
            }
        }
    }
}