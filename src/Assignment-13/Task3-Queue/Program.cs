namespace Queues
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
            // 3.1 - Creating a queue of strings, each representing a person's name
            Queue<string> personQueue = new Queue<string>();

            // 3.2 - Add five persons to the queue
            personQueue.Enqueue("Dineshbalaji");
            personQueue.Enqueue("Jay Kishore");
            personQueue.Enqueue("Sagarika");
            personQueue.Enqueue("Kevin");
            personQueue.Enqueue("Harini");

            // 3.3 - Remove a person from the queue
            try
            {
                string removedPerson = personQueue.Dequeue();
                Console.WriteLine($"Removed a person from the queue!{Environment.NewLine}The person is: {removedPerson}{Environment.NewLine}");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Cannot dequeue because the queue is empty.");
            }

            // 3.4 - Display all the persons in the queue
            Console.WriteLine("Persons in the queue: ");
            foreach (string person in personQueue)
            {
                Console.WriteLine(person);
            }

            Console.ReadKey();
        }
    }
}