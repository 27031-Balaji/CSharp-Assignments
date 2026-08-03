using ShapeHierarchy.Classes;
using static System.Drawing.Color;

namespace ShapeHierarchy
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// It handles user interaction for selecting shapes and performing operations.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Starts the Shape Calculator application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Shape Calculator.");
            Shape shape = GetShape();
            ShowOperations(shape);
        }

        /// <summary>
        /// Prompts the user to select a shape and creates the corresponding object.
        /// </summary>
        /// <returns>A Rectangle or Circle object according to the user's choice.</returns>
        private static Shape GetShape()
        {
            bool isShapeSelected = false;
            Shape shape = null!;
            while (!isShapeSelected)
            {
                Console.WriteLine();
                Console.WriteLine("Choose a Shape:");
                Console.WriteLine("[A] Rectangle");
                Console.WriteLine("[B] Circle");
                Console.Write("Enter your choice: ");
                char shapeChoice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                switch (shapeChoice)
                {
                    case 'A':
                        shape = CreateRectangle();
                        isShapeSelected = true;
                        break;

                    case 'B':
                        shape = CreateCircle();
                        isShapeSelected = true;
                        break;

                    default:
                        Console.WriteLine("Enter a valid choice.");
                        break;
                }
            }

            return shape;
        }

        /// <summary>
        /// Creates a Rectangle object after collecting validated input.
        /// </summary>
        /// <returns>A Rectangle object.</returns>
        private static Rectangle CreateRectangle()
        {
            string color = GetColor();
            double length = GetPositiveNumber("Length");
            double breadth = GetPositiveNumber("Breadth");

            return new Rectangle(color, length, breadth);
        }

        /// <summary>
        /// Creates a Circle object after collecting validated input.
        /// </summary>
        /// <returns>A Circle object.</returns>
        private static Circle CreateCircle()
        {
            string color = GetColor();
            double radius = GetPositiveNumber("Radius");

            return new Circle(color, radius);
        }

        /// <summary>
        /// Prompts the user until a valid known color is entered.
        /// </summary>
        /// <returns>The validated color name.</returns>
        private static string GetColor()
        {
            bool isValidColor = false;
            string color = string.Empty;

            while (!isValidColor)
            {
                Console.Write("Enter Color: ");
                color = Console.ReadLine() ?? string.Empty;
                isValidColor = !string.IsNullOrWhiteSpace(color) && FromName(color).IsKnownColor;
                if (!isValidColor)
                {
                    Console.WriteLine("Invalid color. Please enter the right color.");
                }
            }

            return color;
        }

        /// <summary>
        /// Prompts the user until a valid positive number is entered.
        /// </summary>
        /// <param name="fieldName">The field to be entered.</param>
        /// <returns>A validated positive number.</returns>
        private static double GetPositiveNumber(string fieldName)
        {
            bool isValidNumber = false;
            double value = 0;

            while (!isValidNumber)
            {
                Console.Write($"Enter {fieldName}: ");
                isValidNumber = double.TryParse(Console.ReadLine(), out value) && value > 0;
                if (!isValidNumber)
                {
                    Console.WriteLine($"Invalid {fieldName}. Please enter a positive number.");
                }
            }

            return value;
        }

        /// <summary>
        /// Displays the operations menu until the user chooses to exit.
        /// </summary>
        /// <param name="shape">The selected shape.</param>
        private static void ShowOperations(Shape shape)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("Choose an Operation:");
                Console.WriteLine("[A] Calculate Area");
                Console.WriteLine("[B] Print Details");
                Console.WriteLine("[C] Exit");
                Console.Write("Enter your choice: ");
                char operationChoice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                switch (operationChoice)
                {
                    case 'A':
                        Console.WriteLine($"Area: {shape.CalculateArea():F2}");
                        break;

                    case 'B':
                        Console.WriteLine(shape.PrintDetails());
                        break;

                    case 'C':
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Enter a valid choice.");
                        break;
                }
            }
        }
    }
}