namespace Generics.Collections
{
    /// <summary>
    /// Represents a generic stack collection that stores elements in a last-in, first-out (LIFO) order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the stack.</typeparam>
    internal class GenericStack<T>
    {
        private Stack<T> _stack = new Stack<T>();

        /// <summary>
        /// Gets the total number of elements contained in the stack.
        /// </summary>
        /// <value>The number of elements currently stored in the stack.</value>
        public int Count => this._stack.Count;

        /// <summary>
        /// Inserts an item at the top of the stack.
        /// </summary>
        /// <param name="item">The item to push onto the stack.</param>
        public void Push(T item)
        {
            this._stack.Push(item);
        }

        /// <summary>
        /// Removes and returns the item at the top of the stack.
        /// </summary>
        /// <returns>The item removed from the top of the stack.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the stack is empty.exception>
        public T Pop()
        {
            return this._stack.Pop();
        }

        /// <summary>
        /// Determines whether the stack contains the specified item.
        /// </summary>
        /// <param name="item">The item to locate in the stack.</param>
        /// <returns>True if the item exists in the stack, otherwise false.</returns>
        public bool Contains(T item)
        {
            return this._stack.Contains(item);
        }

        /// <summary>
        /// Displays all elements in the stack along with their sequence number.
        /// </summary>
        public void Display()
        {
            int count = 1;

            foreach (T item in this._stack)
            {
                Console.WriteLine($"No. {count} : {item}");
                count++;
            }
        }
    }
}