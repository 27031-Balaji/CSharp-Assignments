using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Persistence;

namespace InventoryManagement.Services
{
    /// <summary>
    /// Provides business logic for managing products.
    /// </summary>
    internal class ProductServices
    {
        private const int LowStockThreshold = 5;
        private ProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductServices"/> class.
        /// </summary>
        /// <param name="repository">The product repository.</param>
        public ProductServices(ProductRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Adds a new product to the inventory.
        /// </summary>
        /// <param name="name">The product name.</param>
        /// <param name="price">The product price.</param>
        /// <param name="quantity">The initial stock quantity.</param>
        public void AddProduct(string name, decimal price, int quantity)
        {
            Product product = new Product(this.GenerateProductId(), name, price, quantity);
            this._repository.AddProduct(product);
        }

        /// <summary>
        /// Retrieves a product using its ID.
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <returns>The matching product if found, otherwise null.</returns>
        public Product SearchProduct(string productId)
        {
            Product? product = this._repository.GetProductById(productId);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            return product;
        }

        /// <summary>
        /// Updates the name of a product.
        /// </summary>
        /// <param name="product">The product entry to be updated.</param>
        /// <param name="name">The new product name.</param>
        public void EditName(Product product, string name)
        {
            this._repository.UpdateName(product, name.Trim());
        }

        /// <summary>
        /// Updates the price of a product.
        /// </summary>
        /// <param name="product">The product entry to be updated.</param>
        /// <param name="price">The new product price.</param>
        public void EditPrice(Product product, decimal price)
        {
            this._repository.UpdatePrice(product, price);
        }

        /// <summary>
        /// Retrieves all products sorted by name.
        /// </summary>
        /// <returns> A list of all products.</returns>
        public List<Product> GetAllProducts()
        {
            List<Product> products = this._repository.GetAllProducts();
            products.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));
            return products;
        }

        /// <summary>
        /// Deletes a product from the inventory, throws exception otherwise.
        /// </summary>
        /// <param name="product">The product entry to delete.</param>
        public void DeleteProduct(Product product)
        {
            if (!this._repository.DeleteProduct(product))
            {
                throw new ProductNotFoundException();
            }
        }

        /// <summary>
        /// Increases the stock quantity of a product.
        /// </summary>
        /// <param name="product">The product entry to be updated.</param>
        /// <param name="quantity">The quantity to add.</param>
        public void RestockProduct(Product product, int quantity)
        {
            this._repository.UpdateQuantity(product, product.Quantity + quantity);
        }

        /// <summary>
        /// Reduces the stock quantity of a product.
        /// </summary>
        /// <param name="product">The product entry to be updated.</param>
        /// <param name="quantity">The quantity to remove.</param>
        /// <returns>True if the stock was reduced, otherwise false.</returns>
        public bool ReduceStock(Product product, int quantity)
        {
            if (quantity > product.Quantity)
            {
                throw new InsufficientStockException();
            }

            this._repository.UpdateQuantity(product, product.Quantity - quantity);

            return true;
        }

        /// <summary>
        /// Retrieves all products with low stock.
        /// </summary>
        /// <returns>A list of products with stock below the low stock threshold.</returns>
        public List<Product> GetLowStockProducts()
        {
            List<Product> lowStockProducts = new List<Product>();
            foreach (Product product in this._repository.GetAllProducts())
            {
                if (product.Quantity <= LowStockThreshold)
                {
                    lowStockProducts.Add(product);
                }
            }

            lowStockProducts.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));

            return lowStockProducts;
        }

        /// <summary>
        /// Validates that the inventory is not empty. Throws an exception if it is empty.
        /// </summary>
        public void ValidateInventory()
        {
            if (this._repository.ProductCount == 0)
            {
                throw new EmptyInventoryException();
            }
        }

        /// <summary>
        /// Generates a unique product ID generated from the GUID and taking first 12 characters.
        /// </summary>
        /// <returns>A unique product ID.</returns>
        private string GenerateProductId()
        {
            string productId;
            do
            {
                productId = Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper();
            }
            while (this._repository.ProductIdExists(productId));

            return productId;
        }
    }
}