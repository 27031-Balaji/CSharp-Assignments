namespace InventoryManagement.Helper
{
    /// <summary>
    /// Contains all console messages used throughout the Inventory Management application.
    /// </summary>
    internal class ConsoleMessages
    {
        /// <summary>
        /// Message displayed when a new <see cref="Model.Product"/> is added successfully.
        /// </summary>
        public const string ProductAddedMessage = "Product added successfully.";

        /// <summary>
        /// Message displayed when a <see cref="Model.Product"/> name is updated successfully.
        /// </summary>
        public const string NameUpdatedMessage = "Product name updated successfully.";

        /// <summary>
        /// Message displayed when a <see cref="Model.Product"/> price is updated successfully.
        /// </summary>
        public const string PriceUpdatedMessage = "Product price updated successfully.";

        /// <summary>
        /// Message displayed when <see cref="Model.Product"/> editing is completed.
        /// </summary>
        public const string EditCompletedMessage = "Edit completed.";

        /// <summary>
        /// Message displayed when a <see cref="Model.Product"/> is deleted successfully.
        /// </summary>
        public const string ProductDeletedMessage = "Product deleted successfully.";

        /// <summary>
        /// Message displayed when <see cref="Model.Product"/> stock is restocked successfully.
        /// </summary>
        public const string StockRestockedMessage = "Stock updated successfully.";

        /// <summary>
        /// Message displayed when <see cref="Model.Product"/> stock is reduced successfully.
        /// </summary>
        public const string StockReducedMessage = "Stock reduced successfully.";

        /// <summary>
        /// Message displayed when no <see cref="Model.Product"/> are found with low stock levels.
        /// </summary>
        public const string NoLowStockProductsMessage = "No products are low in stock.";
    }
}