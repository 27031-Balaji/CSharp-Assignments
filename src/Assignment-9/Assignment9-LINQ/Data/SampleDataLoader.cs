using Assignment9.Model;

namespace Assignment9.Data
{
    /// <summary>
    /// The SampleDataLoader is used to load the list of products, suppliers and orders for LINQ operations.
    /// </summary>
    public static class SampleDataLoader
    {
        /// <summary>
        /// The Load method loads the sample data to the lists.
        /// </summary>
        /// <returns>The list context to access all the lists.</returns>
        public static SampleDatabaseContext Load()
        {
            SampleDatabaseContext context = new SampleDatabaseContext();

            context.Products.AddRange(new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Laptop", Price = 1200, Category = "Electronics" },
                new Product { ProductId = 2, ProductName = "Smartphone", Price = 900, Category = "Electronics" },
                new Product { ProductId = 3, ProductName = "Television", Price = 1500, Category = "Electronics" },
                new Product { ProductId = 4, ProductName = "Office Chair", Price = 350, Category = "Furniture" },
                new Product { ProductId = 5, ProductName = "Study Desk", Price = 700, Category = "Furniture" },
                new Product { ProductId = 6, ProductName = "C# Programming Guide", Price = 80, Category = "Books" },
                new Product { ProductId = 7, ProductName = "Clean Code", Price = 450, Category = "Books" },
                new Product { ProductId = 8, ProductName = "Design Patterns", Price = 600, Category = "Books" },
                new Product { ProductId = 9, ProductName = "C# in Depth", Price = 350, Category = "Books" },
                new Product { ProductId = 10, ProductName = "Coffee Maker", Price = 400, Category = "Appliances" },
            });

            context.Suppliers.AddRange(new List<Supplier>
            {
                new Supplier { SupplierId = 1, SupplierName = "Tech World", ProductId = 1 },
                new Supplier { SupplierId = 2, SupplierName = "Digital Supplies", ProductId = 1 },
                new Supplier { SupplierId = 3, SupplierName = "Mobile Hub", ProductId = 2 },
                new Supplier { SupplierId = 4, SupplierName = "Smart Devices Ltd", ProductId = 2 },
                new Supplier { SupplierId = 5, SupplierName = "Vision Electronics", ProductId = 3 },
                new Supplier { SupplierId = 6, SupplierName = "Furniture House", ProductId = 4 },
                new Supplier { SupplierId = 7, SupplierName = "Office Furniture Ltd", ProductId = 4 },
                new Supplier { SupplierId = 8, SupplierName = "Wood Works", ProductId = 5 },
                new Supplier { SupplierId = 9, SupplierName = "Book Distributors", ProductId = 6 },
                new Supplier { SupplierId = 10, SupplierName = "Tech Books Publishing", ProductId = 6 },
                new Supplier { SupplierId = 11, SupplierName = "Code Readers Publishing", ProductId = 7 },
                new Supplier { SupplierId = 12, SupplierName = "Programming Books Ltd", ProductId = 8 },
                new Supplier { SupplierId = 13, SupplierName = "Software Publications", ProductId = 9 },
                new Supplier { SupplierId = 14, SupplierName = "Home Appliances Co", ProductId = 10 },
                new Supplier { SupplierId = 15, SupplierName = "Kitchen Supplies Ltd", ProductId = 10 },
            });

            context.Orders.AddRange(new List<Order>
            {
                new Order { OrderId = 1, OrderDate = new DateTime(2026, 1, 5), OrderStatus = "Completed" },
                new Order { OrderId = 2, OrderDate = new DateTime(2026, 1, 12), OrderStatus = "Pending" },
                new Order { OrderId = 3, OrderDate = new DateTime(2026, 2, 3), OrderStatus = "Completed" },
                new Order { OrderId = 4, OrderDate = new DateTime(2026, 2, 18), OrderStatus = "Cancelled" },
                new Order { OrderId = 5, OrderDate = new DateTime(2026, 3, 7), OrderStatus = "Completed" },
                new Order { OrderId = 6, OrderDate = new DateTime(2026, 3, 21), OrderStatus = "Pending" },
                new Order { OrderId = 7, OrderDate = new DateTime(2026, 4, 2), OrderStatus = "Completed" },
                new Order { OrderId = 8, OrderDate = new DateTime(2026, 4, 16), OrderStatus = "Processing" },
                new Order { OrderId = 9, OrderDate = new DateTime(2026, 5, 9), OrderStatus = "Completed" },
                new Order { OrderId = 10, OrderDate = new DateTime(2026, 5, 25), OrderStatus = "Cancelled" },
                new Order { OrderId = 11, OrderDate = new DateTime(2026, 6, 11), OrderStatus = "Completed" },
                new Order { OrderId = 12, OrderDate = new DateTime(2026, 6, 28), OrderStatus = "Pending" },
            });

            return context;
        }
    }
}