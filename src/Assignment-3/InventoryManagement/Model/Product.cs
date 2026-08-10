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
        /// <param name="productId">The unique ID of the <see cref="Product"/>.</param>
        /// <param name="nameOfProduct">The name of the <see cref="Product"/>.</param>
        /// <param name="price">The price of the <see cref="Product"/>.</param>
        /// <param name="quantity">The quantity of stock available for the <see cref="Product"/>.</param>
        public Product(string productId, string nameOfProduct, decimal price, int quantity)
        {
            this.ProductId = productId;
            this.Name = nameOfProduct;
            this.Price = price;
            this.Quantity = quantity;
        }

        /// <summary>
        /// Gets the unique <see cref="Product"/> identifier.
        /// </summary>
        /// <value>The unique ID of the <see cref="Product"/>.</value>
        public string ProductId { get; }

        /// <summary>
        /// Gets or sets the <see cref="Product"/> name.
        /// </summary>
        /// <value>The name of the <see cref="Product"/>.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Product"/> price.
        /// </summary>
        /// <value>The price of the <see cref="Product"/>.</value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the available quantity of the <see cref="Product"/>.
        /// </summary>
        /// <value>The quantity of stock available for the <see cref="Product"/>.</value>
        public int Quantity { get; set; }
    }
}