namespace MockingFramework.Interface
{
    /// <summary>
    /// The interface used for the plugins.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        /// <value>The name of the plugin.</value>
        string PluginName { get; }

        /// <summary>
        /// Encrypts the contents.
        /// </summary>
        /// <param name="contents">The contents to be encrypted.</param>
        /// <returns>The encrypted contents.</returns>
        string Encrypt(string contents);

        /// <summary>
        /// Decrypts the contents.
        /// </summary>
        /// <param name="contents">The contents to be decrypted.</param>
        /// <returns>The decrypted contents.</returns>
        string Decrypt(string contents);
    }
}