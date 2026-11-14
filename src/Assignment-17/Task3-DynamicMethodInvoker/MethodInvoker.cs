using System.Reflection;

namespace DynamicMethodInvoker.Class
{
    /// <summary>
    /// Class that contains methods to invoke the method dynamically.
    /// </summary>
    public class MethodInvoker
    {
        /// <summary>
        /// Dynamically invokes the method for operations.
        /// </summary>
        /// <param name="obj">The object in which the method is to be invoked.</param>
        /// <param name="methodName">The name of the method to be invoked.</param>
        public void InvokeMethod(object obj, string methodName)
        {
            Type type = obj.GetType();
            MethodInfo? method = type.GetMethod(methodName);
            if (method == null)
            {
                Console.WriteLine($"Method '{methodName}' not found.");
                return;
            }

            method.Invoke(obj, null);
        }
    }
}