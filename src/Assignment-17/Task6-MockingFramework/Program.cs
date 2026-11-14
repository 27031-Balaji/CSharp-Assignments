using MockingFramework.Class;
using MockingFramework.Interface;

namespace MockingFramework
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
            MockPluginFramework framework = new MockPluginFramework();
            IPlugin plugin = framework.CreateMockPlugin();
            Console.WriteLine(plugin.PluginName);
            Console.WriteLine(plugin.Encrypt("Hello World"));
            Console.WriteLine(plugin.Decrypt("Hello World"));

            Console.ReadKey();
        }
    }
}