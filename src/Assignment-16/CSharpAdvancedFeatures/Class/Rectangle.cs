namespace CSharpAdvancedFeatures.Class
{
    /// <summary>
    /// Represents the rectangle shape with length and width.
    /// </summary>
    internal class Rectangle : Shape
    {
        /// <summary>
        /// Gets or sets the length of the rectangle.
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the width of the rectangle.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Calculates the area of the rectangle.
        /// </summary>
        /// <returns>The area of the rectangle.</returns>
        public override double CalculateArea()
        {
            return this.Length * this.Width;
        }
    }
}