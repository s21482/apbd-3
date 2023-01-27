using StudentsApi.Models;
using System.IO;

namespace StudentsApi.Services
{
    public class DataParser
    {
        public static HashSet<Student> ReadFromFile()
        {
            StreamReader streamReader = new StreamReader("Data/students.csv");
            HashSet<Student> students = new HashSet<Student>(new Student());

            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                string[] values = line.Split(',');
                if (values.Length != 9)
                {
                    continue;
                }
                if (values.Any(value => string.IsNullOrWhiteSpace(value)))
                {
                    continue;
                }
                {
                    Student student = new Student
                    {
                        Fname = values[0],
                        Lname = values[1],
                        IndexNumber = values[4],
                        Birthdate = values[5],
                        Email = values[6],
                        MothersName = values[7],
                        FathersName = values[8],
                        Studies = new Studies
                        {
                            Name = values[2],
                            Mode = values[3]
                        }
                    };
                    students.Add(student);
                }

            }

            streamReader.Close();
            return students;

        }

        public static void WriteToFile(Student student)
        {
            StreamWriter streamWriter = new StreamWriter("Data/students.csv", true);
            streamWriter.WriteLine(student);
            streamWriter.Close();
        }

        public static void OverwriteFile(HashSet<Student> students)
        {
            StreamWriter streamWriter = new StreamWriter("Data/students.csv");
            foreach (Student student in students)
            {
                streamWriter.WriteLine(student);
            }
            streamWriter.Close();
        }


    }
}
