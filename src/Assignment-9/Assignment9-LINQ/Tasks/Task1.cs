using Assignment9.Data;

namespace Assignment9.Tasks;

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
        Console.WriteLine();
        Console.WriteLine("========== TASK 1 ==========");
        Console.WriteLine();

        var filteredProducts = context.Products
            .Where(product => product.Category == "Electronics" && product.Price > 500)
            .Select(product => new
            {
                product.ProductName,
                product.Price,
            });

        Console.WriteLine("Electronics Products Above $500:");
        Console.WriteLine();

        foreach (var product in filteredProducts)
        {
            Console.WriteLine($"Product: {product.ProductName}, Price: ${product.Price}");
        }

        var sortedProducts = filteredProducts
            .OrderByDescending(product => product.Price);

        Console.WriteLine("Electronics Products Above $500 in sorted order by price (Descending):");
        Console.WriteLine();

        foreach (var product in sortedProducts)
        {
            Console.WriteLine($"Product: {product.ProductName}, Price: ${product.Price}");
        }

        decimal averagePrice = filteredProducts
            .Average(product => product.Price);

        Console.WriteLine();
        Console.WriteLine($"Average Price of Electronics Products Above $500: ${averagePrice}");
    }
}