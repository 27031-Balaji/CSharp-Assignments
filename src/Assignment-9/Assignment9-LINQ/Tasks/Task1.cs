using Assignment9.Data;
using ConsoleTables;

namespace Assignment9.Tasks
{
    /// <summary>
    /// Implements the Task 1 of the LINQ assignment.
    /// </summary>
    public static class Task1
    {
        /// <summary>
        /// The Run method runs the program with the LINQ query.
        /// </summary>
        /// <param name="context">The database context to be used for LINQ operations.</param>
        public static void Run(SampleDatabaseContext context)
        {
            Console.WriteLine("\n========== TASK 1 ==========\n");

            IEnumerable<(string ProductName, decimal Price)> filteredProducts = context.Products
                .Where(product => product.Category == "Electronics" && product.Price > 500)
                .Select(product => (product.ProductName, product.Price));

            Console.WriteLine("Electronics Products Above $500:\n");

            ConsoleTable filteredProductsTable = new ConsoleTable("Product Name", "Price");
            foreach ((string productName, decimal price) in filteredProducts)
            {
                filteredProductsTable.AddRow(productName, price);
            }

            filteredProductsTable.Write(Format.MarkDown);

            IEnumerable<(string ProductName, decimal Price)> sortedProducts = filteredProducts
                .OrderByDescending(product => product.Price);

            Console.WriteLine("\nElectronics Products Above $500 in sorted order by price (Descending):\n");

            ConsoleTable sortedFilteredProductsTable = new ConsoleTable("Product Name", "Price");
            foreach ((string productName, decimal price) in sortedProducts)
            {
                sortedFilteredProductsTable.AddRow(productName, price);
            }

            sortedFilteredProductsTable.Write(Format.MarkDown);

            decimal averagePrice = filteredProducts
                .Average(product => product.Price);

            Console.WriteLine($"\nAverage Price of Electronics Products Above $500: ${averagePrice}");
        }
    }
}