namespace InventoryManagement.Exceptions
{
    /// <summary>
    /// This is used to make a new exception for reduce stock operation when the user reduces stock below the available stock.
    /// </summary>
    internal class InsufficientStockException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsufficientStockException"/> class.
        /// </summary>
        public InsufficientStockException()
            : base("Insufficient stock available.")
        {
        }
    }
}
