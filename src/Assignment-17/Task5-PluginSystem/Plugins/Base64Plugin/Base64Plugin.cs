using System.Text;
using PluginInterface;

namespace Plugins
{
    /// <summary>
    /// The plugin used for Base64 operations.
    /// </summary>
    public class Base64Plugin : IPlugin
    {
        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        /// <value>The name of the plugin.</value>
        public string PluginName => "Base 64 Algorithm";

        /// <summary>
        /// Encrypts the contents using Base64.
        /// </summary>
        /// <param name="contents">The contents to be encrypted.</param>
        /// <returns>The encrypted contents.</returns>
        public string Encrypt(string contents)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(contents));
        }

        /// <summary>
        /// Decrypts the contents using Base64.
        /// </summary>
        /// <param name="contents">The contents to be decrypted.</param>
        /// <returns>The decrypted contents.</returns>
        public string Decrypt(string contents)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(contents));
        }
    }
}