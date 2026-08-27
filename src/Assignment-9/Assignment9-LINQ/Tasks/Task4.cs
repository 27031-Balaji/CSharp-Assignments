using System.Diagnostics;
using Assignment9.Data;
using Assignment9.Model;

namespace Assignment9.Tasks
{
    /// <summary>
    /// Implements the Task 4 of the LINQ assignment.
    /// </summary>
    public static class Task4
    {
        /// <summary>
        /// The Run method runs the program with the LINQ query.
        /// </summary>
        /// <param name="context">The database context to be used for LINQ operations.</param>
        public static void Run(SampleDatabaseContext context)
        {
            Console.WriteLine("\n========== TASK 4 ==========\n");

            Stopwatch stopwatch = Stopwatch.StartNew();

            List<Product> unoptimizedBooks = context.Products
                .OrderByDescending(product => product.Price)
                .Where(product => product.Category == "Books")
                .ToList();

            Console.WriteLine("Unoptimized Query:");
            Console.WriteLine("Sort all products first, then filter books and materialize into a list.\n");

            foreach (var product in unoptimizedBooks)
            {
                Console.WriteLine($"Product: {product.ProductName}, Price: ${product.Price}");
            }

            stopwatch.Stop();
            double unoptimizedTime = stopwatch.Elapsed.TotalMilliseconds;
            Console.WriteLine($"\nExecution Time: {unoptimizedTime} ms\n");
            Console.WriteLine("----------------------------------------------\n");

            stopwatch.Restart();

            IEnumerable<Product> optimizedBooks = context.Products
                .Where(product => product.Category == "Books")
                .OrderByDescending(product => product.Price);

            Console.WriteLine("Optimized Query:");
            Console.WriteLine("Filter books first, then sort only books and don't materialize to make use of deferred execution.\n");

            foreach (var product in optimizedBooks)
            {
                Console.WriteLine($"Product: {product.ProductName}, Price: ${product.Price}");
            }

            stopwatch.Stop();
            double optimizedTime = stopwatch.Elapsed.TotalMilliseconds;
            Console.WriteLine($"\nExecution Time: {optimizedTime} ms");
            Console.WriteLine("----------------------------------------------\n");
        }
    }
}