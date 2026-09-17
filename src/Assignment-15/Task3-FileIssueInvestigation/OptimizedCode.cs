using System.Text;

namespace FileIssueInvestigation.Class
{
    /// <summary>
    /// Represents the optimized code.
    /// </summary>
    internal class OptimizedCode
    {
        /// <summary>
        /// Runs the application with the optimized file operations.
        /// </summary>
        public void Run()
        {
            string path = "optimized_file.txt";
            string data = "This is some test data";

            /*
             Optimized Change 1:
             Removed the unnecessary MemoryStream. The data is already converted
             to a byte array, so storing it again in a MemoryStream and then
             copying it back to another byte array using ToArray() causes
             unnecessary memory allocation and data copying.
            */
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                fileStream.Write(buffer, 0, buffer.Length);
            }

            // Reading from file using FileStream
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    /*
                     Optimized Change 2:
                     Replaced character-by-character output with Encoding.UTF8.GetString().
                     This reduces the number of Console.Write operations and improves
                     readability and performance.
                    */
                    string fileData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine(fileData);
                }
            }
        }
    }
}