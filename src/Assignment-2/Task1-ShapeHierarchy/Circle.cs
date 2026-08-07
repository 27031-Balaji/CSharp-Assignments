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
        /// <param name="color">The color of the <see cref="Circle"/>.</param>
        /// <param name="radius">The radius of the <see cref="Circle"/>.</param>
        public Circle(string color, double radius)
            : base(color)
        {
            this.Radius = radius;
        }

        /// <summary>
        /// Gets the radius of the <see cref="Circle"/>.
        /// </summary>
        /// <value>The radius of the <see cref="Circle"/>.</value>
        public double Radius { get; }

        /// <summary>
        /// Calculates the area of the <see cref="Circle"/>.
        /// </summary>
        /// <returns>The calculated area of the <see cref="Circle"/>.</returns>
        public override double CalculateArea()
        {
            return Math.PI * this.Radius * this.Radius;
        }

        /// <summary>
        /// Returns the description of the <see cref="Circle"/>.
        /// </summary>
        /// <returns>A string containing the <see cref="Circle"/> details.</returns>
        public override string PrintDetails()
        {
            return $"Circle: Color = {this.Color}, Radius = {this.Radius} cm, Area = {this.CalculateArea():F2} cm^2";
        }
    }
}