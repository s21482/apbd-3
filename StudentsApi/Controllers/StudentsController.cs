using Microsoft.AspNetCore.Mvc;
using StudentsApi.Models;
using StudentsApi.Services;

namespace StudentsApi.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentsController : ControllerBase
    {
        private static HashSet<Student> _students = new HashSet<Student>();

        [HttpGet]
        public IActionResult GetStudents()
        {
            _students = DataParser.ReadFromFile();
            return Ok(_students);
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber)
        {
            _students = DataParser.ReadFromFile();
            var student = _students.FirstOrDefault(s => s.IndexNumber == indexNumber);
            if (student == null)
            {
                return BadRequest("Student nie istnieje");
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult PostStudent(Student student)
        {
            _students = DataParser.ReadFromFile();


            if (_students.Contains(student))
            {
                return BadRequest("Student z tym nr indeksu ju¿ istnieje");
            }

            DataParser.WriteToFile(student);

            return Ok(student);

        }

        [HttpPut("{indexNumber}")]
        public IActionResult ModifyStudent(string indexNumber, Student student)
        {
            _students = DataParser.ReadFromFile();


            var obj = _students.FirstOrDefault(x => x.IndexNumber == indexNumber);
            if (obj == null)
            {
                return BadRequest("Student z tym nr indeksu nie istnieje");
            }
            obj.Fname = student.Fname;
            obj.Lname = student.Lname;
            obj.Birthdate = student.Birthdate;
            obj.MothersName = student.MothersName;
            obj.FathersName = student.FathersName;
            obj.Studies.Name = student.Studies.Name;
            obj.Studies.Mode = student.Studies.Mode;
            obj.Email = student.Email;

            DataParser.OverwriteFile(_students);

            return Ok(obj);

        }


        [HttpDelete("{indexNumber}")]
        public IActionResult DeleteStudent(string indexNumber)
        {
            _students = DataParser.ReadFromFile();
            bool found = _students.Any(s => s.IndexNumber == indexNumber);
            if (!found)
            {
                return BadRequest("Student z tym nr indeksu nie istnieje");
            }
            var studentToRemove = _students.SingleOrDefault(s => s.IndexNumber == indexNumber);
            _students.Remove(studentToRemove);
            DataParser.OverwriteFile(_students);

            return Ok($"Pomyœlnie usuniêto studenta o numerze indeksu {indexNumber}");
        }

    }
}