using Assignment9.Model;

public static class Program
{
    private const decimal _minimalPrice = 500;

    public static void Main()
    {
        SampleDatabaseContext context = new SampleDatabaseContext();
        context.Products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Laptop", Price = 1200, Category = "Electronics" },
            new Product { ProductId = 2, ProductName = "Smartphone", Price = 800, Category = "Electronics" },
            new Product { ProductId = 3, ProductName = "Wireless Headphones", Price = 250, Category = "Electronics" },
            new Product { ProductId = 4, ProductName = "Gaming Console", Price = 600, Category = "Electronics" },
            new Product { ProductId = 10, ProductName = "4K TV", Price = 1500, Category = "Electronics" },
            new Product { ProductId = 5, ProductName = "Smart Watch", Price = 550, Category = "Electronics" },
            new Product { ProductId = 6, ProductName = "Office Chair", Price = 300, Category = "Furniture" },
            new Product { ProductId = 7, ProductName = "Dining Table", Price = 700, Category = "Furniture" },
            new Product { ProductId = 8, ProductName = "Refrigerator", Price = 950, Category = "Appliances" },
            new Product { ProductId = 9, ProductName = "Microwave Oven", Price = 200, Category = "Appliances" },
            new Product { ProductId = 11, ProductName = "Clean Code", Price = 45, Category = "Books" },
            new Product { ProductId = 12, ProductName = "Design Patterns", Price = 60, Category = "Books" },
            new Product { ProductId = 13, ProductName = "C# in Depth", Price = 55, Category = "Books" },
            new Product { ProductId = 14, ProductName = "The Pragmatic Programmer", Price = 50, Category = "Books" },
            new Product { ProductId = 15, ProductName = "Refactoring", Price = 65, Category = "Books" },
        };

        context.Suppliers = new List<Supplier>
        {
            new Supplier { SupplierId = 1, ProductId = 1, SupplierName = "Tech World" },
            new Supplier { SupplierId = 2, ProductId = 1, SupplierName = "Global Electronics" },
            new Supplier { SupplierId = 3, ProductId = 2, SupplierName = "Mobile Hub" },
            new Supplier { SupplierId = 4, ProductId = 2, SupplierName = "Digital Planet" },
            new Supplier { SupplierId = 5, ProductId = 3, SupplierName = "Audio Express" },
            new Supplier { SupplierId = 6, ProductId = 4, SupplierName = "Game Center" },
            new Supplier { SupplierId = 7, ProductId = 4, SupplierName = "Console Mart" },
            new Supplier { SupplierId = 8, ProductId = 5, SupplierName = "Smart Devices Ltd" },
            new Supplier { SupplierId = 9, ProductId = 6, SupplierName = "Furniture House" },
            new Supplier { SupplierId = 10, ProductId = 7, SupplierName = "Wood Craft" },
            new Supplier { SupplierId = 11, ProductId = 7, SupplierName = "Premium Furniture" },
            new Supplier { SupplierId = 12, ProductId = 8, SupplierName = "Home Appliances Inc" },
            new Supplier { SupplierId = 13, ProductId = 9, SupplierName = "Kitchen Essentials" },
            new Supplier { SupplierId = 14, ProductId = 10, SupplierName = "Vision Electronics" },
            new Supplier { SupplierId = 15, ProductId = 10, SupplierName = "Ultra HD Suppliers" },
            new Supplier { SupplierId = 16, ProductId = 11, SupplierName = "Book Store Ltd" },
            new Supplier { SupplierId = 17, ProductId = 12, SupplierName = "Knowledge Hub" },
            new Supplier { SupplierId = 18, ProductId = 13, SupplierName = "Readers Point" },
            new Supplier { SupplierId = 19, ProductId = 14, SupplierName = "Tech Books" },
            new Supplier { SupplierId = 20, ProductId = 15, SupplierName = "Programming Publications" },
        };


    }
}