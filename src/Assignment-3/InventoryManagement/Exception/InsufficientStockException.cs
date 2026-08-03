namespace InventoryManagement.Exception
{
    /// <summary>
    /// This is used to make a new exception for reduce stock operation when the user reduces stock above the available stock.
    /// </summary>
    internal class InsufficientStockException : System.Exception
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
