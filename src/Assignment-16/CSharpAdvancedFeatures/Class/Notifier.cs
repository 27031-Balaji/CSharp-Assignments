namespace CSharpAdvancedFeatures.Class
{
    internal class Notifier
    {
        public delegate void Notify(string message);

        public event Notify OnAction;

        public void NotifyUsers(string message)
        {
            this.OnAction?.Invoke(message);
        }
    }
}