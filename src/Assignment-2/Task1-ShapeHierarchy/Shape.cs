namespace Task1.Classes
{
    /// <summary>
    /// Represents a geometric shape with a color. Serves as the base class for specific shapes.
    /// </summary>
    internal abstract class Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class.
        /// </summary>
        /// <param name="color">The color of the shape.</param>
        protected Shape(string color)
        {
            this.Color = color;
        }

        /// <summary>
        /// Gets the color of the shape.
        /// </summary>
        /// <value>The color as a string.</value>
        public string Color { get; }

        /// <summary>
        /// Calculates the area of the shape.
        /// </summary>
        /// <returns>The calculated area.</returns>
        public abstract double CalculateArea();

        /// <summary>
        /// Returns the description of the shape.
        /// </summary>
        /// <returns>A string containing the shape details.</returns>
        public abstract string PrintDetails();
    }
}