namespace StackAndHeap.Class
{
    /// <summary>
    /// Represents a college with the ID and the name of the college.
    /// </summary>
    public class College
    {
        /// <summary>
        /// Gets or sets the ID of the college.
        /// </summary>
        /// <value>The ID of the college.</value>
        public int CollegeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the college.
        /// </summary>
        /// <value>The name of the college.</value>
        public string CollegeName { get; set; } = string.Empty;
    }
}
