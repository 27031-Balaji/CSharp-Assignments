namespace BasicProfiling.Class
{
    /// <summary>
    /// The MemoryEater class is used to allocate memory infinitely causing a memory problem.
    /// </summary>
    public class MemoryEater
    {
        private List<int[]> _memAlloc = new List<int[]>();

        /// <summary>
        /// Method used to allocate memory infinitely.
        /// </summary>
        public void Allocate()
        {
            while (true)
            {
                this._memAlloc.Add(new int[1000]);
                Console.WriteLine($"Heap size: {GC.GetTotalMemory(false) / (1024.0 * 1024.0):F2} MB");

                // Assume memAlloc variable is used only within this loop.
                Thread.Sleep(10);
            }
        }
    }
}
