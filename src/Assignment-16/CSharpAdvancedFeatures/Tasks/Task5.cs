using CSharpAdvancedFeatures.Class;
using ConsoleTables;

namespace CSharpAdvancedFeatures.Tasks
{
    /// <summary>
    /// Represents a method that compares two <see cref="Product"/> instances and returns a value indicating their relative order.
    /// </summary>
    /// <param name="product1">The first <see cref="Product"/> to compare.</param>
    /// <param name="product2">The second <see cref="Product"/> to compare.</param>
    /// <returns>
    /// A signed integer that indicates the relative order of the products.
    /// Less than zero if product1 precedes product2
    /// Zero if they are equal
    /// Greater than zero if product1 follows product2.
    /// </returns>
    internal delegate int SortDelegate(Product product1, Product product2);

    /// <summary>
    /// Implements the Task 5 of the advanced features of C#.
    /// </summary>
    internal class Task5
    {
        /// <summary>
        /// Runs the application that contains the task.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Task 5 - Advanced Use of Delegates for Sorting\n");

            List<Product> products = this.MakeProductList();

            SortDelegate sortByNameDelegate = this.SortByName;
            SortDelegate sortByCategoryDelegate = this.SortByCategory;
            SortDelegate sortByPriceDelegate = this.SortByPrice;

            Console.WriteLine("Products Sorted By Name\n");
            this.SortAndDisplay(sortByNameDelegate, products);

            Console.WriteLine("\nProducts Sorted By Category\n");
            this.SortAndDisplay(sortByCategoryDelegate, products);

            Console.WriteLine("\nProducts Sorted By Price\n");
            this.SortAndDisplay(sortByPriceDelegate, products);
        }

        /// <summary>
        /// Sorts the specified list of products using the provided delegate and displays them in a formatted table.
        /// </summary>
        /// <param name="sortingDelegate">The delegate that defines the sorting order for the products.</param>
        /// <param name="products">The list of products to sort and display.</param>
        public void SortAndDisplay(SortDelegate sortingDelegate, List<Product> products)
        {
            products.Sort(new Comparison<Product>(sortingDelegate));
            ConsoleTable table = new ConsoleTable("Name", "Category", "Price");
            foreach (Product product in products)
            {
                table.AddRow(product.Name, product.Category, product.Price);
            }

            table.Write(Format.MarkDown);
        }

        /// <summary>
        /// Sorts the product by name.
        /// </summary>
        /// <param name="product1">The first <see cref="Product"/> to compare.</param>
        /// <param name="product2">The second <see cref="Product"/> to compare.</param>
        /// <returns>A signed integer which states the relative order of the products.</returns>
        private int SortByName(Product product1, Product product2)
        {
            return product1.Name.CompareTo(product2.Name);
        }

        /// <summary>
        /// Sorts the product by category.
        /// </summary>
        /// <param name="product1">The first <see cref="Product"/> to compare.</param>
        /// <param name="product2">The second <see cref="Product"/> to compare.</param>
        /// <returns>A signed integer which states the relative order of the products.</returns>
        private int SortByCategory(Product product1, Product product2)
        {
            return product1.Category.CompareTo(product2.Category);
        }

        /// <summary>
        /// Sorts the product by price.
        /// </summary>
        /// <param name="product1">The first <see cref="Product"/> to compare.</param>
        /// <param name="product2">The second <see cref="Product"/> to compare.</param>
        /// <returns>A signed integer which states the relative order of the products.</returns>
        private int SortByPrice(Product product1, Product product2)
        {
            return product1.Price.CompareTo(product2.Price);
        }

        /// <summary>
        /// Creates and returns a list of <see cref="Product"/> with predefined names, categories, and prices.
        /// </summary>
        /// <returns>A list of <see cref="Product"/> objects representing various items.</returns>
        private List<Product> MakeProductList()
        {
            List<Product> products = new List<Product>
            {
                new Product { Name = "AirPods Pro", Category = "Electronics", Price = 1500 },
                new Product { Name = "Laptop", Category = "Electronics", Price = 55000 },
                new Product { Name = "Mechanical Keyboard", Category = "Electronics", Price = 4500 },
                new Product { Name = "Wireless Mouse", Category = "Electronics", Price = 1200 },
                new Product { Name = "Smartphone", Category = "Electronics", Price = 30000 },
                new Product { Name = "Office Chair", Category = "Furniture", Price = 8000 },
                new Product { Name = "Study Table", Category = "Furniture", Price = 6500 },
                new Product { Name = "Bookshelf", Category = "Furniture", Price = 5000 },
                new Product { Name = "Water Bottle", Category = "Home", Price = 500 },
                new Product { Name = "Coffee Mug", Category = "Home", Price = 350 },
                new Product { Name = "Backpack", Category = "Accessories", Price = 1800 },
                new Product { Name = "Wallet", Category = "Accessories", Price = 900 },
                new Product { Name = "Running Shoes", Category = "Sports", Price = 4000 },
                new Product { Name = "Cricket Bat", Category = "Sports", Price = 2500 },
                new Product { Name = "Football", Category = "Sports", Price = 1200 },
                new Product { Name = "Notebook", Category = "Stationery", Price = 100 },
                new Product { Name = "Pen Set", Category = "Stationery", Price = 250 },
                new Product { Name = "Monitor", Category = "Electronics", Price = 15000 },
                new Product { Name = "External SSD", Category = "Electronics", Price = 7000 },
                new Product { Name = "Bluetooth Speaker", Category = "Electronics", Price = 3200 },
            };

            return products;
        }
    }
}
