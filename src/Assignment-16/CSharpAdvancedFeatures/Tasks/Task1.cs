using CSharpAdvancedFeatures.Class;

namespace CSharpAdvancedFeatures.Tasks
{
    /// <summary>
    /// Implements the Task 1 of the advanced features of C#.
    /// </summary>
    internal class Task1
    {
        /// <summary>
        /// Runs the application with the tasks.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Task 1 - Understanding and Implementing Events and Delegates in C#\n");
            Notifier notifier = new Notifier();

            notifier.OnAction += this.SendSMS;
            notifier.OnAction += this.SendEmail;

            notifier.NotifyUsers("Hello Users!");
        }

        /// <summary>
        /// Sends the specified message as an SMS notification.
        /// </summary>
        /// <param name="message">The message to be sent via SMS.</param>
        private void SendSMS(string message)
        {
            Console.WriteLine($"SMS Sent: {message}");
        }

        /// <summary>
        /// Sends the specified message as an email notification.
        /// </summary>
        /// <param name="message">The message to be sent via email.</param>
        private void SendEmail(string message)
        {
            Console.WriteLine($"Email Sent: {message}");
        }
    }
}