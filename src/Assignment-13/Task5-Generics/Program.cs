using Generics.Collections;

namespace Generics
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
            Console.WriteLine("Generic Implementation of Collections");
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("1. List Demo");
                Console.WriteLine("2. Stack Demo");
                Console.WriteLine("3. Queue Demo");
                Console.WriteLine("4. Dictionary Demo");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                string choice = (Console.ReadLine() ?? string.Empty).Trim();

                if (!int.TryParse(choice, out int menuChoice))
                {
                    Console.WriteLine("Enter a valid integer for choice.");
                    ClearScreenWithKey();
                    continue;
                }

                switch (menuChoice)
                {
                    case 1:
                        ListDemo();
                        ClearScreenWithKey();
                        break;

                    case 2:
                        StackDemo();
                        ClearScreenWithKey();
                        break;

                    case 3:
                        QueueDemo();
                        ClearScreenWithKey();
                        break;

                    case 4:
                        DictionaryDemo();
                        ClearScreenWithKey();
                        break;

                    case 5:
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Please enter a valid choice.");
                        break;
                }
            }
        }

        /// <summary>
        /// Method used to demonstrate the list operations.
        /// </summary>
        private static void ListDemo()
        {
            // 1.1 - Creating a list of strings, each representing a book title.
            GenericList<string> genericBookList = new GenericList<string>();

            // 1.2 - Adding five books using Add()
            genericBookList.Add("To Kill A Mockingbird");
            genericBookList.Add("Jane Eyre");
            genericBookList.Add("Anna Karenina");
            genericBookList.Add("Don Quixote");
            genericBookList.Add("The Count Of Monte Cristo");

            // 1.3 - Removing a book using Remove() and checking whether it is removed or not.
            if (!genericBookList.Remove("The Bible"))
            {
                Console.WriteLine($"{Environment.NewLine}'The Bible' doesn't exist in the book list.{Environment.NewLine}");
            }

            // Can use normal Remove() to remove the element.
            genericBookList.Remove("Jane Eyre");

            // 1.4 - Check a particular book exists in the list using Contains()
            bool doesBookExist = genericBookList.Contains("Don Quixote");
            Console.WriteLine($"Does 'Don Quixote' exist in the collection? {(doesBookExist ? "Yes" : "No")}{Environment.NewLine}");

            // 1.5 - Display all the books in the list
            Console.WriteLine($"The Book List{Environment.NewLine}");
            genericBookList.Display();
        }

        /// <summary>
        /// Method used to demonstrate the stack operations.
        /// </summary>
        private static void StackDemo()
        {
            // 2.1 - Create a stack of characters
            GenericStack<char> genericStack = new GenericStack<char>();

            // 2.2 - Push each character of a given string onto the stack
            Console.Write("Enter a string to be reversed: ");
            string input = (Console.ReadLine() ?? string.Empty).Trim();

            foreach (char ch in input)
            {
                genericStack.Push(ch);
            }

            // 2.3 - Pop each character and append it to a new string
            char[] reversedInput = new char[input.Length];
            int count = 0;
            while (genericStack.Count > 0)
            {
                reversedInput[count] = genericStack.Pop();
                count++;
            }

            // 2.4 - Display the original and the reversed string
            Console.WriteLine($"The original string: {input}");
            Console.WriteLine($"The reversed string using the stack: {new string(reversedInput)}");
        }

        /// <summary>
        /// Method used to demonstrate the queue operations.
        /// </summary>
        private static void QueueDemo()
        {
            // 3.1 - Creating a queue of strings, each representing a person's name
            GenericQueue<string> genericPersonQueue = new GenericQueue<string>();

            // 3.2 - Add five persons to the queue
            genericPersonQueue.Enqueue("Dineshbalaji");
            genericPersonQueue.Enqueue("Jay Kishore");
            genericPersonQueue.Enqueue("Sagarika");
            genericPersonQueue.Enqueue("Kevin");
            genericPersonQueue.Enqueue("Harini");

            // 3.3 - Remove a person from the queue
            try
            {
                string removedPerson = genericPersonQueue.Dequeue();
                Console.WriteLine($"{Environment.NewLine}Removed a person from the queue!{Environment.NewLine}The person is: {removedPerson}{Environment.NewLine}");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine($"{Environment.NewLine}Cannot dequeue because the queue is empty.");
            }

            // 3.4 - Display all the persons in the queue
            genericPersonQueue.Display();
        }

        /// <summary>
        /// Method used to demonstrate the dictionary operations.
        /// </summary>
        private static void DictionaryDemo()
        {
            // 4.1 - Create a new dictionary with student name and grades
            GenericDictionary<string, int> studentGrades = new GenericDictionary<string, int>();

            // 4.2 - Add five students and their grades
            try
            {
                studentGrades.Add("Lakshmi", 99);
                studentGrades.Add("Vishnu", 89);
                studentGrades.Add("Tarrun", 69);
                studentGrades.Add("Sowndhar", 80);
                studentGrades.Add("Dineshbalaji", 50);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Cannot add the key-value pair. Key already exists.");
            }

            // 4.3 - Remove a student from the dictionary
            studentGrades.Remove("Dineshbalaji");
            Console.WriteLine($"{Environment.NewLine}The student 'Dineshbalaji' has been removed.{Environment.NewLine}");

            // 4.4 - Display all the students and their grades
            studentGrades.Display();
        }

        private static void ClearScreenWithKey()
        {
            Console.WriteLine($"{Environment.NewLine}Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}