using Assignment9.Data;
using Assignment9.Model;
using Assignment9.Utils;
using ConsoleTables;

namespace Assignment9.Tasks
{
    /// <summary>
    /// Demonstrates the QueryBuilder utility.
    /// </summary>
    public static class Task5
    {
        /// <summary>
        /// Runs the QueryBuilder Task 5.
        /// </summary>
        /// <param name="context">The database context to be used.</param>
        public static void Run(SampleDatabaseContext context)
        {
            Console.WriteLine("\n========== TASK 5 ==========\n");

            List<Product> filteredProducts = new QueryBuilder<Product>(context.Products)
                .Filter(product => product.Price > 200)
                .SortBy(product => product.Price)
                .Execute();

            Console.WriteLine("Products With Price Greater Than 200:\n");

            ConsoleTable filteredProductsTable = new ConsoleTable("Product ID", "Product Name", "Category", "Price");

            foreach (Product product in filteredProducts)
            {
                filteredProductsTable.AddRow(
                    product.ProductId,
                    product.ProductName,
                    product.Category,
                    product.Price);
            }

            filteredProductsTable.Write(Format.MarkDown);

            var productSuppliers = new QueryBuilder<Product>(context.Products)
                .Filter(product => product.Price > 200)
                .SortBy(product => product.Price)
                .Join(
                    context.Suppliers,
                    product => product.ProductId,
                    supplier => supplier.ProductId,
                    (product, supplier) => new
                    {
                        product.ProductId,
                        product.ProductName,
                        product.Category,
                        product.Price,
                        supplier.SupplierId,
                        supplier.SupplierName,
                    })
                .Execute();

            Console.WriteLine("\nProducts With Price Greater Than 200 And Their Suppliers:\n");

            ConsoleTable productSupplierTable = new ConsoleTable("Product ID", "Product Name", "Category", "Price", "Supplier ID", "Supplier Name");
            foreach (var item in productSuppliers)
            {
                productSupplierTable.AddRow(
                    item.ProductId,
                    item.ProductName,
                    item.Category,
                    item.Price,
                    item.SupplierId,
                    item.SupplierName);
            }

            productSupplierTable.Write(Format.MarkDown);
        }
    }
}