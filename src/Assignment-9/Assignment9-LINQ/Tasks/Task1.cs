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
            Console.WriteLine($"{Environment.NewLine}========== TASK 1 =========={Environment.NewLine}");

            var filteredProducts = context.Products
                .Where(product => product.Category.Equals("Electronics") && product.Price > 500)
                .Select(product => new
                {
                    product.ProductName,
                    product.Price,
                });

            Console.WriteLine($"Electronics Products Above $500:{Environment.NewLine}");

            ConsoleTable filteredProductsTable = new ConsoleTable("Product Name", "Price");
            foreach (var product in filteredProducts)
            {
                filteredProductsTable.AddRow(product.ProductName, product.Price);
            }

            filteredProductsTable.Write(Format.MarkDown);

            var sortedProducts = filteredProducts
                .OrderByDescending(product => product.Price);

            Console.WriteLine($"{Environment.NewLine}Electronics Products Above $500 in sorted order by price (Descending):{Environment.NewLine}");

            ConsoleTable sortedFilteredProductsTable = new ConsoleTable("Product Name", "Price");
            foreach (var product in sortedProducts)
            {
                sortedFilteredProductsTable.AddRow(product.ProductName, product.Price);
            }

            sortedFilteredProductsTable.Write(Format.MarkDown);

            decimal averagePrice = filteredProducts
                .Average(product => product.Price);

            Console.WriteLine($"{Environment.NewLine}Average Price of Electronics Products Above $500: ${averagePrice}");
        }
    }
}