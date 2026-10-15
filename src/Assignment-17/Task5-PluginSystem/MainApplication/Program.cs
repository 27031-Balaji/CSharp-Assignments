using System.Reflection;
using PluginInterface;

namespace MainApplication
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
            string pluginFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "PluginAssemblyFiles");
            if (!Directory.Exists(pluginFolderPath))
            {
                Console.WriteLine("Plugin folder path doesn't exist.\n");
                return;
            }

            List<IPlugin> loadedPlugins = LoadPlugins(pluginFolderPath);
            if (loadedPlugins.Count == 0)
            {
                Console.WriteLine("No plugins loaded.\n");
                return;
            }

            while (true)
            {
                ShowAlgorithms(loadedPlugins);
                Console.Write("Enter your choice: ");
                string choice = (Console.ReadLine() ?? string.Empty).Trim();

                if (!int.TryParse(choice, out int resultChoice) || resultChoice <= 0 || resultChoice > loadedPlugins.Count + 1)
                {
                    Console.WriteLine("Invalid choice.\n");
                    ClearScreenWithKey();
                    continue;
                }

                if (resultChoice == loadedPlugins.Count + 1)
                {
                    break;
                }

                IPlugin selectedPlugin = loadedPlugins[resultChoice - 1];
                bool isRunning = true;
                while (isRunning)
                {
                    ShowPluginOperations();
                    Console.Write("Enter your choice: ");
                    string operationChoice = (Console.ReadLine() ?? string.Empty).Trim();
                    switch (operationChoice)
                    {
                        case "A":
                            Encrypt(selectedPlugin);
                            break;

                        case "B":
                            Decrypt(selectedPlugin);
                            break;

                        case "C":
                            isRunning = false;
                            ClearScreen();
                            break;

                        default:
                            Console.WriteLine("Invalid choice.\n");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Loads all the plugin assemblies.
        /// </summary>
        /// <param name="pluginFolderPath">The plugin assembly folder path to be used for loading.</param>
        /// <returns>The list of loaded plugins.</returns>
        private static List<IPlugin> LoadPlugins(string pluginFolderPath)
        {
            List<IPlugin> plugins = new List<IPlugin>();
            string[] filePaths = Directory.GetFiles(pluginFolderPath, "*.dll");
            foreach (string filePath in filePaths)
            {
                Assembly specificPluginAssembly = Assembly.LoadFrom(filePath);
                foreach (Type type in specificPluginAssembly.GetTypes())
                {
                    if (type.GetInterfaces().Contains(typeof(IPlugin)) && type.IsClass && !type.IsAbstract && type.IsPublic)
                    {
                        IPlugin specificPlugin = (IPlugin)Activator.CreateInstance(type)!;
                        plugins.Add(specificPlugin);
                    }
                }
            }

            return plugins;
        }

        /// <summary>
        /// Shows the algorithm menu to the user.
        /// </summary>
        /// <param name="loadedPlugins">The list of loaded plugins.</param>
        private static void ShowAlgorithms(List<IPlugin> loadedPlugins)
        {
            Console.WriteLine("Welcome to the Encoding/Decoding platform.\n");
            Console.WriteLine("Choose an algorithm to encode/decode: ");
            int count = 1;
            foreach (IPlugin plugin in loadedPlugins)
            {
                Console.WriteLine($"{count}. {plugin.PluginName}");
                count++;
            }

            Console.WriteLine($"{count}. Exit\n");
        }

        /// <summary>
        /// Shows the plugin operations to the user.
        /// </summary>
        private static void ShowPluginOperations()
        {
            Console.WriteLine("\nSelect operation to be done.\n");
            Console.WriteLine("[A] Encrypt");
            Console.WriteLine("[B] Decrypt");
            Console.WriteLine("[C] Exit\n");
        }

        /// <summary>
        /// Encrypts the contents given by the user.
        /// </summary>
        /// <param name="selectedPlugin">The selected plugin used for encryption.</param>
        private static void Encrypt(IPlugin selectedPlugin)
        {
            Console.Write($"Enter the string to be encrypted by {selectedPlugin.PluginName}: ");
            string content = (Console.ReadLine() ?? string.Empty).Trim();
            string encryptedContent = selectedPlugin.Encrypt(content);
            Console.WriteLine($"Encrypted Content for {content} is: {encryptedContent}");
        }

        /// <summary>
        /// Decrypts the contents given by the user.
        /// </summary>
        /// <param name="selectedPlugin">The selected plugin used for decryption.</param>
        private static void Decrypt(IPlugin selectedPlugin)
        {
            Console.Write($"Enter the string to be decrypted by {selectedPlugin.PluginName}: ");
            string content = (Console.ReadLine() ?? string.Empty).Trim();
            string decryptedContent = selectedPlugin.Encrypt(content);
            Console.WriteLine($"Encrypted Content for {content} is: {decryptedContent}");
        }

        /// <summary>
        /// Prompts the user for a key and then clears the screen.
        /// </summary>
        private static void ClearScreenWithKey()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Clears the screen.
        /// </summary>
        private static void ClearScreen()
        {
            Console.Clear();
        }
    }
}