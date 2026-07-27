using InventoryManagement.Models;
using InventoryManagement.Persistence;

namespace InventoryManagement.Services
{
    internal class ProductServices
    {
        private ProductRepository _repository;

        public ProductServices(ProductRepository repository)
        {
            this._repository = repository;
        }

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

        public void AddProduct(string name, decimal price, int quantity)
        {
            Product product = new Product(this.GenerateProductId(), name, price, quantity);
            this._repository.AddProduct(product);
        }
    }
}