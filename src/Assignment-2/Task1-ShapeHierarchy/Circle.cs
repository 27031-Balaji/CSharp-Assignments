namespace ShapeHierarchy.Classes
{
    /// <summary>
    /// Represents a circle shape with a radius and color.
    /// </summary>
    internal class Circle : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="color">The color of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        public Circle(string color, double radius)
            : base(color)
        {
            this.Radius = radius;
        }

        /// <summary>
        /// Gets the radius of the circle.
        /// </summary>
        /// <value>The radius as a double.</value>
        public double Radius { get; }

        /// <summary>
        /// Calculates the area of the circle.
        /// </summary>
        /// <returns>The calculated area.</returns>
        public override double CalculateArea()
        {
            return Math.PI * this.Radius * this.Radius;
        }

        /// <summary>
        /// Returns the description of the circle.
        /// </summary>
        /// <returns>A string containing the circle details.</returns>
        public override string PrintDetails()
        {
            return $"Circle: Color = {this.Color}, Radius = {this.Radius}, Area = {this.CalculateArea():F2}";
        }
    }
}