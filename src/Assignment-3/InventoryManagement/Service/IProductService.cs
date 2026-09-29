using InventoryManagement.Model;

namespace InventoryManagement.Service
{
    /// <summary>
    /// Defines business operations for managing <see cref="Product"/> instances.
    /// </summary>
    internal interface IProductService
    {
        /// <summary>
        /// Adds a new <see cref="Product"/> to the inventory.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="quantity">The initial stock quantity.</param>
        void AddProduct(string name, decimal price, int quantity);

        /// <summary>
        /// Retrieves a <see cref="Product"/> using its unique identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>The matching <see cref="Product"/>.</returns>
        Product SearchProductById(string productId);

        /// <summary>
        /// Searches for <see cref="Product"/> instances matching the specified name.
        /// </summary>
        /// <param name="nameOfProduct">The name or partial name of the product to search for.</param>
        /// <returns>A list of matching <see cref="Product"/> instances.</returns>
        List<Product> SearchProductsByName(string nameOfProduct);

        /// <summary>
        /// Updates the name of the specified <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <param name="name">The new product name.</param>
        void EditProductName(Product product, string name);

        /// <summary>
        /// Updates the price of the specified <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <param name="price">The new product price.</param>
        void EditProductPrice(Product product, decimal price);

        /// <summary>
        /// Retrieves all <see cref="Product"/> instances from the inventory.
        /// </summary>
        /// <returns>A list of all products.</returns>
        List<Product> GetAllProducts();

        /// <summary>
        /// Deletes the specified <see cref="Product"/> from the inventory.
        /// </summary>
        /// <param name="product">The product to delete.</param>
        void DeleteProduct(Product product);

        /// <summary>
        /// Increases the stock quantity of the specified <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The product to restock.</param>
        /// <param name="quantity">The quantity to add.</param>
        void RestockProduct(Product product, int quantity);

        /// <summary>
        /// Decreases the stock quantity of the specified <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The product whose stock will be reduced.</param>
        /// <param name="quantity">The quantity to remove.</param>
        void ReduceStock(Product product, int quantity);

        /// <summary>
        /// Retrieves all <see cref="Product"/> instances whose stock quantity
        /// is below or equal to the low stock threshold.
        /// </summary>
        /// <returns>A list of low-stock products.</returns>
        List<Product> GetLowStockProducts();

        /// <summary>
        /// Checks whether the inventory has products.
        /// </summary>
        /// <returns>True if the inventory has products, else false.</returns>
        bool HasProducts();
    }
}