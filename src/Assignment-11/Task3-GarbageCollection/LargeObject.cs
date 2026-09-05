namespace GarbageCollection.Class
{
    /// <summary>
    /// The class used to make a large object.
    /// </summary>
    public class LargeObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LargeObject"/> class.
        /// </summary>
        public LargeObject()
        {
            this.Data = new int[100000];
        }

        /// <summary>
        /// Gets or sets an array of integers.
        /// </summary>
        /// <value>The array of integers.</value>
        public int[] Data { get; set; }
    }
}