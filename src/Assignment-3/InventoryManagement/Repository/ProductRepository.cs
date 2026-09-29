using InventoryManagement.Exceptions;
using InventoryManagement.Model;

namespace InventoryManagement.Repository
{
    /// <summary>
    /// Stores and manages <see cref="Product"/> data in memory.
    /// </summary>
    internal class ProductRepository : IRepository
    {
        private readonly Dictionary<string, Product> _products;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        public ProductRepository()
        {
            this._products = new Dictionary<string, Product>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the total number of <see cref="Product"/> in the repository.
        /// </summary>
        /// <value>
        /// The number of <see cref="Product"/> stored in the repository.
        /// </value>
        public int Count { get => this._products.Count; }

        /// <summary>
        /// Adds a <see cref="Product"/> to the repository.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> to add.</param>
        public void AddProduct(Product product)
        {
            this._products.Add(product.ProductId, product);
        }

        /// <summary>
        /// Retrieves a <see cref="Product"/> using its ID.
        /// </summary>
        /// <param name="productId">The <see cref="Product"/> ID.</param>
        /// <returns>The matching <see cref="Product"/> if found, otherwise null.</returns>
        public Product? GetProductById(string productId)
        {
            if (!this._products.TryGetValue(productId, out Product? product))
            {
                return null;
            }

            return this.Clone(product);
        }

        /// <summary>
        /// Retrieves a list of <see cref="Product"/> that match the specified name (case-insensitive).
        /// </summary>
        /// <param name="nameOfProduct">The name of the <see cref="Product"/> to be searched.</param>
        /// <returns>The list of <see cref="Product"/> with the matching name.</returns>
        public List<Product> GetProductsByName(string nameOfProduct)
        {
            List<Product> products = new List<Product>();
            string searchName = nameOfProduct.Replace(" ", string.Empty);
            foreach (Product product in this._products.Values)
            {
                string productName = product.Name.Replace(" ", string.Empty);
                if (productName.Contains(searchName, StringComparison.OrdinalIgnoreCase))
                {
                    products.Add(this.Clone(product));
                }
            }

            return products;
        }

        /// <summary>
        /// Updates the details of a <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> containing the updated details.</param>
        public void UpdateProduct(Product product)
        {
            Product originalProduct = this.FindOriginalProduct(product.ProductId);

            originalProduct.Name = product.Name;
            originalProduct.Price = product.Price;
            originalProduct.Quantity = product.Quantity;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Product"/> ID already exists.
        /// </summary>
        /// <param name="productId">The <see cref="Product"/> ID to search for.</param>
        /// <returns>True if the <see cref="Product"/> ID exists, otherwise false.</returns>
        public bool DoesProductExist(string productId)
        {
            return this._products.ContainsKey(productId);
        }

        /// <summary>
        /// Retrieves all the <see cref="Product"/> from the repository.
        /// </summary>
        /// <returns>A copy of all the <see cref="Product"/> in the repository.</returns>
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            foreach (Product product in this._products.Values)
            {
                products.Add(this.Clone(product));
            }

            return products;
        }

        /// <summary>
        /// Removes a <see cref="Product"/> from the repository.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> to remove.</param>
        /// <exception cref="ProductNotFoundException">
        /// Thrown when the specified product does not exist in the repository.
        /// </exception>
        public void DeleteProduct(Product product)
        {
            if (!this._products.Remove(product.ProductId))
            {
                throw new ProductNotFoundException(product.ProductId);
            }
        }

        /// <summary>
        /// Returns the <see cref="Product"/> from the repository.
        /// </summary>
        /// <param name="productId">The <see cref="Product"/> ID to be searched.</param>
        /// <returns>The original <see cref="Product"/> in the repository.</returns>
        /// <exception cref="ProductNotFoundException">
        /// Thrown when the specified product does not exist in the repository.
        /// </exception>
        private Product FindOriginalProduct(string productId)
        {
            if (!this._products.TryGetValue(productId, out Product? product))
            {
                throw new ProductNotFoundException(productId);
            }

            return product;
        }

        /// <summary>
        /// Returns the deep copy of the product info.
        /// </summary>
        /// <param name="product">The product object to be cloned.</param>
        /// <returns>The deep copy of the product.</returns>
        private Product Clone(Product product)
        {
            return new Product(product.ProductId, product.Name, product.Price, product.Quantity);
        }
    }
}