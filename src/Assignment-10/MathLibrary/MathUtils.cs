namespace MathLibrary
{
    /// <summary>
    /// Class library used to handle the calculation operations.
    /// </summary>
    public class MathUtils
    {
        /// <summary>
        /// Used to add two numbers.
        /// </summary>
        /// <param name="firstNumber">The first number.</param>
        /// <param name="secondNumber">The second number.</param>
        /// <returns>The addition result of the two numbers.</returns>
        public int Add(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }

        /// <summary>
        /// Used to subtract two numbers.
        /// </summary>
        /// <param name="firstNumber">The first number.</param>
        /// <param name="secondNumber">The second number.</param>
        /// <returns>The subtraction result of the two numbers.</returns>
        public int Subtract(int firstNumber, int secondNumber)
        {
            return firstNumber - secondNumber;
        }

        /// <summary>
        /// Used to multiply two numbers.
        /// </summary>
        /// <param name="firstNumber">The first number.</param>
        /// <param name="secondNumber">The second number.</param>
        /// <returns>The multiplication result of the two numbers.</returns>
        public int Multiply(int firstNumber, int secondNumber)
        {
            return firstNumber * secondNumber;
        }

        /// <summary>
        /// Used to divide two numbers.
        /// </summary>
        /// <param name="firstNumber">The first number.</param>
        /// <param name="secondNumber">The second number.</param>
        /// <returns>The division result of the two numbers.</returns>
        /// <exception cref="DivideByZeroException">Exception thrown when the second input is given as zero.</exception>
        public double Divide(int firstNumber, int secondNumber)
        {
            if (secondNumber == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            return (double) firstNumber / secondNumber;
        }
    }
}