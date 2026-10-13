using MathApp;
using UtilityApp;

namespace DisplayApp
{
    /// <summary>
    /// Starts the UI operations for the application.
    /// </summary>
    public class ConsoleUI
    {
        private readonly Validator _validator = new Validator();
        private readonly MathUtils _mathUtils = new MathUtils();

        /// <summary>
        /// Starts the UI operations for the calculator.
        /// </summary>
        public void Start()
        {
            this.DisplayMessage("Calculator Application");

            while (true)
            {
                int firstNumber = this.GetValidNumberInput("first");
                string operatorToBeUsed = this.GetValidOperatorInput();
                int secondNumber = this.GetValidNumberInput("second");

                try
                {
                    this.DisplayResult(firstNumber, secondNumber, operatorToBeUsed);
                }
                catch (DivideByZeroException ex)
                {
                    this.DisplayMessage(ex.Message);
                }

                if (!this.GetValidContinueOperatorInput())
                {
                    break;
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Displays a message on the console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Calculates and displays the result.
        /// </summary>
        /// <param name="firstNumber">The first number.</param>
        /// <param name="secondNumber">The second number.</param>
        /// <param name="operatorToBeUsed">The operator to use.</param>
        private void DisplayResult(
            int firstNumber,
            int secondNumber,
            string operatorToBeUsed)
        {
            double result;

            switch (operatorToBeUsed)
            {
                case "+":
                    result = this._mathUtils.Add(firstNumber, secondNumber);
                    break;

                case "-":
                    result = this._mathUtils.Subtract(firstNumber, secondNumber);
                    break;

                case "*":
                    result = this._mathUtils.Multiply(firstNumber, secondNumber);
                    break;

                case "/":
                    result = this._mathUtils.Divide(firstNumber, secondNumber);
                    break;

                default:
                    this.DisplayMessage("Unsupported operator.");
                    return;
            }

            this.DisplayMessage($"The result is: {result:F2}");
        }

        /// <summary>
        /// Gets a valid number from the user.
        /// </summary>
        /// <param name="order">The order of the number.</param>
        /// <returns>The validated number.</returns>
        private int GetValidNumberInput(string order)
        {
            while (true)
            {
                Console.Write($"Enter the {order} number: ");
                string input = (Console.ReadLine() ?? string.Empty).Trim();
                if (this._validator.IsValidNumber(input, out int number))
                {
                    return number;
                }

                this.DisplayMessage("Enter a valid integer value.");
            }
        }

        /// <summary>
        /// Gets a valid operator from the user.
        /// </summary>
        /// <returns>The validated operator.</returns>
        private string GetValidOperatorInput()
        {
            while (true)
            {
                Console.Write("Enter the operator (+, -, *, /): ");
                string operatorInput = (Console.ReadLine() ?? string.Empty).Trim();
                if (this._validator.IsValidOperator(operatorInput))
                {
                    return operatorInput;
                }

                this.DisplayMessage("Enter a valid operator (+, -, *, /).");
            }
        }

        /// <summary>
        /// Gets a valid continue choice from the user.
        /// </summary>
        /// <returns>
        /// True if the user chooses to continue; otherwise, false.
        /// </returns>
        private bool GetValidContinueOperatorInput()
        {
            while (true)
            {
                Console.Write("\nDo you want to continue (Y/N)? ");
                string choice = (Console.ReadLine() ?? string.Empty).Trim();
                if (this._validator.IsValidContinueChoice(choice))
                {
                    return choice.Equals("Y", StringComparison.OrdinalIgnoreCase);
                }

                this.DisplayMessage("Enter either Y or N.");
            }
        }
    }
}