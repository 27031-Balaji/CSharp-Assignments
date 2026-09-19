using ConsoleTables;

namespace CSharpAdvancedFeatures.Tasks
{
    /// <summary>
    /// Implements the task 6 of the advanced features in C#.
    /// </summary>
    internal class Task6
    {
        /// <summary>
        /// Runs the application containing the task.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Task 6 - Implementing and Manipulating Records in C# 9.0 and Above\n");

            List<Book> bookList = this.CreateBookRecords();

            Console.WriteLine("\nDisplaying the list of book records...\n");
            this.DisplayBookRecords(bookList);

            Console.WriteLine("\nChecking the value equality of records...\n");

            Book book1 = new Book("C# in Depth", "Jon Skeet", "978-1617294532");
            Book book2 = new Book("C# in Depth", "Jon Skeet", "978-1617294532");

            Console.WriteLine($"Book 1 == Book 2 : {book1 == book2}");

            Console.WriteLine("\nChecking the immutability of records...\n");

            Book newBook = new Book("Code Complete", "Steve McConnell", "978-0735619678");

            Console.WriteLine("Original Record:\n");
            this.DisplayBookRecords(new List<Book> { newBook });

            // Will not compile because records are immutable.
            // newBook.author = "Dineshbalaji";
            Console.WriteLine("Cannot modify the author directly because the record is immutable.");

            Console.WriteLine("\nCreating a new record using the with keyword...\n");
            Book modifiedBook = newBook with
            {
                author = "Dineshbalaji"
            };

            Console.WriteLine("Original Record:\n");
            this.DisplayBookRecords(new List<Book> { newBook });

            Console.WriteLine("\nModified Record:\n");
            this.DisplayBookRecords(new List<Book> { modifiedBook });

            Console.WriteLine("\nDisplaying a book using deconstruction...\n");

            this.DisplayBook(book1);
        }

        /// <summary>
        /// Displays a book using deconstruction.
        /// </summary>
        private void DisplayBook(Book book)
        {
            var (title, author, isbn) = book;

            Console.WriteLine($"Title  : {title}");
            Console.WriteLine($"Author : {author}");
            Console.WriteLine($"ISBN   : {isbn}");
        }

        /// <summary>
        /// Displays a list of books.
        /// </summary>
        private void DisplayBookRecords(List<Book> bookList)
        {
            ConsoleTable table = new ConsoleTable("Title", "Author", "ISBN");

            foreach (Book book in bookList)
            {
                table.AddRow(book.title, book.author, book.isbn);
            }

            table.Write(Format.MarkDown);
        }

        /// <summary>
        /// Creates a list of book records.
        /// </summary>
        private List<Book> CreateBookRecords()
        {
            return new List<Book>
            {
                new Book("The Pragmatic Programmer", "Andrew Hunt", "978-0201616224"),
                new Book("Clean Code", "Robert C. Martin", "978-0132350884"),
                new Book("Design Patterns", "Erich Gamma", "978-0201633610"),
                new Book("Refactoring", "Martin Fowler", "978-0134757599"),
                new Book("Head First Design Patterns", "Eric Freeman", "978-0596007126"),
            };
        }
    }

    /// <summary>
    /// Represents a book record.
    /// </summary>
    /// <param name="title">The title of the book.</param>
    /// <param name="author">The author of the book.</param>
    /// <param name="isbn">The ISBN of the book.</param>
    public record Book(string title, string author, string isbn);
}