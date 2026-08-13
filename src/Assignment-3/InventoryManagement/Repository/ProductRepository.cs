using InventoryManagement.Model;

namespace InventoryManagement.Repository
{
    /// <summary>
    /// Stores and manages <see cref="Product"/> data in memory.
    /// </summary>
    internal class ProductRepository
    {
        private readonly List<Product> _products;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        public ProductRepository()
        {
            this._products = new List<Product>();
        }

        /// <summary>
        /// Gets the total number of <see cref="Product"/> in the repository.
        /// </summary>
        /// <value>
        /// The number of <see cref="Product"/> stored in the repository.
        /// </value>
        public int ProductCount { get => this._products.Count; }

        /// <summary>
        /// Adds a <see cref="Product"/> to the repository.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> to add.</param>
        public void AddProduct(Product product)
        {
            this._products.Add(product);
        }

        /// <summary>
        /// Retrieves a <see cref="Product"/> using its ID.
        /// </summary>
        /// <param name="productId">The <see cref="Product"/> ID.</param>
        /// <returns>The matching <see cref="Product"/> if found, otherwise null.</returns>
        public Product? GetProductById(string productId)
        {
            Product? product = this._products.Find(product => product.ProductId == productId);
            return this.Clone(product!);
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
            foreach (Product product in this._products)
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
        /// Updates the name of a <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> to update.</param>
        /// <param name="name">The new <see cref="Product"/> name.</param>
        public void UpdateName(Product product, string name)
        {
            Product originalProduct = this.FindOriginalProduct(product.ProductId);
            originalProduct.Name = name;
        }

        /// <summary>
        /// Updates the price of a <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> to update.</param>
        /// <param name="price">The new <see cref="Product"/> price.</param>
        public void UpdatePrice(Product product, decimal price)
        {
            Product originalProduct = this.FindOriginalProduct(product.ProductId);
            originalProduct.Price = price;
        }

        /// <summary>
        /// Updates the quantity of a <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> to update.</param>
        /// <param name="quantity">The new stock quantity.</param>
        public void UpdateQuantity(Product product, int quantity)
        {
            Product originalProduct = this.FindOriginalProduct(product.ProductId);
            originalProduct.Quantity = quantity;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Product"/> ID already exists.
        /// </summary>
        /// <param name="productId">The <see cref="Product"/> ID to search for.</param>
        /// <returns>True if the <see cref="Product"/> ID exists, otherwise false.</returns>
        public bool ProductIdExists(string productId)
        {
            return this._products.Any(product => product.ProductId == productId);
        }

        /// <summary>
        /// Retrieves all the <see cref="Product"/> from the repository.
        /// </summary>
        /// <returns>A copy of all the <see cref="Product"/> in the repository.</returns>
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            foreach (Product product in this._products)
            {
                products.Add(this.Clone(product));
            }

            return products;
        }

        /// <summary>
        /// Removes a <see cref="Product"/> from the repository.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> to remove.</param>
        /// <returns>True if the <see cref="Product"/> was removed, otherwise false.</returns>
        public bool DeleteProduct(Product product)
        {
            Product originalProduct = this.FindOriginalProduct(product.ProductId);
            return this._products.Remove(product);
        }

        private Product FindOriginalProduct(string productId)
        {
            return this._products.Find(product => product.ProductId == productId) !;
        }

        private Product Clone(Product product)
        {
            return new Product(product.ProductId, product.Name, product.Price, product.Quantity);
        }
    }
}