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

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            List<Product> unoptimizedBooks = context.Products
                .OrderByDescending(product => product.Price)
                .Where(product => product.Category == "Books")
                .ToList();

            stopWatch.Stop();

            Console.WriteLine("Unoptimized Query:");
            Console.WriteLine("Sort all products first, then filter books and materialize into a list.\n");

            foreach (Product product in unoptimizedBooks)
            {
                Console.WriteLine($"Product: {product.ProductName}, Price: ${product.Price}");
            }

            double unoptimizedTime = stopWatch.Elapsed.TotalMilliseconds;
            Console.WriteLine($"\nExecution Time: {unoptimizedTime} ms\n");
            Console.WriteLine("----------------------------------------------\n");

            stopWatch.Restart();

            IEnumerable<Product> optimizedBooks = context.Products
                .Where(product => product.Category == "Books")
                .OrderByDescending(product => product.Price);

            stopWatch.Stop();

            Console.WriteLine("Optimized Query:");
            Console.WriteLine("Filter books first, then sort only books.\n");

            foreach (Product product in optimizedBooks)
            {
                Console.WriteLine($"Product: {product.ProductName}, Price: ${product.Price}");
            }

            double optimizedTime = stopWatch.Elapsed.TotalMilliseconds;
            Console.WriteLine($"\nExecution Time: {optimizedTime} ms");
            Console.WriteLine("----------------------------------------------\n");
        }
    }
}