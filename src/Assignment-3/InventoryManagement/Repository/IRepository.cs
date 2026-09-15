using InventoryManagement.Model;

namespace InventoryManagement.Repository
{
    /// <summary>
    /// Defines a contract for managing products in a repository.
    /// </summary>
    internal interface IRepository
    {
        /// <summary>
        /// Gets the count of products in the repository.
        /// </summary>
        /// <value>The count of products in the repository.</value>
        int Count { get; }

        /// <summary>
        /// Adds a <see cref="Model.Product"/> to the repository.
        /// </summary>
        /// <param name="product">The <see cref="Model.Product"/> to be added.</param>
        void AddProduct(Product product);

        /// <summary>
        /// Updates a specific <see cref="Model.Product"/> to the repository.
        /// </summary>
        /// <param name="product">The <see cref="Model.Product"/> to be added.</param>
        void UpdateProduct(Product product);

        /// <summary>
        /// Deletes a <see cref="Model.Product"/> to the repository.
        /// </summary>
        /// <param name="product">The <see cref="Model.Product"/> to be added.</param>
        /// <returns>True if the <see cref="Model.Product"/> is deleted, else false.</returns>
        bool DeleteProduct(Product product);

        /// <summary>
        /// Returns all products from the repository.
        /// </summary>
        /// <returns>A list of all products in the repository.</returns>
        List<Product> GetAllProducts();

        /// <summary>
        /// Retrieves the <see cref="Model.Product"/> that matches the specified unique identifier.
        /// </summary>
        /// <param name="productId">The unique identifier of the <see cref="Model.Product"/> to retrieve.</param>
        /// <returns>The <see cref="Model.Product"/> associated with the specified identifier, or null.</returns>
        Product? GetProductById(string productId);

        /// <summary>
        /// Retrieves products that match the specified name.
        /// </summary>
        /// <param name="productName">The <see cref="Model.Product"/> name of the product to retrieve.</param>
        /// <returns>A list of products that match the specified name.</returns>
        List<Product> GetProductsByName(string productName);

        /// <summary>
        /// Determines whether a product exists.
        /// </summary>
        /// <param name="productId">The unique identifier of the <see cref="Model.Product"/>.</param>
        /// <returns>True if the <see cref="Model.Product"/> already exists, else false.</returns>
        bool DoesProductExist(string productId);
    }
}