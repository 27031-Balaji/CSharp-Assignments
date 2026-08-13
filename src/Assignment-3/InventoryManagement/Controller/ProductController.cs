using InventoryManagement.Enum;
using InventoryManagement.Exception;
using InventoryManagement.Helper;
using InventoryManagement.Model;
using InventoryManagement.Service;
using InventoryManagement.View;

namespace InventoryManagement.Controller
{
    /// <summary>
    /// Coordinates user interactions and application flow for inventory management.
    /// </summary>
    internal class ProductController
    {
        private readonly ProductService _services;
        private readonly ProductHelper _helper;
        private readonly ConsoleOperation _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="services">The product service.</param>
        /// <param name="helper">The product helper.</param>
        /// <param name="view">The console view.</param>
        public ProductController(ProductService services, ProductHelper helper, ConsoleOperation view)
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
            while (isRunning)
            {
                string menuChoice = this._view.ShowMainMenu();
                switch (menuChoice.ToUpper())
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
                        this._view.ShowMessage(ConsoleMessages.ExitMessage, MessageType.Info);
                        Thread.Sleep(1000);
                        isRunning = false;
                        break;

                    default:
                        this._view.ShowInvalidMessage("option");
                        break;
                }
            }
        }

        /// <summary>
        /// Adds a new <see cref="Product"/> to the inventory.
        /// </summary>
        private void AddProduct()
        {
            if (!this.GetValidProductName(out string name, "add"))
            {
                return;
            }

            if (!this.GetValidProductPrice(out decimal price))
            {
                return;
            }

            if (!this.GetValidProductQuantity(out int quantity))
            {
                return;
            }

            this._services.AddProduct(name, price, quantity);
            this._view.ShowMessage(ConsoleMessages.ProductAddedMessage, MessageType.Success);
            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Updates the details of an existing <see cref="Product"/> by its ID.
        /// </summary>
        private void EditProduct()
        {
            if (!this.HasProducts())
            {
                return;
            }

            Product? product = this.GetValidProductWithId("edit");
            if (product == null)
            {
                return;
            }

            bool isRunning = true;
            while (isRunning)
            {
                string editChoice = this._view.ShowEditMenu();
                switch (editChoice.ToUpper())
                {
                    case "A":
                        this.EditProductName(product);
                        break;

                    case "B":
                        this.EditProductPrice(product);
                        break;

                    case "C":
                        isRunning = false;
                        this._view.ShowMessage(ConsoleMessages.EditCompletedMessage, MessageType.Success);
                        break;

                    default:
                        this._view.ShowInvalidMessage("option");
                        if (!this._view.AskRetry())
                        {
                            isRunning = false;
                        }

                        break;
                }
            }

            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Searches the details of an existing <see cref="Product"/> by its ID or name.
        /// </summary>
        private void SearchProduct()
        {
            if (!this.HasProducts())
            {
                return;
            }

            string searchChoice = this._view.ShowSearchMenu();
            switch (searchChoice.ToUpper())
            {
                case "A":
                    this.SearchProductById();
                    break;

                case "B":
                    this.SearchProductByName();
                    break;

                case "C":
                    this._view.ClearScreen();
                    return;

                default:
                    this._view.ShowInvalidMessage("option");
                    if (!this._view.AskRetry())
                    {
                        this._view.ClearScreen();
                        return;
                    }

                    break;
            }
        }

        /// <summary>
        /// Searches for a <see cref="Product"/> by its ID.
        /// </summary>
        private void SearchProductById()
        {
            Product? product = this.GetValidProductWithId("search");
            if (product == null)
            {
                return;
            }

            this._view.DisplaySingleProduct(product);
            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Searches for a <see cref="Product"/> by its name.
        /// </summary>
        private void SearchProductByName()
        {
            bool continueSearch = true;
            while (continueSearch)
            {
                if (!this.GetValidProductName(out string name, "search"))
                {
                    return;
                }

                try
                {
                    List<Product> products = this._services.SearchProductsByName(name);
                    this._view.DisplayProducts(products);
                    this._view.ClearScreenWithKey();
                    continueSearch = false;
                }
                catch (ProductNotFoundException ex)
                {
                    this._view.ShowMessage(ex.Message, MessageType.Error);
                    continueSearch = this._view.AskRetry();
                    if (!continueSearch)
                    {
                        this._view.ClearScreen();
                    }
                }
            }
        }

        /// <summary>
        /// Displays all <see cref="Product"/> in the inventory.
        /// </summary>
        private void ViewAllProducts()
        {
            if (!this.HasProducts())
            {
                return;
            }

            List<Product> products = this._services.GetAllProducts();
            this._view.DisplayProducts(products);
            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Deletes a <see cref="Product"/> from the inventory by its ID.
        /// </summary>
        private void DeleteProduct()
        {
            if (!this.HasProducts())
            {
                return;
            }

            Product? product = this.GetValidProductWithId("delete");
            if (product == null)
            {
                return;
            }

            this._view.DisplaySingleProduct(product);
            if (this._view.ConfirmDelete())
            {
                try
                {
                    this._services.DeleteProduct(product);
                    this._view.ShowMessage(ConsoleMessages.ProductDeletedMessage, MessageType.Success);
                }
                catch (ProductNotFoundException ex)
                {
                    this._view.ShowMessage(ex.Message, MessageType.Error);
                }
            }

            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Increases the stock quantity of a <see cref="Product"/> using ID as the input.
        /// </summary>
        private void RestockProduct()
        {
            if (!this.HasProducts())
            {
                return;
            }

            Product? product = this.GetValidProductWithId("restock");
            if (product == null)
            {
                return;
            }

            this._view.DisplaySingleProduct(product);
            if (!this.GetValidProductQuantity(out int quantity))
            {
                return;
            }

            this._services.RestockProduct(product, quantity);
            this._view.ShowMessage(ConsoleMessages.StockRestockedMessage, MessageType.Success);
            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Reduces the stock quantity of a <see cref="Product"/> using ID as the input.
        /// </summary>
        private void ReduceStock()
        {
            if (!this.HasProducts())
            {
                return;
            }

            Product? product = this.GetValidProductWithId("reduce stock");
            if (product == null)
            {
                return;
            }

            this._view.DisplaySingleProduct(product);
            bool shouldRetry = true;
            while (shouldRetry)
            {
                if (!this.GetValidProductQuantity(out int quantity))
                {
                    return;
                }

                try
                {
                    this._services.ReduceStock(product, quantity);
                    this._view.ShowMessage(ConsoleMessages.StockReducedMessage, MessageType.Success);
                    this._view.ClearScreenWithKey();
                    shouldRetry = false;
                }
                catch (InsufficientStockException ex)
                {
                    this._view.ShowMessage(ex.Message, MessageType.Error);
                    shouldRetry = this._view.AskRetry();
                    if (!shouldRetry)
                    {
                        this._view.ClearScreen();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Displays all <see cref="Product"/> that are low in stock.
        /// </summary>
        private void ViewLowStockProducts()
        {
            if (!this.HasProducts())
            {
                return;
            }

            List<Product> lowStockProducts = this._services.GetLowStockProducts();
            if (lowStockProducts.Count == 0)
            {
                this._view.ShowMessage(ConsoleMessages.NoLowStockProductsMessage, MessageType.Info);
            }
            else
            {
                this._view.DisplayProducts(lowStockProducts);
            }

            this._view.ClearScreenWithKey();
        }

        /// <summary>
        /// Checks whether the inventory system has any <see cref="Product"/>.
        /// </summary>
        /// <returns>True if the inventory has any <see cref="Product"/>, otherwise false.</returns>
        private bool HasProducts()
        {
            try
            {
                this._services.ValidateInventory();

                return true;
            }
            catch (EmptyInventoryException ex)
            {
                this._view.ShowMessage(ex.Message, MessageType.Error);
                this._view.ClearScreenWithKey();

                return false;
            }
        }

        /// <summary>
        /// Reads and validates a <see cref="Product"/> ID by searching it in the repository.
        /// </summary>
        /// <param name="operation">The operation being performed.</param>
        /// <returns>The matching <see cref="Product"/> if found, otherwise null.</returns>
        private Product? GetValidProductWithId(string operation)
        {
            do
            {
                string productId = this._view.ReadProductId(operation);
                if (!this._helper.IsValidProductId(productId))
                {
                    continue;
                }

                try
                {
                    return this._services.SearchProductById(productId);
                }
                catch (ProductNotFoundException ex)
                {
                    this._view.ShowMessage(ex.Message, MessageType.Error);
                }
            }
            while (this.CanRetry("Product ID"));

            return null;
        }

        /// <summary>
        /// Reads and validates a <see cref="Product"/> name.
        /// </summary>
        /// <param name="name">The validated <see cref="Product"/> name.</param>
        /// <returns>True if the <see cref="Product"/> name is valid, otherwise false.</returns>
        private bool GetValidProductName(out string name, string operation)
        {
            name = string.Empty;
            do
            {
                name = this._view.ReadProductName(operation);
                if (this._helper.IsValidName(name))
                {
                    return true;
                }
            }
            while (this.CanRetry("name"));

            return false;
        }

        /// <summary>
        /// Reads and validates a <see cref="Product"/> price.
        /// </summary>
        /// <param name="price">The validated <see cref="Product"/> price.</param>
        /// <returns>True if the <see cref="Product"/> price is valid, otherwise false.</returns>
        private bool GetValidProductPrice(out decimal price)
        {
            price = 0;
            string input;
            do
            {
                input = this._view.ReadProductPrice();
                if (this._helper.IsValidPrice(input, out price))
                {
                    return true;
                }
            }
            while (this.CanRetry("price"));

            return false;
        }

        /// <summary>
        /// Reads and validates a <see cref="Product"/> quantity.
        /// </summary>
        /// <param name="quantity">The validated <see cref="Product"/> quantity.</param>
        /// <returns>True if the <see cref="Product"/> quantity is valid, otherwise false.</returns>
        private bool GetValidProductQuantity(out int quantity)
        {
            string input;
            quantity = 0;
            do
            {
                input = this._view.ReadProductQuantity();
                if (this._helper.IsValidQuantity(input, out quantity))
                {
                    return true;
                }
            }
            while (this.CanRetry("quantity"));

            return false;
        }

        /// <summary>
        /// Displays an invalid input message for the specified field and prompts the user to decide whether to retry the operation.
        /// </summary>
        /// <param name="field">The name of the field that contains invalid input.</param>
        /// <returns>True if the user chooses to retry, else false.</returns>
        private bool CanRetry(string field)
        {
            this._view.ShowInvalidMessage(field);
            bool shouldRetry = this._view.AskRetry();
            if (!shouldRetry)
            {
                this._view.ClearScreen();
            }

            return shouldRetry;
        }

        /// <summary>
        /// Edits the name of the <see cref="Product"/> searched.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> where the name is to be edited.</param>
        private void EditProductName(Product product)
        {
            if (!this.GetValidProductName(out string name, "edit"))
            {
                return;
            }

            this._services.EditProductName(product, name);
            this._view.ShowMessage(ConsoleMessages.NameUpdatedMessage, MessageType.Success);
        }

        /// <summary>
        /// Edits the price of the <see cref="Product"/> searched.
        /// </summary>
        /// <param name="product">The <see cref="Product"/> where the name is to be edited.</param>
        private void EditProductPrice(Product product)
        {
            if (!this.GetValidProductPrice(out decimal price))
            {
                return;
            }

            this._services.EditProductPrice(product, price);
            this._view.ShowMessage(ConsoleMessages.PriceUpdatedMessage, MessageType.Success);
        }
    }
}