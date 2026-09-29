namespace InventoryManagement.Exceptions
{
    /// <summary>
    /// This is used to make a new exception for when <see cref="Model.Product"/> is not found in the inventory.
    /// </summary>
    internal class ProductNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductNotFoundException"/> class.
        /// </summary>
        public ProductNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message to be printed when the exception arises.</param>
        public ProductNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductNotFoundException" /> class.
        /// </summary>
        /// <param name="message">The message to be printed when the exception arises.</param>
        /// <param name="innerException">The inner exception that is used to preserve the exception chain.</param>
        public ProductNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}