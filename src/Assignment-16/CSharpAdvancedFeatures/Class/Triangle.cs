namespace CSharpAdvancedFeatures.Class
{
    /// <summary>
    /// Represents the triangle shape with base and height.
    /// </summary>
    internal class Triangle : Shape
    {
        /// <summary>
        /// Gets or sets the base of the triangle.
        /// </summary>
        public double Base { get; set; }

        /// <summary>
        /// Gets or sets the height of the triangle.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Calculates the area of the triangle.
        /// </summary>
        /// <returns>The area of the triangle.</returns>
        public override double CalculateArea()
        {
            return 0.5 * this.Base * this.Height;
        }
    }
}