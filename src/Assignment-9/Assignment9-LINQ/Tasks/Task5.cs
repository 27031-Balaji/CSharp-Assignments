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

            List<Product> expensiveProducts = new QueryBuilder<Product>(context.Products)
                .Filter("Price", 200, FilterCondition.GreaterThanOrEqualTo)
                .SortBy(product => product.Price)
                .Execute();

            Console.WriteLine("Products With Price >= 200:\n");
            foreach (Product product in expensiveProducts)
            {
                Console.WriteLine($"{product.ProductName} - {product.Price}");
            }

            Console.WriteLine("\n------------------------------------\n");

            List<Product> affordableProducts = new QueryBuilder<Product>(context.Products)
                .Filter("Price", 200, FilterCondition.LessThanOrEqualTo)
                .SortBy(product => product.Price)
                .Execute();

            Console.WriteLine("Products With Price <= 200:\n");
            foreach (Product product in affordableProducts)
            {
                Console.WriteLine($"{product.ProductName} - {product.Price}");
            }

            Console.WriteLine("\n------------------------------------\n");

            List<Product> containsProducts = new QueryBuilder<Product>(context.Products)
                .Filter("ProductName", "C#", FilterCondition.Contains)
                .Execute();

            Console.WriteLine("Products Where Name Contains 'C#':\n");
            foreach (Product product in containsProducts)
            {
                Console.WriteLine(product.ProductName);
            }

            Console.WriteLine("\n------------------------------------\n");

            List<Product> startsWithProducts = new QueryBuilder<Product>(context.Products)
                .Filter("ProductName", "S", FilterCondition.StartsWith)
                .Execute();

            Console.WriteLine("Products Where Name Starts With 'S':\n");
            foreach (Product product in startsWithProducts)
            {
                Console.WriteLine(product.ProductName);
            }

            Console.WriteLine("\n------------------------------------\n");

            List<Product> endsWithProducts = new QueryBuilder<Product>(context.Products)
                .Filter("ProductName", "r", FilterCondition.EndsWith)
                .Execute();

            Console.WriteLine("Products Where Name Ends With 'r':\n");
            foreach (Product product in endsWithProducts)
            {
                Console.WriteLine(product.ProductName);




            }

            Console.WriteLine("\n------------------------------------\n");

            var productSuppliers = new QueryBuilder<Product>(context.Products)
                .Filter("Price", 200, FilterCondition.GreaterThanOrEqualTo)
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

            Console.WriteLine("Products With Price >= 200 And Their Suppliers:\n");

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