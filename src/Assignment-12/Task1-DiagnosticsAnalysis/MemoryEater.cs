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

                // Assume memAlloc variable is used only within this loop.
                Thread.Sleep(10);
            }
        }
    }
}
