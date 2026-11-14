using System.Reflection;
using System.Text;

namespace SerializationAPI
{
    /// <summary>
    /// Reflection-based serializer taking an object and serializing it.
    /// </summary>
    public class ReflectionSerializer
    {
        /// <summary>
        /// The method that serializes a given object.
        /// </summary>
        /// <param name="obj">The object to be serialized.</param>
        /// <returns>The serialized content.</returns>
        public string Serialize(object obj)
        {
            StringBuilder builder = new StringBuilder();
            Type type = obj.GetType();
            foreach (PropertyInfo property in type.GetProperties())
            {
                object? value = property.GetValue(obj);
                builder.Append($"{property.Name}: {value}; ");
            }

            return builder.ToString();
        }
    }
}