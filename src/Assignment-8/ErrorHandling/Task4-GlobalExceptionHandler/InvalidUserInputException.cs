namespace ErrorHandling.CustomException
{
    /// <summary>
    /// This is used to make a new exception when the user enters an invalid user input.
    /// </summary>
    public class InvalidUserInputException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserInputException" /> class.
        /// </summary>
        public InvalidUserInputException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserInputException" /> class.
        /// </summary>
        /// <param name="message">The message to be printed when the exception arises.</param>
        public InvalidUserInputException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserInputException" /> class.
        /// </summary>
        /// <param name="message">The message to be printed when the exception arises.</param>
        /// <param name="innerException">The inner exception that is used to preserve the exception chain.</param>
        public InvalidUserInputException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}