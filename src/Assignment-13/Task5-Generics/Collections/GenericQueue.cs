namespace Generics.Collections
{
    /// <summary>
    /// Represents a generic queue collection that stores elements in a first-in, first-out (FIFO) order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the queue.</typeparam>
    internal class GenericQueue<T>
    {
        private Queue<T> _queue = new Queue<T>();

        /// <summary>
        /// Gets the total number of elements contained in the queue.
        /// </summary>
        /// <value>The number of elements currently stored in the queue.</value>
        public int Count => this._queue.Count;

        /// <summary>
        /// Adds an item to the end of the queue.
        /// </summary>
        /// <param name="item">The item to add to the queue.</param>
        public void Enqueue(T item)
        {
            this._queue.Enqueue(item);
        }

        /// <summary>
        /// Removes and returns the item at the beginning of the queue.
        /// </summary>
        /// <returns>The item that was removed from the beginning of the queue.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
        public T Dequeue()
        {
            return this._queue.Dequeue();
        }

        /// <summary>
        /// Determines whether the queue contains the specified item.
        /// </summary>
        /// <param name="item">The item to locate in the queue.</param>
        /// <returns>True if the item exists in the queue, otherwise false.</returns>
        public bool Contains(T item)
        {
            return this._queue.Contains(item);
        }

        /// <summary>
        /// Displays all elements in the queue along with their sequence number.
        /// </summary>
        public void Display()
        {
            int count = 1;

            foreach (T item in this._queue)
            {
                Console.WriteLine($"No. {count} : {item}");
                count++;
            }
        }
    }
}