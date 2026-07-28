using InventoryManagement.Helpers;
using InventoryManagement.Models;
using InventoryManagement.Services;
using InventoryManagement.View;

namespace InventoryManagement.Controllers
{
    /// <summary>
    /// Coordinates user interactions and application flow for inventory management.
    /// </summary>
    internal class ProductControllers
    {
        // Validation Messages
        private const string InvalidNameMessage = "Enter a valid product name.";
        private const string InvalidPriceMessage = "Enter a valid price.";
        private const string InvalidQuantityMessage = "Enter a valid quantity.";
        private const string InvalidOptionMessage = "Enter a valid option.";
        private const string InvalidProductIdMessage = "Enter a valid Product ID.";
        private const string InventoryEmptyMessage = "Inventory is empty.";
        private const string ProductNotFoundMessage = "Product not found.";
        private const string InsufficientStockMessage = "Insufficient stock available.";

        // Product Messages
        private const string ProductAddedMessage = "Product added successfully.";
        private const string NameUpdatedMessage = "Product name updated successfully.";
        private const string PriceUpdatedMessage = "Product price updated successfully.";
        private const string EditCompletedMessage = "Edit completed.";
        private const string ProductDeletedMessage = "Product deleted successfully.";
        private const string StockRestockedMessage = "Stock updated successfully.";
        private const string StockReducedMessage = "Stock reduced successfully.";
        private const string NoLowStockProductsMessage = "No products are low in stock.";

        // Application Messages
        private const string ExitMessage = "Exiting Application...";

        private readonly ProductServices _services;
        private readonly ProductHelpers _helper;
        private readonly ConsoleOperations _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductControllers"/> class.
        /// </summary>
        /// <param name="services">The product service.</param>
        /// <param name="helper">The product helper.</param>
        /// <param name="view">The console view.</param>
        public ProductControllers(ProductServices services, ProductHelpers helper, ConsoleOperations view)
        {
            this._services = services;
            this._helper = helper;
            this._view = view;
        }

        /// <summary>
        /// Starts the main menu of the application.
        /// </summary>
        public void Run()
        {
            bool isRunning = true;

            do
            {
                string option = this._view.ShowMainMenu();

                switch (option.Trim().ToUpper())
                {
                    case "A":
                        this.AddProduct();
                        break;

                    case "B":
                        this.EditProduct();
                        break;

                    case "C":
                        this.SearchProduct();
                        break;

                    case "D":
                        this.ViewAllProducts();
                        break;

                    case "E":
                        this.DeleteProduct();
                        break;

                    case "F":
                        this.RestockProduct();
                        break;

                    case "G":
                        this.ReduceStock();
                        break;

                    case "H":
                        this.ViewLowStockProducts();
                        break;

                    case "I":
                        this._view.ShowInfo(ExitMessage);
                        isRunning = false;
                        break;

                    default:
                        this._view.ShowError(InvalidOptionMessage);
                        this._view.FlushScreenWithKey();
                        break;
                }
            }
            while (isRunning);
        }

