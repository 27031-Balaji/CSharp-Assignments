using CSharpAdvancedFeatures.Class;

namespace CSharpAdvancedFeatures.Tasks
{
    internal class Task7
    {
        public void Run()
        {
            Console.WriteLine("Task 7 - Implementing Advanced Pattern Matching in C# 7.0 and Above\n");
            Console.WriteLine("Creating a list of shape objects...\n");

            List<Shape?> shapesList = new List<Shape?>
            {
                new Circle { Name = "Circle 1", Radius = 10 },
                new Circle { Name = "Circle 2", Radius = 20 },
                null,
                new Rectangle { Name = "Rectangle 1", Length = 10, Width = 20 },
                new Rectangle { Name = "Rectangle 2", Length = 30, Width = 40 },
                null,
                new Triangle { Name = "Triangle 1", Base = 10, Height = 20 },
                new Triangle { Name = "Triangle 2", Base = 30, Height = 40 },
                null,
            };

            Console.WriteLine("Displaying using pattern matching...\n");

            foreach (Shape? shape in shapesList)
            {
                this.DisplayShapeDetails(shape);
            }
        }

        private void DisplayShapeDetails(Shape? shape)
        {
            string details = shape switch
            {
                Circle circle =>
                    $"Circle name: {circle.Name}, " +
                    $"Circle radius: {circle.Radius}, " +
                    $"Circle area: {circle.CalculateArea()}",

                Rectangle rectangle =>
                    $"Rectangle name: {rectangle.Name}, " +
                    $"Rectangle length: {rectangle.Length}, " +
                    $"Rectangle width: {rectangle.Width}, " +
                    $"Rectangle area: {rectangle.CalculateArea()}",

                Triangle triangle =>
                    $"Triangle name: {triangle.Name}, " +
                    $"Triangle base: {triangle.Base}, " +
                    $"Triangle height: {triangle.Height}, " +
                    $"Triangle area: {triangle.CalculateArea()}",

                null =>
                    $"Shape name: None",

                _ =>
                    $"Unknown shape type."
            };

            Console.WriteLine(details);
        }
    }
}