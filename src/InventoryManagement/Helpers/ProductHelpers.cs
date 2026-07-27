namespace InventoryManagement.Helpers
{
    internal class ProductHelper
    {
        public bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        public bool IsValidPrice(string input, out decimal price)
        {
            return decimal.TryParse(input, out price) && price > 0;
        }

        public bool IsValidQuantity(string input, out int quantity)
        {
            return int.TryParse(input, out quantity) && quantity >= 0;
        }
    }
}