namespace Task1
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            // 1.1 - Creating a list of strings, each representing a book title.
            List<string> books = new List<string>();

            // 1.2 - Adding five books using Add()
            books.Add("To Kill A Mockingbird");
            books.Add("Jane Eyre");
            books.Add("Anna Karenina");
            books.Add("Don Quixote");
            books.Add("The Count Of Monte Cristo");

            // 1.3 - Removing a book using Remove() and checking whether it is removed or not.
            if (!books.Remove("The Bible"))
            {
                Console.WriteLine($"'The Bible' doesn't exist in the book list.{Environment.NewLine}");
            }

            // Can use normal Remove() to remove the element.
            books.Remove("Jane Eyre");

            // 1.4 - Check a particular book exists in the list using Contains()
            bool doesBookExist = books.Contains("Don Quixote");
            Console.WriteLine($"Does 'Don Quixote' exist in the collection? {(doesBookExist ? "Yes" : "No")}{Environment.NewLine}");

            // 1.5 - Display all the books in the list
            Console.WriteLine($"The Book List{Environment.NewLine}");
            int count = 1;
            foreach (string book in books)
            {
                Console.WriteLine($"No. {count} : {book}");
                count++;
            }

            Console.ReadKey();
        }
    }
}