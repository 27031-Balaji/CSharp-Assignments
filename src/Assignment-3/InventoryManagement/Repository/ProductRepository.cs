using InventoryManagement.Model;

namespace InventoryManagement.Repository
{
    /// <summary>
    /// Stores and manages <see cref="Product"/> data in memory.
    /// </summary>
    internal class ProductRepository : IRepository
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
        public int Count { get => this._products.Count; }

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
            Product? product = this._products.Find(p => string.Equals(p.ProductId, productId, StringComparison.OrdinalIgnoreCase));
            return product == null ? null : this.Clone(product!);
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
            return this._products.Remove(originalProduct);
        }

        /// <summary>
        /// Returns the <see cref="Product"/> from the repository.
        /// </summary>
        /// <param name="productId">The <see cref="Product"/> ID to be searched.</param>
        /// <returns>The original <see cref="Product"/> in the repository.</returns>
        private Product FindOriginalProduct(string productId)
        {
            return this._products.Find(product => product.ProductId == productId) !;
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