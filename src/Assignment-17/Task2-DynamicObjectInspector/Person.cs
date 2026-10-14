namespace DynamicObjectInspector.Class
{
    /// <summary>
    /// Person class used to inspect the object.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the name of the person (Defaulting to Dinesh).
        /// </summary>
        /// <value>The name of the person.</value>
        public string Name { get; set; } = "Dinesh";

        /// <summary>
        /// Gets or sets the age of the person (Defaulting to 25).
        /// </summary>
        /// <value>The age of the person.</value>
        public int Age { get; set; } = 20;

        /// <summary>
        /// Gets or sets the city of the person (Defaulting to Coimbatore).
        /// </summary>
        /// <value>The city of the person.</value>
        public string City { get; set; } = "Coimbatore";
    }
}