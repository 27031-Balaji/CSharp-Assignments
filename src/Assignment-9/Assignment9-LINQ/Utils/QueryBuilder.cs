namespace Assignment9.Utils
{
    /// <summary>
    /// Provides a fluent interface for filtering, sorting, joining, and executing queries on a collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the data collection.</typeparam>
    public class QueryBuilder<T>
    {
        private IEnumerable<T> _query;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder{T}"/> class.
        /// </summary>
        /// <param name="data">The collection of elements to be used for query construction.</param>
        public QueryBuilder(IEnumerable<T> data)
        {
            this._query = data;
        }

        /// <summary>
        /// Filters the elements of the query based on a specified predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The current QueryBuilder instance with the applied filter.</returns>
        public QueryBuilder<T> Filter(Func<T, bool> predicate)
        {
            this._query = this._query.Where(predicate);
            return this;
        }

        /// <summary>
        /// Sorts the query results in ascending order according to a specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to sort by.</typeparam>
        /// <param name="keySelector">A function to extract the key for sorting from each element.</param>
        /// <returns>The current QueryBuilder instance with the applied sorting.</returns>
        public QueryBuilder<T> SortBy<TKey>(Func<T, TKey> keySelector)
        {
            this._query = this._query.OrderBy(keySelector);
            return this;
        }

        /// <summary>
        /// Correlates elements from two sequences based on matching keys and projects the results.
        /// </summary>
        /// <typeparam name="TInner">The type of the elements in the inner sequence.</typeparam>
        /// <typeparam name="TKey">The type of the key used for matching elements.</typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <param name="inner">The sequence to join to the outer sequence.</param>
        /// <param name="outerKey">A function to extract the join key from each element of the outer sequence.</param>
        /// <param name="innerKey">A function to extract the join key from each element of the inner sequence.</param>
        /// <param name="resultSelector">A function to create a result element from two matching elements.</param>
        /// <returns>A sequence of result elements obtained by joining the outer and inner sequences.</returns>
        public QueryBuilder<TResult> Join<TInner, TKey, TResult>(
            IEnumerable<TInner> inner,
            Func<T, TKey> outerKey,
            Func<TInner, TKey> innerKey,
            Func<T, TInner, TResult> resultSelector)
        {
            var result = this._query.Join(inner, outerKey, innerKey, resultSelector);
            return new QueryBuilder<TResult>(result);
        }

        /// <summary>
        /// Executes the query by materializing it to a list.
        /// </summary>
        /// <returns>The list after executing all the queries.</returns>
        public List<T> Execute()
        {
            return this._query.ToList();
        }
    }
}