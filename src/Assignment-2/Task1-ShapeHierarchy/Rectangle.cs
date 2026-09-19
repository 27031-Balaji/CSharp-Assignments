namespace ShapeHierarchy.Classes
{
    /// <summary>
    /// Represents a rectangle shape with length, breadth and color.
    /// </summary>
    internal class Rectangle : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="color">The color of the <see cref="Rectangle"/>.</param>
        /// <param name="length">The length of the <see cref="Rectangle"/>.</param>
        /// <param name="breadth">The breadth of the <see cref="Rectangle"/>.</param>
        public Rectangle(string color, double length, double breadth)
            : base(color)
        {
            this.Length = length;
            this.Breadth = breadth;
        }

        /// <summary>
        /// Gets the length of the <see cref="Rectangle"/>.
        /// </summary>
        /// <value>The length of the <see cref="Rectangle"/>.</value>
        public double Length { get; }

        /// <summary>
        /// Gets the breadth of the <see cref="Rectangle"/>.
        /// </summary>
        /// <value>The breadth of the <see cref="Rectangle"/>.</value>
        public double Breadth { get; }

        /// <summary>
        /// Calculates the area of the <see cref="Rectangle"/>.
        /// </summary>
        /// <returns>The calculated area of the <see cref="Rectangle"/>.</returns>
        public override double CalculateArea()
        {
            return this.Length * this.Breadth;
        }

        /// <summary>
        /// Returns the description of the <see cref="Rectangle"/>.
        /// </summary>
        /// <returns>A string containing the <see cref="Rectangle"/> details.</returns>
        public override string PrintDetails()
        {
            return $"Rectangle: Color = {this.Color}, Length = {this.Length} cm, Breadth = {this.Breadth} cm, Area = {this.CalculateArea():F2} cm^2";
        }
    }
}