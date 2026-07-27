using InventoryManagement.Models;

namespace InventoryManagement.Persistence
{
    internal class ProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            this._products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            this._products.Add(product);
        }

        public bool ProductIdExists(string productId)
        {
            return this._products.Any(product => product.ProductId == productId);
        }

        public List<Product> GetAllProducts()
        {
            return this._products;
        }
    }
}