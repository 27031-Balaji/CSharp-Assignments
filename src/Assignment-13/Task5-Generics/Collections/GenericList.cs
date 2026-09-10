namespace Generics.Collections
{
    /// <summary>
    /// Represents a generic list collection that stores elements of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    internal class GenericList<T>
    {
        private List<T> _list = new List<T>();

        /// <summary>
        /// Gets the total number of elements contained in the list.
        /// </summary>
        /// <value>The number of elements currently stored in the list.</value>
        public int Count => this._list.Count;

        /// <summary>
        /// Adds an item to the end of the list.
        /// </summary>
        /// <param name="item">The item to add to the list.</param>
        public void Add(T item)
        {
            this._list.Add(item);
        }

        /// <summary>
        /// Removes the first occurrence of the specified item from the list.
        /// </summary>
        /// <param name="item">The item to remove from the list.</param>
        /// <returns>True if the item was successfully removed, otherwise false.</returns>
        public bool Remove(T item)
        {
            return this._list.Remove(item);
        }

        /// <summary>
        /// Determines whether the list contains the specified item.
        /// </summary>
        /// <param name="item">The item to locate in the list.</param>
        /// <returns>True if the item exists in the list, otherwise false.</returns>
        public bool Contains(T item)
        {
            return this._list.Contains(item);
        }

        /// <summary>
        /// Displays all elements in the list along with their sequence number.
        /// </summary>
        public void Display()
        {
            int count = 1;

            foreach (T item in this._list)
            {
                Console.WriteLine($"No. {count} : {item}");
                count++;
            }
        }
    }
}