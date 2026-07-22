using Task1.Models;

namespace Task1.Services
{
    /// <summary>
    /// Provides services and utility operations for shapes.
    /// </summary>
    internal class ShapeServices
    {
        /// <summary>
        /// Creates a new rectangle object for area calculation and printing description.
        /// </summary>
        /// <param name="color">The color of the shape.</param>
        /// <param name="length">The length of the rectangle.</param>
        /// <param name="breadth">The breadth of the rectangle.</param>
        /// <returns>The rectangle object.</returns>
        public Shape CreateRectangle(string color, double length, double breadth)
        {
            return new Rectangle(color, length, breadth);
        }

        /// <summary>
        /// Creates a new circle object for area calculation and printing description.
        /// </summary>
        /// <param name="color">The color of the shape.</param>
        /// <param name="radius">The length of the rectangle.</param>
        /// <returns>The circle object.</returns>
        public Shape CreateCircle(string color, double radius)
        {
            return new Circle(color, radius);
        }
    }
}