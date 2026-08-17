using ErrorHandling.Task1;
using ErrorHandling.Task2;

namespace Assignments
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Write("Error Handling in C#\n");
                Console.Write("Enter the option for the task you want to see: \n");
                Console.Write("[A] Task 1\n");
                Console.Write("[B] Task 2\n");
                Console.Write("[C] Task 3\n");
                Console.Write("[D] Task 4\n");
                Console.Write("[E] Task 5\n");
                Console.Write("[F] Exit\n");
                Console.Write("Enter your option: ");
                string option = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
                switch (option)
                {
                    case "A":
                        Task1 task1 = new Task1();
                        task1.Run();
                        break;

                    case "B":
                        Task2 task2 = new Task2();
                        task2.Run();
                        break;

                    case "C":
                        break;

                    case "D":
                        break;

                    case "E":
                        break;

                    case "F":
                        isRunning = false;
                        Console.Write("Exiting...");
                        Thread.Sleep(1000);
                        break;

                    default:
                        Console.Write("Invalid Option. Enter a valid one.\n\n");
                        break;
                }
            }
        }
    }
}