using InventoryManagement.Model;

namespace InventoryManagement.Repository
{
    /// <summary>
    /// Stores and manages product data in memory.
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
        /// Gets the total number of products in the repository.
        /// </summary>
        /// <value>
        /// The number of products stored in the repository.
        /// </value>
        public int ProductCount { get => this._products.Count; }

        /// <summary>
        /// Adds a product to the repository.
        /// </summary>
        /// <param name="product">The product to add.</param>
        public void AddProduct(Product product)
        {
            this._products.Add(product);
        }

        /// <summary>
        /// Retrieves a product using its ID.
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <returns>The matching product if found, otherwise null.</returns>
        public Product? GetProductById(string productId)
        {
            return this._products.Find(product => product.ProductId == productId);
        }

        /// <summary>
        /// Retrieves a list of products that match the specified name (case-insensitive).
        /// </summary>
        /// <param name="nameOfProduct">The name of the product to be searched.</param>
        /// <returns>The list of products with the matching name.</returns>
        public List<Product> GetProductsByName(string nameOfProduct)
        {
            List<Product> products = new List<Product>();
            string searchName = nameOfProduct.Replace(" ", string.Empty).Trim();
            foreach (Product product in this._products)
            {
                string productName = product.Name.Replace(" ", string.Empty);
                if (productName.Contains(searchName, StringComparison.OrdinalIgnoreCase))
                {
                    products.Add(product);
                }
            }

            return products;
        }

        /// <summary>
        /// Updates the name of a product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <param name="name">The new product name.</param>
        public void UpdateName(Product product, string name)
        {
            product.Name = name;
        }

        /// <summary>
        /// Updates the price of a product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <param name="price">The new product price.</param>
        public void UpdatePrice(Product product, decimal price)
        {
            product.Price = price;
        }

        /// <summary>
        /// Updates the quantity of a product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <param name="quantity">The new stock quantity.</param>
        public void UpdateQuantity(Product product, int quantity)
        {
            product.Quantity = quantity;
        }

        /// <summary>
        /// Determines whether the specified product ID already exists.
        /// </summary>
        /// <param name="productId">The product ID to search for.</param>
        /// <returns>True if the product ID exists, otherwise false.</returns>
        public bool ProductIdExists(string productId)
        {
            return this._products.Any(product => product.ProductId == productId);
        }

        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        /// <returns>A copy of all products in the repository.</returns>
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            foreach (Product product in this._products)
            {
                products.Add(new Product(product.ProductId, product.Name, product.Price, product.Quantity));
            }

            return products;
        }

        /// <summary>
        /// Removes a product from the repository.
        /// </summary>
        /// <param name="product">The product to remove.</param>
        /// <returns>True if the product was removed, otherwise false.</returns>
        public bool DeleteProduct(Product product)
        {
            return this._products.Remove(product);
        }
    }
}