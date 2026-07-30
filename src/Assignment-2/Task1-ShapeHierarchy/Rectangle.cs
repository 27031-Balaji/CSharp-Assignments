namespace Task1.Classes
{
    /// <summary>
    /// Represents a rectangle shape with length, breadth and color.
    /// </summary>
    internal class Rectangle : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="color">The color of the rectangle.</param>
        /// <param name="length">The length of the rectangle.</param>
        /// <param name="breadth">The breadth of the rectangle.</param>
        public Rectangle(string color, double length, double breadth)
            : base(color)
        {
            this.Length = length;
            this.Breadth = breadth;
        }

        /// <summary>
        /// Gets the length of the rectangle.
        /// </summary>
        /// <value>The length as a double.</value>
        public double Length { get; }

        /// <summary>
        /// Gets the breadth of the rectangle.
        /// </summary>
        /// <value>The breadth as a double.</value>
        public double Breadth { get; }

        /// <summary>
        /// Calculates the area of the rectangle.
        /// </summary>
        /// <returns>The calculated area.</returns>
        public override double CalculateArea()
        {
            return this.Length * this.Breadth;
        }

        /// <summary>
        /// Returns the description of the rectangle.
        /// </summary>
        /// <returns>A string containing the rectangle details.</returns>
        public override string PrintDetails()
        {
            return $"Rectangle: Color = {this.Color}, Length = {this.Length}, Breadth = {this.Breadth}, Area = {this.CalculateArea():F2}";
        }
    }
}