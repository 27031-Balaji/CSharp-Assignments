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
            Console.WriteLine("Task 3 - Garbage Collection\n");
            Console.WriteLine($"Before: {GC.GetTotalMemory(false):N0} bytes");

            CreateAndDestroyObjects();
            Console.WriteLine($"After creation/destruction: {GC.GetTotalMemory(false):N0} bytes");
            Console.WriteLine("\nTriggering Garbage Collection...");

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Console.WriteLine($"After GC.Collect(): {GC.GetTotalMemory(false):N0} bytes");
            Console.ReadKey();
        }

        /// <summary>
        /// Method used to create a large list of objects and destroy them using null.
        /// </summary>
        public static void CreateAndDestroyObjects()
        {
            List<LargeObject> objects = new List<LargeObject>();
            for (int i = 0; i < 10000; i++)
            {
                objects.Add(new LargeObject());
            }

            objects.Clear();
            objects = null!;
        }
    }
}