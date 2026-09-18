namespace CSharpAdvancedFeatures.Class
{
    /// <summary>
    /// Provides a mechanism for notifying subscribers with a message using an event-driven pattern.
    /// </summary>
    internal class Notifier
    {
        /// <summary>
        /// Delegate used to notify subscribers with a message.
        /// </summary>
        /// <param name="message">The message to be delivered to the subscriber.</param>
        public delegate void Notify(string message);

        /// <summary>
        /// Occurs when a notification is triggered.
        /// All subscribed notification handlers are invoked with the provided message.
        /// </summary>
        public event Notify OnAction;

        /// <summary>
        /// Raises the notification event and sends the specified message to all subscribers.
        /// </summary>
        /// <param name="message">The message to send to subscribers.</param>
        public void NotifyUsers(string message)
        {
            this.OnAction?.Invoke(message);
        }
    }
}