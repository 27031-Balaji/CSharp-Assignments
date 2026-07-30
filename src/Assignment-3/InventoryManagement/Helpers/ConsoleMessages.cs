namespace InventoryManagement.Messages
{
    /// <summary>
    /// Contains all console messages used throughout the Inventory Management application.
    /// </summary>
    internal class ConsoleMessages
    {
        /// <summary>
        /// Message displayed when a new product is added successfully.
        /// </summary>
        public const string ProductAddedMessage = "Product added successfully.";

        /// <summary>
        /// Message displayed when a product name is updated successfully.
        /// </summary>
        public const string NameUpdatedMessage = "Product name updated successfully.";

        /// <summary>
        /// Message displayed when a product price is updated successfully.
        /// </summary>
        public const string PriceUpdatedMessage = "Product price updated successfully.";

        /// <summary>
        /// Message displayed when product editing is completed.
        /// </summary>
        public const string EditCompletedMessage = "Edit completed.";

        /// <summary>
        /// Message displayed when a product is deleted successfully.
        /// </summary>
        public const string ProductDeletedMessage = "Product deleted successfully.";

        /// <summary>
        /// Message displayed when product stock is restocked successfully.
        /// </summary>
        public const string StockRestockedMessage = "Stock updated successfully.";

        /// <summary>
        /// Message displayed when product stock is reduced successfully.
        /// </summary>
        public const string StockReducedMessage = "Stock reduced successfully.";

        /// <summary>
        /// Message displayed when no products are found with low stock levels.
        /// </summary>
        public const string NoLowStockProductsMessage = "No products are low in stock.";

        /// <summary>
        /// Message displayed when the application is exiting.
        /// </summary>
        public const string ExitMessage = "Exiting Application...";
    }
}