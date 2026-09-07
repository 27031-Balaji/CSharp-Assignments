using System.Diagnostics;

namespace GarbageCollection.Class
{
    /// <summary>
    /// This class is used to create and destroy objects and test the heap size.
    /// </summary>
    internal class CreateAndDestroyObjects
    {
        /// <summary>
        /// This method is used to run the creation and destruction operation.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Task 3 - Garbage Collection\n");
            Console.WriteLine($"Before: {GC.GetTotalMemory(false):N0} bytes");

            List<LargeObject> objects = new List<LargeObject>();
            for (int i = 0; i < 2000; i++)
            {
                objects.Add(new LargeObject());
            }

            Console.WriteLine($"After object creation: {GC.GetTotalMemory(false):N0} bytes");
            for (int i = 0; i < 2000; i++)
            {
                objects[i] = null!;
            }

            Console.WriteLine($"After creation and destruction: {GC.GetTotalMemory(true):N0} bytes");

            Console.WriteLine("\nTriggering Garbage Collection...");

            Stopwatch stopwatch = Stopwatch.StartNew();
            GC.Collect();
            stopwatch.Stop();

            Console.WriteLine($"GC execution time: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"After GC.Collect(): {GC.GetTotalMemory(true):N0} bytes");
        }
    }
}