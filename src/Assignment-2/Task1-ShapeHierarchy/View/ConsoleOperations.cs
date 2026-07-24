using Task1.Helpers;
using Task1.Models;
using Task1.Services;

namespace Task1.View
{
    /// <summary>
    /// Provides console-based operations to create shapes and display their details.
    /// </summary>
    internal class ConsoleOperations
    {
        /// <summary>
        /// The shape services used to create shapes.
        /// </summary>
        private readonly ShapeServices _shapeServices;

        /// <summary>
        /// Helper methods for validating shape input.
        /// </summary>
        private readonly ShapeHelpers _shapeHelpers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleOperations"/> class.
        /// </summary>
        /// <param name="shapeServices">Service used to create shape instances.</param>
        /// <param name="shapeHelpers">Helper used to validate shape input.</param>
        public ConsoleOperations(ShapeServices shapeServices, ShapeHelpers shapeHelpers)
        {
            this._shapeServices = shapeServices;
            this._shapeHelpers = shapeHelpers;
        }

        /// <summary>
        /// Runs the interactive console flow.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Welcome to Shape Calculator.");
            Shape shape = this.GetShape();
            this.ShowOperations(shape);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Gets the shape details from the user and creates the shape.
        /// </summary>
        /// <returns>The created shape.</returns>
        private Shape GetShape()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose a shape:");
                Console.WriteLine("[A] Rectangle");
                Console.WriteLine("[B] Circle");
                Console.Write("Enter your choice: ");
                char choice = char.ToUpper(Console.ReadKey().KeyChar);
                if (!this._shapeHelpers.IsValidChoice(choice))
                {
                    Console.WriteLine("\nEnter a valid choice (A or B). Please try again.");
                    continue;
                }

                Console.WriteLine();

                string color = this.GetColor();
                switch (choice)
                {
                    case 'A':
                        double length = this.GetPositiveNumber("Length");
                        double breadth = this.GetPositiveNumber("Breadth");
                        return this._shapeServices.CreateRectangle(color, length, breadth);

                    case 'B':
                        double radius = this.GetPositiveNumber("Radius");
                        return this._shapeServices.CreateCircle(color, radius);
                }
            }
        }

        /// <summary>
        /// Gets a valid color from the user.
        /// </summary>
        /// <returns>The valid color.</returns>
        private string GetColor()
        {
            while (true)
            {
                Console.Write("Enter Color: ");
                string color = Console.ReadLine() ?? string.Empty;
                if (this._shapeHelpers.IsValidColor(color))
                {
                    return color;
                }

                Console.WriteLine("Enter a valid color.");
            }
        }

        /// <summary>
        /// Gets a valid positive number from the user.
        /// </summary>
        /// <param name="dimension">The dimension that we are getting from the user.</param>
        /// <returns>The valid positive number.</returns>
        private double GetPositiveNumber(string dimension)
        {
            while (true)
            {
                Console.Write($"Enter {dimension}: ");
                string input = Console.ReadLine() ?? string.Empty;
                if (this._shapeHelpers.IsValidPositiveNumber(input, out double number))
                {
                    return number;
                }

                Console.WriteLine($"{dimension} must be a valid number greater than zero. Please try again.");
            }
        }

        /// <summary>
        /// Displays the available operations for the created shape.
        /// </summary>
        /// <param name="shape">The created shape.</param>
        private void ShowOperations(Shape shape)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("[A] Calculate Area");
                Console.WriteLine("[B] Print Details");
                Console.WriteLine("[C] Exit");
                Console.Write("Enter your choice: ");
                char choice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
                switch (choice)
                {
                    case 'A':
                        Console.WriteLine($"Area: {shape.CalculateArea():F2}");
                        break;

                    case 'B':
                        Console.WriteLine(shape.PrintDetails());
                        break;

                    case 'C':
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}