using Assignment9.Data;
using Assignment9.Model;
using ConsoleTables;

namespace Assignment9.Tasks
{
    /// <summary>
    /// Implements the Task 2 of the LINQ assignment.
    /// </summary>
    public static class Task2
    {
        /// <summary>
        /// The Run method runs the program with the LINQ query.
        /// </summary>
        /// <param name="context">The database context to be used for LINQ operations.</param>
        public static void Run(SampleDatabaseContext context)
        {
            Console.WriteLine($"{Environment.NewLine}========== TASK 2 =========={Environment.NewLine}");

            var groupedProducts = context.Products.GroupBy(product => product.Category);
            Console.WriteLine($"Products Grouped By Category:{Environment.NewLine}");

            ConsoleTable categoryTable = new ConsoleTable("Category", "Product Count", "Most Expensive Product", "Price");
            foreach (var group in groupedProducts)
            {
                Product? mostExpensiveProduct = group.MaxBy(product => product.Price);
                categoryTable.AddRow(group.Key, group.Count(), mostExpensiveProduct?.ProductName ?? "None", mostExpensiveProduct?.Price ?? 0);
            }

            categoryTable.Write(Format.MarkDown);

            var productSuppliers = context.Products.Join(
                                    context.Suppliers,
                                    product => product.ProductId,
                                    supplier => supplier.ProductId,
                                    (product, supplier) => new
                                    {
                                        product.ProductId,
                                        product.ProductName,
                                        product.Price,
                                        product.Category,
                                        supplier.SupplierId,
                                        supplier.SupplierName,
                                    });

            Console.WriteLine($"{Environment.NewLine}Products and Their Suppliers:{Environment.NewLine}");

            ConsoleTable productSuppliersTable = new ConsoleTable("Product ID", "Product Name", "Category", "Price", "Supplier ID", "Supplier Name");
            foreach (var product in productSuppliers)
            {
                productSuppliersTable.AddRow(product.ProductId, product.ProductName, product.Price, product.Category, product.SupplierId, product.SupplierName);
            }

            productSuppliersTable.Write(Format.MarkDown);
        }
    }
}