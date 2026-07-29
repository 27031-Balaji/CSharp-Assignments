namespace InventoryManagement.Exceptions
{
    /// <summary>
    /// This is used to make a new exception for when product is not found in the inventory.
    /// </summary>
    internal class ProductNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductNotFoundException"/> class.
        /// </summary>
        public ProductNotFoundException()
            : base("Product not found in the inventory.")
        {
        }
    }
}