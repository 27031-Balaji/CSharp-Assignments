namespace BasicProfiling.Class
{
    /// <summary>
    /// The MemoryEater class is used to allocate memory optimized to store only 100 arrays in a circular manner.
    /// </summary>
    public class MemoryEater
    {
        private const int MemorySize = 100;
        private int _currentIndex;
        private int[][] _memAlloc = new int[MemorySize][];

        /// <summary>
        /// Method used to allocate memory in a circular manner.
        /// </summary>
        public void Allocate()
        {
            while (true)
            {
                this._memAlloc[this._currentIndex] = new int[1000];
                this._currentIndex = (this._currentIndex + 1) % MemorySize;
                Console.WriteLine($"Heap size: {GC.GetTotalMemory(false) / (1024.0 * 1024.0):F2} MB");

                // Assume memAlloc variable is used only within this loop.
                Thread.Sleep(10);
            }
        }
    }
}
