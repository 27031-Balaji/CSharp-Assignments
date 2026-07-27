using InventoryManagement.Helpers;
using InventoryManagement.Services;
using InventoryManagement.View;

namespace InventoryManagement.Controllers
{
    internal class ProductController
    {
        private ProductServices _services;
        private ProductHelper _helper;
        private ConsoleOperations _view;

        public ProductController(ProductServices services, ProductHelper helper, ConsoleOperations view)
        {
            this._services = services;
            this._helper = helper;
            this._view = view;
        }

        public void Run()
        {
            bool isRunning = true;
            do
            {
                this._view.DisplayMenu();
                string choice = this._view.ReadChoice();
                switch (choice)
                {
                    case "1":
                        this.AddProduct();
                        break;

                    case "2":
                        this.EditProduct();
                        break;

                    case "3":
                        this.SearchProduct();
                        break;

                    case "4":
                        this.ViewAllProducts();
                        break;

                    case "5":
                        this.DeleteProduct();
                        break;

                    case "6":
                        this.RestockProduct();
                        break;

                    case "7":
                        this.ReduceStock();
                        break;

                    case "8":
                        this.ViewLowStockProducts();
                        break;

                    case "9":
                        isRunning = false;
                        break;

                    default:
                        this._view.DisplayMessage("Invalid choice.");
                        this._view.FlushScreenWithKey();
                        break;
                }
            }
            while (isRunning);
        }

        private void AddProduct()
        {
            string name;
            do
            {
                name = this._view.ReadProductName();

                if (!this._helper.IsValidName(name))
                {
                    this._view.DisplayMessage("Product name cannot be empty.");
                }
            }
            while (!this._helper.IsValidName(name));

            string priceInput;
            decimal price;
            do
            {
                priceInput = this._view.ReadProductPrice();
                if (!this._helper.IsValidPrice(priceInput, out price))
                {
                    this._view.DisplayMessage("Price must be greater than zero.");
                }
            }
            while (!this._helper.IsValidPrice(priceInput, out price));

            string quantityInput;
            int quantity;
            do
            {
                quantityInput = this._view.ReadProductQuantity();

                if (!this._helper.IsValidQuantity(quantityInput, out quantity))
                {
                    this._view.DisplayMessage("Quantity must be zero or greater.");
                }
            }
            while (!this._helper.IsValidQuantity(quantityInput, out quantity));

            this._services.AddProduct(name, price, quantity);

            this._view.DisplayMessage("Product added successfully.");
            this._view.FlushScreenWithKey();
        }

        private void EditProduct()
        {
            this._view.DisplayMessage("Edit Product - Coming Soon");
            this._view.FlushScreenWithKey();
        }

        private void SearchProduct()
        {
            this._view.DisplayMessage("Search Product - Coming Soon");
            this._view.FlushScreenWithKey();
        }

        private void ViewAllProducts()
        {
            this._view.DisplayMessage("View All Products - Coming Soon");
            this._view.FlushScreenWithKey();
        }

        private void DeleteProduct()
        {
            this._view.DisplayMessage("Delete Product - Coming Soon");
            this._view.FlushScreenWithKey();
        }

        private void RestockProduct()
        {
            this._view.DisplayMessage("Restock Product - Coming Soon");
            this._view.FlushScreenWithKey();
        }

        private void ReduceStock()
        {
            this._view.DisplayMessage("Reduce Stock - Coming Soon");
            this._view.FlushScreenWithKey();
        }

        private void ViewLowStockProducts()
        {
            this._view.DisplayMessage("View Low Stock Products - Coming Soon");
            this._view.FlushScreenWithKey();
        }
    }
}