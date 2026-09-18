using CSharpAdvancedFeatures.Class;

namespace CSharpAdvancedFeatures.Tasks
{
    /// <summary>
    /// Handles user notifications by subscribing to events and sending messages via SMS and email.
    /// </summary>
    /// <remarks>Subscribes to notification events and executes corresponding actions to notify
    /// users.</remarks>
    internal class Task1
    {
        /// <summary>
        /// Runs the application with the tasks.
        /// </summary>
        public void Run()
        {
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