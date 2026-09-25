namespace Assignment9.Model
{
    /// <summary>
    /// Represents an supplier with the ID, name and product ID.
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// Gets or sets the ID of the supplier.
        /// </summary>
        /// <value>The ID of the supplier.</value>
        public int SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the name of the supplier.
        /// </summary>
        /// <value>The name of the supplier.</value>
        public string SupplierName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ID of the product.
        /// </summary>
        /// <value>The ID of the product.</value>
        public int ProductId { get; set; }
    }
}