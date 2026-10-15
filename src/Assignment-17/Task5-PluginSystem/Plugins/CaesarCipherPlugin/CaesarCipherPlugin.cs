using PluginInterface;

namespace Plugins
{
    /// <summary>
    /// The plugin used for caesar cipher.
    /// </summary>
    public class CaesarCipherPlugin : IPlugin
    {
        private const int Shift = 3;

        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        /// <value>The name of the plugin.</value>
        public string PluginName => $"Caesar Cipher (Shift = {Shift})";

        /// <summary>
        /// Encrypts the contents.
        /// </summary>
        /// <param name="contents">The contents to be encrypted.</param>
        /// <returns>The encrypted contents.</returns>
        public string Encrypt(string contents)
        {
            return this.ShiftText(contents, Shift);
        }

        /// <summary>
        /// Decrypts the contents.
        /// </summary>
        /// <param name="contents">The contents to be decrypted.</param>
        /// <returns>The decrypted contents.</returns>
        public string Decrypt(string contents)
        {
            return this.ShiftText(contents, -Shift);
        }

        /// <summary>
        /// Shifts the text according to the shift number.
        /// </summary>
        /// <param name="text">The text to be shifted.</param>
        /// <param name="shift">The shift number.</param>
        /// <returns>The shifted text.</returns>
        private string ShiftText(string text, int shift)
        {
            char[] result = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                result[i] = (char)(text[i] + shift);
            }

            return new string(result);
        }
    }
}