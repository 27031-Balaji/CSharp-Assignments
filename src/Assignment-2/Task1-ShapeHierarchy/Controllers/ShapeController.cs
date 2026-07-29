using Task1.Helpers;
using Task1.Models;
using Task1.Services;
using Task1.View;

namespace Task1.Controllers
{
    /// <summary>
    /// Controls the application flow for shape operations.
    /// </summary>
    internal class ShapeController
    {
        // Invalid Error Messages
        private const string InvalidShapeChoiceMessage = "Enter a valid choice (A or B). Please try again.";
        private const string InvalidColorMessage = "Enter a valid color.";
        private const string InvalidOperationMessage = "Invalid choice. Please try again.";

        private readonly ShapeServices _shapeServices;
        private readonly ShapeHelpers _shapeHelpers;
        private readonly ConsoleOperations _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeController"/> class.
        /// </summary>
        /// <param name="shapeServices">Provides shape creation services.</param>
        /// <param name="shapeHelpers">Provides helper methods for validation.</param>
        /// <param name="view">Provides console input and output operations.</param>
        public ShapeController(ShapeServices shapeServices, ShapeHelpers shapeHelpers, ConsoleOperations view)
        {
            this._shapeServices = shapeServices;
            this._shapeHelpers = shapeHelpers;
            this._view = view;
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        public void Run()
        {
            this._view.ShowWelcomeMessage();
            Shape shape = this.GetShape();
            this.ShowOperations(shape);
            this._view.FlushScreenWithKey();
        }

        /// <summary>
        /// Gets the shape details from the user.
        /// </summary>
        /// <returns>The created shape.</returns>
        private Shape GetShape()
        {
            do
            {
                char choice = this._view.ShowShapeMenu();
                if (!this._shapeHelpers.IsValidChoice(choice))
                {
                    this._view.ShowMessage(InvalidShapeChoiceMessage);
                    continue;
                }

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
            while (true);
        }

        /// <summary>
        /// Gets a valid color from the user.
        /// </summary>
        /// <returns>The validated color.</returns>
        private string GetColor()
        {
            do
            {
                string color = this._view.ReadColor();
                if (this._shapeHelpers.IsValidColor(color))
                {
                    return color;
                }

                this._view.ShowMessage(InvalidColorMessage);
            }
            while (true);
        }

        /// <summary>
        /// Gets a valid positive number from the user.
        /// </summary>
        /// <param name="dimension">The dimension name.</param>
        /// <returns>The validated positive number.</returns>
        private double GetPositiveNumber(string dimension)
        {
            do
            {
                string input = this._view.ReadPositiveNumber(dimension);
                if (this._shapeHelpers.IsValidPositiveNumber(input, out double number))
                {
                    return number;
                }

                this._view.ShowMessage($"{dimension} must be a valid number greater than zero. Please try again.");
            }
            while (true);
        }

        /// <summary>
        /// Displays the operation menu for the selected shape.
        /// </summary>
        /// <param name="shape">The selected shape.</param>
        private void ShowOperations(Shape shape)
        {
            char choice;
            do
            {
                choice = this._view.ShowOperationMenu();
                switch (choice)
                {
                    case 'A':
                        this._view.ShowArea(shape.CalculateArea());
                        break;

                    case 'B':
                        this._view.ShowDetails(shape.PrintDetails());
                        break;

                    case 'C':
                        return;

                    default:
                        this._view.ShowMessage(InvalidOperationMessage);
                        break;
                }
            }
            while (choice != 'C');
        }
    }
}