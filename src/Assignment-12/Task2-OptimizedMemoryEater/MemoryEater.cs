namespace BasicProfiling.Class
{
    /// <summary>
    /// The MemoryEater class is used to allocate memory infinitely causing a memory problem.
    /// </summary>
    public class MemoryEater
    {
        private const int BufferSize = 100;
        private int _currentIndex;
        private int[][] _memAlloc = new int[BufferSize][];

        /// <summary>
        /// Method used to allocate memory infinitely.
        /// </summary>
        public void Allocate()
        {
            while (true)
            {
                this._memAlloc[this._currentIndex] = new int[1000];
                this._currentIndex = (this._currentIndex + 1) % BufferSize;
                Console.WriteLine($"Heap size: {GC.GetTotalMemory(false) / (1024.0 * 1024.0):F2} MB");

                // Assume memAlloc variable is used only within this loop.
                Thread.Sleep(10);
            }
        }
    }
}
