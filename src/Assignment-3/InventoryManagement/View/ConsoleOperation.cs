using ConsoleTables;
using InventoryManagement.Enum;
using InventoryManagement.Model;

namespace InventoryManagement.View
{
    /// <summary>
    /// Handles all console input and output operations.
    /// </summary>
    internal class ConsoleOperation
    {
        /// <summary>
        /// Displays the main menu and reads the user's choice.
        /// </summary>
        /// <returns>The selected menu option.</returns>
        public string ShowMainMenu()
        {
            Console.Write("Inventory Management System\n");
            Console.Write("========================================================\n");
            Console.Write("\nSelect an option:\n");
            Console.Write("[A] Add Product\n");
            Console.Write("[B] Edit Product\n");
            Console.Write("[C] Search Product\n");
            Console.Write("[D] View All Products\n");
            Console.Write("[E] Delete Product\n");
            Console.Write("[F] Restock Product\n");
            Console.Write("[G] Reduce Stock\n");
            Console.Write("[H] View Low Stock Products\n");
            Console.Write("[I] Exit\n");
            Console.Write("\nEnter your choice: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// This method displays the search menu and reads the user's choice for searching <see cref="Model.Product"/>.
        /// </summary>
        /// <returns>The choice entered by the user.</returns>
        public string ShowSearchMenu()
        {
            Console.Write("\nSearch Product\n");
            Console.Write("[A] Search by Product ID\n");
            Console.Write("[B] Search by Product Name\n");
            Console.Write("[C] Back\n");
            Console.Write("\nChoose an option: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Reads the <see cref="Model.Product"/> name.
        /// </summary>
        /// <param name="operation">The operation being performed.</param>
        /// <returns>The <see cref="Model.Product"/> name entered by the user.</returns>
        public string ReadProductName(string operation)
        {
            Console.Write($"Enter product name to {operation}: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Reads the <see cref="Model.Product"/> price.
        /// </summary>
        /// <returns>The <see cref="Model.Product"/> price entered by the user.</returns>
        public string ReadProductPrice()
        {
            Console.Write("Enter Product Price: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Reads the <see cref="Model.Product"/> quantity.
        /// </summary>
        /// <returns>The <see cref="Model.Product"/> quantity entered by the user.</returns>
        public string ReadProductQuantity()
        {
            Console.Write("Enter Product Quantity: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Reads the <see cref="Model.Product"/> ID.
        /// </summary>
        /// <param name="operation">The operation being performed.</param>
        /// <returns>The <see cref="Model.Product"/> ID entered by the user.</returns>
        public string ReadProductId(string operation)
        {
            Console.Write($"Enter Product ID to {operation}: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
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
            Console.Write(message + "\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the invalid error message with different fields.
        /// </summary>
        /// <param name="fieldName">The field name to be printed as an invalid message.</param>
        public void ShowInvalidMessage(string fieldName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Enter a valid {fieldName}.\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays all <see cref="Model.Product"/>.
        /// </summary>
        /// <param name="products">The <see cref="Model.Product"/> list to display.</param>
        public void DisplayProducts(List<Product> products)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nProduct List: \n\n");
            Console.ResetColor();
            var table = new ConsoleTable("Serial Number", "Product Id", "Product Name", "Price", "Stock");
            for (int i = 0; i < products.Count; i++)
            {
                table.AddRow(i + 1, products[i].ProductId, products[i].Name, products[i].Price, products[i].Quantity);
            }

            table.Write(Format.MarkDown);
        }

        /// <summary>
        /// Displays a single <see cref="Model.Product"/>.
        /// </summary>
        /// <param name="product">The specific <see cref="Model.Product"/> to display.</param>
        public void DisplaySingleProduct(Product product)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nProduct Found!\n\n");
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
            Console.Write("\n[A] Edit Name\n");
            Console.Write("[B] Edit Price\n");
            Console.Write("[C] Exit\n");
            Console.Write("Choose an option: ");

            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        /// <summary>
        /// Asks the user to confirm <see cref="Model.Product"/> deletion.
        /// </summary>
        /// <returns>True if the user confirms the deletion, otherwise false.</returns>
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
                        Console.Write("Please enter Y or N.\n");
                        break;
                }
            }
        }

        /// <summary>
        /// Clears the console.
        /// </summary>
        public void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Waits for a key press before clearing the console.
        /// </summary>
        public void ClearScreenWithKey()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\nPress any key to continue...\n");
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
                        Console.Write("Please enter Y or N.\n");
                        break;
                }
            }
        }
    }
}