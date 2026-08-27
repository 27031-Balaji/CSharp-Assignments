namespace Assignment9.Model
{
    /// <summary>
    /// Represents a product with the ID, name, price and category.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the ID of the product..
        /// </summary>
        /// <value>The ID of the product.</value>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>The name of the product.</value>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        /// <value>The price of the product.</value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        /// <value>The category of the product.</value>
        public string Category { get; set; } = string.Empty;
    }
}