        /// <summary>
        /// Checks whether the inventory contains any products.
        /// </summary>
        /// <returns>True if the inventory contains products, otherwise false.</returns>
        private bool HasProducts()
        {
            if (this._services.IsInventoryEmpty())
            {
                this._view.ShowError(InventoryEmptyMessage);
                this._view.FlushScreenWithKey();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Adds a new product to the inventory.
        /// </summary>
        private void AddProduct()
        {
            string name;
            if (!this.GetValidProductName(out name))
            {
                return;
            }

            decimal price;
            if (!this.GetValidPrice(out price))
            {
                return;
            }

            int quantity;
            if (!this.GetValidQuantity(out quantity))
            {
                return;
            }

            this._services.AddProduct(name, price, quantity);
            this._view.ShowSuccess(ProductAddedMessage);
            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Updates the details of an existing product.
        /// </summary>
        private void EditProduct()
        {
            if (!this.HasProducts())
            {
                return;
            }

            Product? product = this.GetValidProduct("edit");
            if (product == null)
            {
                return;
            }

            bool isRunning = true;
            do
            {
                string option = this._view.ShowEditMenu();
                switch (option.Trim().ToUpper())
                {
                    case "A":
                        string name;
                        if (!this.GetValidProductName(out name))
                        {
                            break;
                        }

                        this._services.EditName(product, name);
                        this._view.ShowSuccess(NameUpdatedMessage);
                        break;

                    case "B":
                        decimal price;
                        if (!this.GetValidPrice(out price))
                        {
                            break;
                        }

                        this._services.EditPrice(product, price);
                        this._view.ShowSuccess(PriceUpdatedMessage);
                        break;

                    case "C":
                        isRunning = false;
                        this._view.ShowSuccess(EditCompletedMessage);
                        break;

                    default:

                        this._view.ShowError(InvalidOptionMessage);
                        if (!this._view.AskRetry())
                        {
                            isRunning = false;
                        }

                        break;
                }
            }
            while (isRunning);
            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Searches for a product by its ID.
        /// </summary>
        private void SearchProduct()
        {
            if (!this.HasProducts())
            {
                return;
            }

            Product? product = this.GetValidProduct("search");
            if (product == null)
            {
                return;
            }

            this._view.DisplaySingleProduct(product);
            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Displays all products in the inventory.
        /// </summary>
        private void ViewAllProducts()
        {
            if (!this.HasProducts())
            {
                return;
            }

            List<Product> products = this._services.GetAllProducts();
            this._view.DisplayProducts(products);
            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Deletes a product from the inventory.
        /// </summary>
        private void DeleteProduct()
        {
            if (!this.HasProducts())
            {
                return;
            }

            Product? product = this.GetValidProduct("delete");
            if (product == null)
            {
                return;
            }

            this._view.DisplaySingleProduct(product);
            if (this._view.ConfirmDelete())
            {
                this._services.DeleteProduct(product);
                this._view.ShowSuccess(ProductDeletedMessage);
            }

            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Increases the stock quantity of a product.
        /// </summary>
        private void RestockProduct()
        {
            if (!this.HasProducts())
            {
                return;
            }

            Product? product = this.GetValidProduct("restock");
            if (product == null)
            {
                return;
            }

            this._view.DisplaySingleProduct(product!);

            int quantity;
            if (!this.GetValidQuantity(out quantity))
            {
                return;
            }

            this._services.RestockProduct(product, quantity);
            this._view.ShowSuccess(StockRestockedMessage);
            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Reduces the stock quantity of a product.
        /// </summary>
        private void ReduceStock()
        {
            if (!this.HasProducts())
            {
                return;
            }

            Product? product = this.GetValidProduct("reduce stock");
            if (product == null)
            {
                return;
            }

            this._view.DisplaySingleProduct(product!);
            int quantity;
            if (!this.GetValidQuantity(out quantity))
            {
                return;
            }

            bool isReduced = this._services.ReduceStock(product, quantity);
            if (isReduced)
            {
                this._view.ShowSuccess(StockReducedMessage);
            }
            else
            {
                this._view.ShowError(InsufficientStockMessage);
            }

            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Displays all products that are low in stock.
        /// </summary>
        private void ViewLowStockProducts()
        {
            if (!this.HasProducts())
            {
                return;
            }

            List<Product> products = this._services.GetLowStockProducts();
            if (products.Count == 0)
            {
                this._view.ShowError(NoLowStockProductsMessage);
            }
            else
            {
                this._view.DisplayProducts(products);
            }

            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Reads and validates a product ID.
        /// </summary>
        /// <param name="operation">The operation being performed.</param>
        /// <returns>The matching proudct if found, otherwise null.</returns>
        private Product? GetValidProduct(string operation)
        {
            do
            {
                string productId = this._view.ReadProductId(operation);
                if (!this._helper.IsValidProductId(productId))
                {
                    this._view.ShowError(InvalidProductIdMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return null;
                    }

                    continue;
                }

                Product? product = this._services.SearchProduct(productId);
                if (product == null)
                {
                    this._view.ShowError(ProductNotFoundMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return null;
                    }

                    continue;
                }

                return product;
            }
            while (true);
        }

        /// <summary>
        /// Reads and validates a product name.
        /// </summary>
        /// <param name="name">The validated product name.</param>
        /// <returns>True if the product name is valid, otherwise false.</returns>
        private bool GetValidProductName(out string name)
        {
            do
            {
                name = this._view.ReadProductName();
                if (!this._helper.IsValidName(name))
                {
                    this._view.ShowError(InvalidNameMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return false;
                    }

                    continue;
                }

                name = name.Trim();
                return true;
            }
            while (true);
        }

        /// <summary>
        /// Reads and validates a product price.
        /// </summary>
        /// <param name="price">The validated product price.</param>
        /// <returns>True if the product price is valid, otherwise false.</returns>
        private bool GetValidPrice(out decimal price)
        {
            do
            {
                string input = this._view.ReadProductPrice();
                if (!this._helper.IsValidPrice(input, out price))
                {
                    this._view.ShowError(InvalidPriceMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return false;
                    }

                    continue;
                }

                return true;
            }
            while (true);
        }

        /// <summary>
        /// Reads and validates a product quantity.
        /// </summary>
        /// <param name="quantity">The validated product quantity.</param>
        /// <returns>True if the product quantity is valid, otherwise false.</returns>
        private bool GetValidQuantity(out int quantity)
        {
            do
            {
                string input = this._view.ReadProductQuantity();
                if (!this._helper.IsValidQuantity(input, out quantity))
                {
                    this._view.ShowError(InvalidQuantityMessage);
                    if (!this._view.AskRetry())
                    {
                        this._view.FlushScreen();
                        return false;
                    }

                    continue;
                }

                return true;
            }
            while (true);
        }
    }
}