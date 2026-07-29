using ConsoleTables;
using InventoryManagement.Enums;
using InventoryManagement.Models;

namespace InventoryManagement.View
{
    /// <summary>
    /// Handles all console input and output operations.
    /// </summary>
    internal class ConsoleOperations
    {
        /// <summary>
        /// Displays the main menu and reads the user's choice.
        /// </summary>
        /// <returns>The selected menu option.</returns>
        public string ShowMainMenu()
        {
            Console.WriteLine("Inventory Management System");
            Console.WriteLine("========================================================");
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("[A] Add Product");
            Console.WriteLine("[B] Edit Product");
            Console.WriteLine("[C] Search Product");
            Console.WriteLine("[D] View All Products");
            Console.WriteLine("[E] Delete Product");
            Console.WriteLine("[F] Restock Product");
            Console.WriteLine("[G] Reduce Stock");
            Console.WriteLine("[H] View Low Stock Products");
            Console.WriteLine("[I] Exit");
            Console.Write("\nEnter your choice: ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Reads the product name.
        /// </summary>
        /// <returns>The product name entered by the user.</returns>
        public string ReadProductName()
        {
            Console.Write("Enter Product Name: ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Reads the product price.
        /// </summary>
        /// <returns>The product price entered by the user.</returns>
        public string ReadProductPrice()
        {
            Console.Write("Enter Product Price: ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Reads the product quantity.
        /// </summary>
        /// <returns>The product quantity entered by the user.</returns>
        public string ReadProductQuantity()
        {
            Console.Write("Enter Product Quantity: ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Reads the product ID.
        /// </summary>
        /// <param name="operation">The operation being performed.</param>
        /// <returns>The product ID entered by the user.</returns>
        public string ReadProductId(string operation)
        {
            Console.Write($"Enter Product ID to {operation}: ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// This method is used to show success message with Green color.
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        /// <param name="type">The message type (Success, Error or Info).</param>
        public void ShowMessage(string message, MessageType type)
        {
            Console.ForegroundColor = type switch
            {
                MessageType.Success => ConsoleColor.Green,
                MessageType.Error => ConsoleColor.Red,
                MessageType.Info => ConsoleColor.Cyan,
                _ => ConsoleColor.White
            };
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays all products.
        /// </summary>
        /// <param name="products">The product list to display.</param>
        public void DisplayProducts(List<Product> products)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nProduct List: \n");
            Console.ResetColor();
            var table = new ConsoleTable("Serial Number", "Product Id", "Product Name", "Price", "Stock");
            for (int i = 0; i < products.Count; i++)
            {
                table.AddRow(i + 1, products[i].ProductId, products[i].Name, products[i].Price, products[i].Quantity);
            }

            table.Write(Format.MarkDown);
        }

        /// <summary>
        /// Displays a single product.
        /// </summary>
        /// <param name="product">The specific product to display.</param>
        public void DisplaySingleProduct(Product product)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nProduct Found!\n");
            Console.ResetColor();
            var table = new ConsoleTable("Product Id", "Product Name", "Price", "Stock");
            table.AddRow(product.ProductId, product.Name, product.Price, product.Quantity);

            table.Write(Format.MarkDown);
        }

        /// <summary>
        /// Displays the edit menu and reads the user's choice.
        /// </summary>
        /// <returns>The selected menu option.</returns>
        public string ShowEditMenu()
        {
            Console.WriteLine("\n[A] Edit Name");
            Console.WriteLine("[B] Edit Price");
            Console.WriteLine("[C] Exit");
            Console.Write("Choose an option: ");

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Asks the user to confirm product deletion.
        /// </summary>
        /// <returns>True if the user confirms, otherwise false.</returns>
        public bool ConfirmDelete()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nDelete this product? (Y/N): ");
                Console.ResetColor();
                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice.Trim().ToUpper())
                {
                    case "Y":
                        return true;

                    case "N":
                        return false;

                    default:
                        Console.WriteLine("Please enter Y or N.");
                        break;
                }
            }
        }

        /// <summary>
        /// Clears the console.
        /// </summary>
        public void FlushScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Waits for a key press before clearing the console.
        /// </summary>
        public void FlushScreenWithKey()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.ResetColor();
        }

        /// <summary>
        /// Asks the user whether to retry the current operation.
        /// </summary>
        /// <returns>True if the user wants to retry, otherwise false.</returns>
        public bool AskRetry()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nTry again? (Y/N): ");
                Console.ResetColor();
                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice.Trim().ToUpper())
                {
                    case "Y":
                        return true;

                    case "N":
                        return false;

                    default:
                        Console.WriteLine("Please enter Y or N.");
                        break;
                }
            }
        }
    }
}