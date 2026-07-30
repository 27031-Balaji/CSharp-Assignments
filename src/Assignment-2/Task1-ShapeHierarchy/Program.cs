using Task1.Classes;
using static System.Drawing.Color; // Static Directive because using System.Drawing has another class named Rectangle, which conflicts with my Rectangle class.

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Shape Calculator.");
            Shape shape;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose a Shape:");
                Console.WriteLine("[A] Rectangle");
                Console.WriteLine("[B] Circle");
                Console.Write("Enter your choice: ");

                char choice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                if (choice != 'A' && choice != 'B')
                {
                    Console.WriteLine("Enter a valid choice.");
                    continue;
                }

                string color;

                while (true)
                {
                    Console.Write("Enter Color: ");
                    color = Console.ReadLine() ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(color) && FromName(color).IsKnownColor)
                    {
                        break;
                    }

                    Console.WriteLine("Enter a valid color.");
                }

                if (choice == 'A')
                {
                    double length;
                    while (true)
                    {
                        Console.Write("Enter Length: ");
                        if (double.TryParse(Console.ReadLine(), out length) && length > 0)
                        {
                            break;
                        }

                        Console.WriteLine("Length must be greater than zero.");
                    }

                    double breadth;
                    while (true)
                    {
                        Console.Write("Enter Breadth: ");
                        if (double.TryParse(Console.ReadLine(), out breadth) && breadth > 0)
                        {
                            break;
                        }

                        Console.WriteLine("Breadth must be greater than zero.");
                    }

                    shape = new Rectangle(color, length, breadth);
                }
                else
                {
                    double radius;
                    while (true)
                    {
                        Console.Write("Enter Radius: ");
                        if (double.TryParse(Console.ReadLine(), out radius) && radius > 0)
                        {
                            break;
                        }

                        Console.WriteLine("Radius must be greater than zero.");
                    }

                    shape = new Circle(color, radius);
                }

                break;
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose an Operation:");
                Console.WriteLine("[A] Calculate Area");
                Console.WriteLine("[B] Print Details");
                Console.WriteLine("[C] Exit");
                Console.Write("Enter your choice: ");

                char operation = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                switch (operation)
                {
                    case 'A':
                        Console.WriteLine($"Area: {shape.CalculateArea():F2}");
                        break;

                    case 'B':
                        Console.WriteLine(shape.PrintDetails());
                        break;

                    case 'C':
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}