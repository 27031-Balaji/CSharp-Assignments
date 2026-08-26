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

        var electronicsAboveLimit = context.Products
                                .Where(product => product.Category == "Electronics" && product.Price > _minimalPrice)
                                .Select(product => new
                                {
                                    product.ProductName,
                                    product.Price,
                                })
                                .ToList();

        var sortedElectronicsAboveLimitByPrice = electronicsAboveLimit
                                        .OrderByDescending(product => product.Price);

        decimal averagePriceOfElectronicsAboveLimit = sortedElectronicsAboveLimitByPrice
                                                        .Average(product => product.Price);

        Console.Write($"Electronics products above ${_minimalPrice}\n\n");
        foreach (var product in electronicsAboveLimit)
        {
            Console.Write($"Product Name: {product.ProductName}, Price: {product.Price}\n");
        }

        Console.Write($"\nElectronics products above ${_minimalPrice} in descending order of price\n\n");
        foreach (var product in sortedElectronicsAboveLimitByPrice)
        {
            Console.Write($"Product Name: {product.ProductName}, Price: {product.Price}\n");
        }

        Console.WriteLine($"\nAverage price of electronics products above ${_minimalPrice}: ${averagePriceOfElectronicsAboveLimit}");

        Console.ReadKey();
    }
}