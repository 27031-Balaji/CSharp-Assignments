namespace WebsiteContentExtraction
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// When the application is run, the main method is executed.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        /// <returns>The task doing the operation.</returns>
        public static async Task Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Write("Enter the ID of the user (1-100): ");
                string input = (Console.ReadLine() ?? string.Empty).Trim();
                if (!int.TryParse(input, out int id) || id < 1 || id > 100)
                {
                    Console.WriteLine("Wrong input. Please try again.");
                    continue;
                }

                try
                {
                    Console.WriteLine("The blog post for the ID is: ");
                    string contentOfTheUser = await GetUserBlogPostAsync(id);
                    Console.WriteLine(contentOfTheUser);
                    Console.WriteLine();

                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    isRunning = false;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Gets the blog post of the user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The JSON content of the blog post posted by the user.</returns>
        public static async Task<string> GetUserBlogPostAsync(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string endPoint = $"https://jsonplaceholder.typicode.com/posts/{id}";
                HttpResponseMessage response = await httpClient.GetAsync(endPoint);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }
    }
}