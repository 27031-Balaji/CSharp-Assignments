using PluginInterface;

namespace Plugins
{
    /// <summary>
    /// The plugin used to implement the AtBash plugin.
    /// </summary>
    public class AtbashPlugin : IPlugin
    {
        /// <summary>
        /// Gets the name of the AtBash Plugin.
        /// </summary>
        /// <value>The name of the plugin.</value>
        public string PluginName => "Atbash";

        /// <summary>
        /// Encrypts the contents using AtBash.
        /// </summary>
        /// <param name="contents">The contents to be encrypted.</param>
        /// <returns>The encrypted content.</returns>
        public string Encrypt(string contents) => this.Transform(contents);

        /// <summary>
        /// Decrypts the contents using AtBash.
        /// </summary>
        /// <param name="contents">The contents to be decrypted.</param>
        /// <returns>The decrypted content.</returns>
        public string Decrypt(string contents) => this.Transform(contents);

        /// <summary>
        /// Transforms the given text by changing the alphabets.
        /// </summary>
        /// <param name="text">The text to be transformed.</param>
        /// <returns>The transformed text.</returns>
        private string Transform(string text)
        {
            char[] result = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c >= 'A' && c <= 'Z')
                {
                    result[i] = (char)('Z' - (c - 'A'));
                }
                else if (c >= 'a' && c <= 'z')
                {
                    result[i] = (char)('z' - (c - 'a'));
                }
                else
                {
                    result[i] = c;
                }
            }

            return new string(result);
        }
    }
}