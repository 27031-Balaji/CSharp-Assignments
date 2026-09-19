namespace ShapeHierarchy.Classes
{
    /// <summary>
    /// Represents a geometric shape with a color. Serves as the base class for specific shapes.
    /// </summary>
    internal abstract class Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class.
        /// </summary>
        /// <param name="color">The color of the <see cref="Shape"/>.</param>
        protected Shape(string color)
        {
            this.Color = color;
        }

        /// <summary>
        /// Gets the color of the <see cref="Shape"/>.
        /// </summary>
        /// <value>The color of the <see cref="Shape"/>.</value>
        public string Color { get; }

        /// <summary>
        /// Calculates the area of the <see cref="Shape"/>.
        /// </summary>
        /// <returns>The calculated area.</returns>
        public abstract double CalculateArea();

        /// <summary>
        /// Returns the description of the <see cref="Shape"/>.
        /// </summary>
        /// <returns>A string containing the <see cref="Shape"/> details.</returns>
        public abstract string PrintDetails();
    }
}