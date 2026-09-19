namespace CSharpAdvancedFeatures.Class
{
    /// <summary>
    /// Represents a basic shape with name.
    /// </summary>
    internal abstract class Shape
    {
        /// <summary>
        /// Gets or sets the name of the shape.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Calculates the area of the shape.
        /// </summary>
        /// <returns>The area of the shape.</returns>
        public abstract double CalculateArea();
    }
}