namespace InventoryManagement.Exception
{
    /// <summary>
    /// This is used to make a new exception when the <see cref="Model.Product"/> inventory is empty and the user tries to perform an operation that requires products in the inventory.
    /// </summary>
    internal class EmptyInventoryException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyInventoryException"/> class.
        /// </summary>
        public EmptyInventoryException()
            : base("Inventory is empty.")
        {
        }
    }
}