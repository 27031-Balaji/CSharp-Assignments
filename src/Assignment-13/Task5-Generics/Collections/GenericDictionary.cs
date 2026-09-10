namespace Generics.Collections
{
    /// <summary>
    /// Represents a generic dictionary collection that stores key/value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary. Keys must not be null.</typeparam>
    /// <typeparam name="TValue">The type of values stored in the dictionary.</typeparam>
    internal class GenericDictionary<TKey, TValue>
        where TKey : notnull
    {
        private Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

        /// <summary>
        /// Gets the total number of key/value pairs contained in the dictionary.
        /// </summary>
        /// <value>The total number of key/value pairs contained in the dictionary.</value>
        public int Count { get => this._dictionary.Count; }

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <exception cref="ArgumentException">Thrown when an element with the same key already exists.</exception>
        public void Add(TKey key, TValue value)
        {
            this._dictionary.Add(key, value);
        }

        /// <summary>
        /// Removes the element with the specified key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        public void Remove(TKey key)
        {
            this._dictionary.Remove(key);
        }

        /// <summary>
        /// Determines whether the dictionary contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the dictionary.</param>
        /// <returns>True if the dictionary contains the specified key, otherwise false.</returns>
        public bool Contains(TKey key)
        {
            return this._dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Displays all key/value pairs in the dictionary to the console.
        /// </summary>
        public void Display()
        {
            foreach (KeyValuePair<TKey, TValue> pair in this._dictionary)
            {
                Console.WriteLine($"{pair.Key} : {pair.Value}");
            }
        }
    }
}