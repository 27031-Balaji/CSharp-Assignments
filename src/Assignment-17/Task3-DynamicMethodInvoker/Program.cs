using DynamicMethodInvoker.Class;

namespace DynamicMethodInvoker
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
            Calculator calculator = new Calculator();
            MethodInvoker invoker = new MethodInvoker();
            Console.Write("Enter method name (Add, Subtract, Multiply): ");
            string methodName = Console.ReadLine() ?? string.Empty;
            invoker.InvokeMethod(calculator, methodName);
            Console.ReadKey();
        }
    }
}