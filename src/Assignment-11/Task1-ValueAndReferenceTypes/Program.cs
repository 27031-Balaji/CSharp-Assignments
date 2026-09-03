using ValueAndReferenceTypes.Class;
using ValueAndReferenceTypes.Struct;

namespace ValueAndReferenceTypes
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            // Value type variable
            Student student = new Student { StudentId = 1, StudentName = "John Doe" };

            // Reference type variable
            College college = new College { CollegeId = 505, CollegeName = "ABC Engineering College" };

            Console.WriteLine($"The value type before change of name: {student.StudentName}");
            Console.WriteLine($"The reference type before change of name: {college.CollegeName}\n");

            // Change the name of both type variables
            ChangeName(student, college);

            Console.WriteLine($"The value type after change of name: {student.StudentName}");
            Console.WriteLine($"The reference type after change of name: {college.CollegeName}");
            Console.ReadKey();
        }

        /// <summary>
        /// This method is used to change the name of the value and reference type variable.
        /// </summary>
        /// <param name="student">The student struct as a value type.</param>
        /// <param name="college">The college object as a reference type</param>
        public static void ChangeName(Student student, College college)
        {
            student.StudentName = "Lorem Ipsum";
            college.CollegeName = "XYZ PolyTechnic College";
        }
    }
}