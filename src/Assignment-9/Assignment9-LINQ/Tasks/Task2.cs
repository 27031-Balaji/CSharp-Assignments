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
            Console.WriteLine("\n========== TASK 2 ==========\n");

            IEnumerable<IGrouping<string, Product>> groupedProducts = context.Products.GroupBy(product => product.Category);
            Console.WriteLine("Products Grouped By Category:\n");

            ConsoleTable categoryTable = new ConsoleTable("Category", "Product Count", "Most Expensive Product", "Price");
            foreach (IGrouping<string, Product> group in groupedProducts)
            {
                Product mostExpensiveProduct = group
                    .OrderByDescending(product => product.Price)
                    .First();

                categoryTable.AddRow(group.Key, group.Count(), mostExpensiveProduct.ProductName, mostExpensiveProduct.Price);
            }

            categoryTable.Write(Format.MarkDown);

            IEnumerable<(int ProductId,
                         string ProductName,
                         decimal ProductPrice,
                         string Category,
                         int SupplierId,
                         string SupplierName)> productSuppliers = context.Products
                                                        .Join(
                                                            context.Suppliers,
                                                            product => product.ProductId,
                                                            supplier => supplier.ProductId,
                                                            (product, supplier) =>
                                                                (product.ProductId,
                                                                 product.ProductName,
                                                                 product.Price,
                                                                 product.Category,
                                                                 supplier.SupplierId,
                                                                 supplier.SupplierName));

            Console.WriteLine("Products and Their Suppliers:\n");

            ConsoleTable productSuppliersTable = new ConsoleTable("Product ID", "Product Name", "Category", "Price", "Supplier ID", "Supplier Name");
            foreach ((int productId,
                      string productName,
                      decimal price,
                      string category,
                      int supplierId,
                      string supplierName) in productSuppliers)
            {
                productSuppliersTable.AddRow(productId, productName, price, category, supplierId, supplierName);
            }

            productSuppliersTable.Write(Format.MarkDown);
        }
    }
}