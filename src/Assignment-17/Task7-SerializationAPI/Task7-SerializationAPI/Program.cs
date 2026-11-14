using System.Diagnostics;

namespace SerializationAPI
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    internal class Program
    {
        private const int Iterations = 10000;

        /// <summary>
        /// The method that executes when the application is run.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            Person person = new Person();
            ReflectionSerializer reflectionSerializer = new ReflectionSerializer();
            EmitSerializer emitSerializer = new EmitSerializer();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string reflectionResult = string.Empty;
            for (int i = 0; i < Iterations; i++)
            {
                reflectionResult = reflectionSerializer.Serialize(person);
            }

            stopwatch.Stop();
            Console.WriteLine($"Reflection: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine(reflectionResult);

            Func<object, string> serializer = emitSerializer.CreateSerializer(typeof(Person));
            stopwatch.Restart();
            string emitResult = string.Empty;
            for (int i = 0; i < Iterations; i++)
            {
                emitResult = serializer(person);
            }

            stopwatch.Stop();
            Console.WriteLine($"Reflection.Emit: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine(emitResult);
            Console.ReadKey();
        }
    }
}