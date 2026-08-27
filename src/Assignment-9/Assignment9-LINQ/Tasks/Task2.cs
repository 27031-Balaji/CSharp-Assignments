using Assignment9.Data;

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
            Console.WriteLine();
            Console.WriteLine("========== TASK 2 ==========");
            Console.WriteLine();

            var groupedProducts = context.Products.GroupBy(product => product.Category);
            Console.WriteLine("Products Grouped By Category:");
            Console.WriteLine();

            foreach (var group in groupedProducts)
            {
                Console.WriteLine($"Category: {group.Key}");
                Console.WriteLine($"Number of Products: {group.Count()}");
                var mostExpensiveProduct = group
                    .OrderByDescending(product => product.Price)
                    .First();

                Console.WriteLine($"Most Expensive Product: {mostExpensiveProduct.ProductName}, " + $"Price: ${mostExpensiveProduct.Price}");
                Console.WriteLine();
            }

            var productSuppliers = context.Products
                .Join(
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

            Console.WriteLine("Products and Their Suppliers:");
            Console.WriteLine();

            foreach (var item in productSuppliers)
            {
                Console.WriteLine($"Product: {item.ProductName} " + $"(ID: {item.ProductId})");
                Console.WriteLine($"Price: ${item.Price}");
                Console.WriteLine($"Category: {item.Category}");
                Console.WriteLine($"Supplier: {item.SupplierName} " + $"(Supplier ID: {item.SupplierId})");
                Console.WriteLine();
            }
        }
    }
}