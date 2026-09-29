namespace InventoryManagement.ConstantLiteral
{
    /// <summary>
    /// Contains the constants used in various parts of the application.
    /// </summary>
    internal static class Constant
    {
        /// <summary>
        /// Provides the low stock threshold for finding the low-stock products.
        /// </summary>
        internal const int LowStockThreshold = 5;

        /// <summary>
        /// Provides the maximum length of the product ID.
        /// </summary>
        internal const int IdLength = 8;

        /// <summary>
        /// Provides the minimum quantity to be entered for adding a product.
        /// </summary>
        internal const int MinimumQuantityForAddingProduct = 0;

        /// <summary>
        /// Provides the minimum quantity to be entered for stock changes like reduce stock or restock.
        /// </summary>
        internal const int MinimumQuantityForStockChange = 1;
    }
}