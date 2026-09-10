namespace Dictionaries
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            // 4.1 - Create a new dictionary with student name and grades
            Dictionary<string, int> studentGrades = new Dictionary<string, int>();

            // 4.2 - Add five students and their grades
            studentGrades.Add("Lakshmi", 99);
            studentGrades.Add("Vishnu", 89);
            studentGrades.Add("Tarrun", 69);
            studentGrades.Add("Sowndhar", 80);
            studentGrades.Add("Dineshbalaji", 50);

            // 4.3 - Remove a student from the dictionary
            studentGrades.Remove("Dineshbalaji");
            Console.WriteLine($"The student 'Dineshbalaji' has been removed.{Environment.NewLine}");

            // 4.4 - Display all the students and their grades
            Console.WriteLine("The student list with their grades: ");
            foreach (KeyValuePair<string, int> studentGrade in studentGrades)
            {
                Console.WriteLine($"{studentGrade.Key} : {studentGrade.Value}");
            }

            Console.ReadKey();
        }
    }
}