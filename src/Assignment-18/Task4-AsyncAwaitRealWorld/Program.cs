using System.Text.Json;

namespace AsyncAwaitRealWorld
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main method that runs when the application is run.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>The main task that runs the methods.</returns>
        public static async Task Main(string[] args)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    Console.WriteLine("Starting blog post analysis...");
                    Console.WriteLine();

                    int propertyCount = await MethodC(httpClient);
                    Console.WriteLine($"Total key-value pairs: {propertyCount}");
                    Console.WriteLine("Process completed successfully.");
                    Console.WriteLine();

                    Console.ReadKey();
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP error: {ex.Message}");
                    Console.ReadKey();
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON parsing error: {ex.Message}");
                    Console.ReadKey();
                }
                catch (TaskCanceledException ex)
                {
                    Console.WriteLine($"Request timed out or was cancelled: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Implements the CPU-bound operation to find the ID of the blog post.
        /// </summary>
        /// <returns>The ID of the post.</returns>
        public static int MethodA()
        {
            // Simulate the database operation to find the ID of the blog post.
            long totalSales = 0;
            for (int i = 1; i <= 234891728; i++)
            {
                totalSales += i;
            }

            int postId = (int)(totalSales % 100) + 1;

            Console.WriteLine("Blog post analysis completed.");
            Console.WriteLine($"Selected record ID: {postId}");
            Console.WriteLine();

            return postId;
        }

        /// <summary>
        /// Awaits the CPU-bound MethodA() and calls the API for the JSON string.
        /// </summary>
        /// <param name="httpClient">The HTTP Client used to get the data.</param>
        /// <returns>The JSON data as a string.</returns>
        public static async Task<string> MethodB(HttpClient httpClient)
        {
            int postId = await Task.Run(() => MethodA());
            string endpoint = $"https://jsonplaceholder.typicode.com/posts/{postId}";

            Console.WriteLine("Fetching blog post from Web API...");
            Console.WriteLine();
            using HttpResponseMessage response = await httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            return json;
        }

        /// <summary>
        /// Awaits the API operation MethodB() and finds the number of properties in the JSON data and prints it.
        /// </summary>
        /// <param name="httpClient">The HTTP client used to retrieve the JSON data.</param>
        /// <returns>The count of properties in the JSON data.</returns>
        public static async Task<int> MethodC(HttpClient httpClient)
        {
            string json = await MethodB(httpClient);
            int count = 0;

            using (JsonDocument document = JsonDocument.Parse(json))
            {
                foreach (JsonProperty property in document.RootElement.EnumerateObject())
                {
                    Console.WriteLine($"{property.Name}: {property.Value}");
                    count++;
                }

                Console.WriteLine();
            }

            return count;
        }
    }
}