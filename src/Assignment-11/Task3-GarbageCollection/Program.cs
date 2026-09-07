using GarbageCollection.Class;

namespace GarbageCollection
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            CreateAndDestroyObjects createAndDestroy = new CreateAndDestroyObjects();
            createAndDestroy.Run();
        }
    }
}