namespace InventoryManagement.Exceptions
{
    /// <summary>
    /// This is used to make a new exception when the <see cref="Model.Product"/> inventory is empty and the user tries to perform an operation that requires products in the inventory.
    /// </summary>
    internal class EmptyInventoryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyInventoryException"/> class.
        /// </summary>
        public EmptyInventoryException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyInventoryException" /> class.
        /// </summary>
        /// <param name="message">The message to be printed when the exception arises.</param>
        public EmptyInventoryException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyInventoryException" /> class.
        /// </summary>
        /// <param name="message">The message to be printed when the exception arises.</param>
        /// <param name="innerException">The inner exception that is used to preserve the exception chain.</param>
        public EmptyInventoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}