using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using UniversityBusiness;
using UniversityDataAccess.Repositories;

namespace UniversityOData.Controllers
{
    [Route("odata/Students")]
    [ApiController]
    public class StudentsController : ODataController
    {
        IStudentRepository studentRepository;

        public StudentsController()
        {
            studentRepository = new StudentRepository();
        }

        [EnableQuery]
        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            var list = await studentRepository.GetStudents();
            return Ok(list);
        }

        // GET: api/Students/5
        public async Task<ActionResult> Get([FromODataUri] int key)
        {
            var student = await studentRepository.GetStudentById(key);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Specializations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existStudent = await studentRepository.GetStudentById(key);
            if (existStudent == null)
            {
                return NotFound();
            }
            existStudent.StudentName = student.StudentName;
            existStudent.SpecializationId = student.SpecializationId;
            studentRepository.Update(existStudent);

            return Created(student);
        }

        // POST: api/Specializations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            studentRepository.Add(student);

            return Created(student);
        }

        // DELETE: api/Specializations/5
        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var student = await studentRepository.GetStudentById(key);
            if (student == null)
            {
                return NotFound();
            }

            studentRepository.Delete(student);

            return NoContent();
        }
    }
}
