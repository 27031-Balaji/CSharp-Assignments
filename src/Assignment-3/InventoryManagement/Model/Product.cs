namespace InventoryManagement.Model
{
    /// <summary>
    /// Represents a product in the inventory.
    /// </summary>
    internal class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="productId">The unique ID of the product.</param>
        /// <param name="nameOfProd">The name of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="quantity">The quantity of stock available for the product.</param>
        public Product(string productId, string nameOfProd, decimal price, int quantity)
        {
            this.ProductId = productId;
            this.Name = nameOfProd;
            this.Price = price;
            this.Quantity = quantity;
        }

        /// <summary>
        /// Gets the unique product identifier.
        /// </summary>
        /// <value>The unique ID of the product.</value>
        public string ProductId { get; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        /// <value>The name of the product.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        /// <value>The price of the product.</value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the available quantity.
        /// </summary>
        /// <value>The quantity of stock available.</value>
        public int Quantity { get; set; }
    }
}