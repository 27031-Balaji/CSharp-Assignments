namespace InventoryManagement.Helper
{
    /// <summary>
    /// Validates product information to check whether the input entered is right or not.
    /// </summary>
    internal class ProductHelper
    {
        /// <summary>
        /// Determines whether a product name is valid.
        /// </summary>
        /// <param name="name">The product name.</param>
        /// <returns>True if the product name is valid, otherwise false.</returns>
        public bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        /// <summary>
        /// Determines whether a product price is valid.
        /// </summary>
        /// <param name="input">The price entered by the user.</param>
        /// <param name="price">The validated product price.</param>
        /// <returns>True if the product price is valid, otherwise false.</returns>
        public bool IsValidPrice(string input, out decimal price)
        {
            return decimal.TryParse(input, out price) && price > 0;
        }

        /// <summary>
        /// Determines whether a product quantity is valid.
        /// </summary>
        /// <param name="input">The quantity entered by the user.</param>
        /// <param name="quantity">The validated product quantity.</param>
        /// <returns>True if the product quantity is valid, otherwise false.</returns>
        public bool IsValidQuantity(string input, out int quantity)
        {
            return int.TryParse(input, out quantity) && quantity >= 0;
        }

        /// <summary>
        /// Determines whether a product ID is valid.
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <returns>True if the product ID is valid, otherwise false.</returns>
        public bool IsValidProductId(string productId)
        {
            return !string.IsNullOrWhiteSpace(productId) && productId.Length == 12 && productId.All(char.IsLetterOrDigit);
        }
    }
}