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
            Console.WriteLine("Calculator Application");
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
                    Console.WriteLine(ex.Message);
                }

                bool shouldContinue = this.GetValidContinueOperatorInput();
                if (!shouldContinue)
                {
                    break;
                }

                Console.WriteLine();
            }
        }

        private void DisplayResult(int firstNumber, int secondNumber, string operatorToBeUsed)
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
                    Console.WriteLine("Unsupported operator.");
                    return;
            }

            Console.WriteLine($"The result is: {result:F2}");
        }

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

                Console.WriteLine("Enter a valid integer value.");
            }
        }

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

                Console.WriteLine("Enter a valid operator (+, -, *, /).");
            }
        }

        private bool GetValidContinueOperatorInput()
        {
            string choice;
            while (true)
            {
                Console.Write("\nDo you want to continue (Y/N)? ");
                choice = (Console.ReadLine() ?? string.Empty).Trim();
                if (this._validator.IsValidContinueChoice(choice))
                {
                    if (choice.Equals("N", StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }

                    return true;
                }

                Console.WriteLine("Enter either Y or N.");
            }
        }
    }
}