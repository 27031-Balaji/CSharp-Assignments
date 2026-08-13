using InventoryManagement.Exception;
using InventoryManagement.Model;
using InventoryManagement.Repository;

namespace InventoryManagement.Service
{
    /// <summary>
    /// Provides business logic for managing <see cref="Product"/> instances.
    /// </summary>
    internal class ProductService
    {
        private const int LowStockThreshold = 5;
        private ProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="Product"/> repository.</param>
        public ProductService(ProductRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Adds a new <see cref="Product"/> to the inventory.
        /// </summary>
        /// <param name="name">The <see cref="Product"/> name.</param>
        /// <param name="price">The <see cref="Product"/> price.</param>
        /// <param name="quantity">The initial stock quantity.</param>
        public void AddProduct(string name, decimal price, int quantity)
        {
            Product product = new Product(this.GenerateProductId(), name, price, quantity);
            this._repository.AddProduct(product);
        }

        /// <summary>
        /// Retrieves a <see cref="Product"/> using its ID.
        /// </summary>
        /// <param name="productId">The <see cref="Product"/> ID.</param>
        /// <returns>The matching <see cref="Product"/> if found, otherwise null.</returns>
        public Product SearchProductById(string productId)
        {
            Product? product = this._repository.GetProductById(productId);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            return product;
        }

        /// <summary>
        /// Search for a list of <see cref="Product"/> by name. Throws an exception if no products are found.
        /// </summary>
        /// <param name="nameOfProduct">The name of the <see cref="Product"/> to be searched.</param>
        /// <returns>The list of <see cref="Product"/> with the matching name.</returns>
        public List<Product> SearchProductsByName(string nameOfProduct)
        {
            List<Product> products = this._repository.GetProductsByName(nameOfProduct);

            if (products.Count == 0)
            {
                throw new ProductNotFoundException();
            }

            this.SortProductsByName(products);

            return products;
        }

        /// <summary>
        /// Updates the name of a <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> entry to be updated.</param>
        /// <param name="name">The new <see cref="Product"/> name.</param>
        public void EditProductName(Product product, string name)
        {
            this._repository.UpdateName(product, name);
        }

        /// <summary>
        /// Updates the price of a <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> entry to be updated.</param>
        /// <param name="price">The new <see cref="Product"/> price.</param>
        public void EditProductPrice(Product product, decimal price)
        {
            this._repository.UpdatePrice(product, price);
        }

        /// <summary>
        /// Retrieves all <see cref="Product"/> sorted by name.
        /// </summary>
        /// <returns> A list of all <see cref="Product"/>.</returns>
        public List<Product> GetAllProducts()
        {
            List<Product> products = this._repository.GetAllProducts();
            this.SortProductsByName(products);

            return products;
        }

        /// <summary>
        /// Deletes a <see cref="Product"/> from the inventory, throws exception otherwise.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> entry to delete.</param>
        public void DeleteProduct(Product product)
        {
            if (!this._repository.DeleteProduct(product))
            {
                throw new ProductNotFoundException();
            }
        }

        /// <summary>
        /// Increases the stock quantity of a <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> entry to be updated.</param>
        /// <param name="quantity">The quantity to add.</param>
        public void RestockProduct(Product product, int quantity)
        {
            this._repository.UpdateQuantity(product, product.Quantity + quantity);
        }

        /// <summary>
        /// Reduces the stock quantity of a <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> entry to be updated.</param>
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
        /// Retrieves all <see cref="Product"/> with low stock.
        /// </summary>
        /// <returns>A list of <see cref="Product"/> with stock below the low stock threshold.</returns>
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

            this.SortProductsByName(lowStockProducts);

            return lowStockProducts;
        }

        /// <summary>
        /// Checks whether the inventory is empty or not. Throws an exception if it is empty.
        /// </summary>
        public void CheckInventory()
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
            while (this._repository.IsProductExists(productId));

            return productId;
        }

        /// <summary>
        /// Sorts the <see cref="Product"/> list by name in ascending order, ignoring case.
        /// </summary>
        /// <param name="products">The list of <see cref="Product"/> to be sorted.</param>
        private void SortProductsByName(List<Product> products)
        {
            products.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));
        }
    }
}