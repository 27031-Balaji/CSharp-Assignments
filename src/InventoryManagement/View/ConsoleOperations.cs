namespace InventoryManagement.View
{
    internal class ConsoleOperations
    {
        public void DisplayMenu()
        {
            Console.Clear();

            Console.WriteLine("==========================================");
            Console.WriteLine("      INVENTORY MANAGEMENT SYSTEM");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Edit Product");
            Console.WriteLine("3. Search Product");
            Console.WriteLine("4. View All Products");
            Console.WriteLine("5. Delete Product");
            Console.WriteLine("6. Restock Product");
            Console.WriteLine("7. Reduce Stock");
            Console.WriteLine("8. View Low Stock Products");
            Console.WriteLine("9. Exit");
            Console.WriteLine("==========================================");
        }

        public string ReadChoice()
        {
            Console.Write("Enter your choice: ");
            return Console.ReadLine() ?? string.Empty;
        }

        public string ReadProductName()
        {
            Console.Write("Enter Product Name: ");
            return Console.ReadLine() ?? string.Empty;
        }

        public string ReadProductPrice()
        {
            Console.Write("Enter Product Price: ");
            return Console.ReadLine() ?? string.Empty;
        }

        public string ReadProductQuantity()
        {
            Console.Write("Enter Product Quantity: ");
            return Console.ReadLine() ?? string.Empty;
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void FlushScreenWithKey()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}