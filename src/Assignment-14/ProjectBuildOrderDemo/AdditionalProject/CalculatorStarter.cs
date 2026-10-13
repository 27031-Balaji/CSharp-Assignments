using DisplayApp;

namespace AdditionalProject
{
    /// <summary>
    /// Starts the calculator application.
    /// </summary>
    public class CalculatorStarter
    {
        private readonly ConsoleUI _consoleUI = new ConsoleUI();

        /// <summary>
        /// Starts the calculator application.
        /// </summary>
        public void Start()
        {
            this._consoleUI.Start();
        }
    }
}