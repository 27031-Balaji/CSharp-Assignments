namespace InventoryManagement.Exceptions
{
    /// <summary>
    /// This is used to make a new exception for reduce stock operation when the user reduces stock above the available stock.
    /// </summary>
    internal class InsufficientStockException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsufficientStockException"/> class.
        /// </summary>
        public InsufficientStockException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsufficientStockException"/> class.
        /// </summary>
        /// <param name="message">The message to be printed when the exception arises.</param>
        public InsufficientStockException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsufficientStockException" /> class.
        /// </summary>
        /// <param name="message">The message to be printed when the exception arises.</param>
        /// <param name="innerException">The inner exception that is used to preserve the exception chain.</param>
        public InsufficientStockException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}