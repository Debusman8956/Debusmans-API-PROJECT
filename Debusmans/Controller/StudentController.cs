using Debusman.Api.models;
using Debusman.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Debusman.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly BazeContext _context;

        // Constructor that accepts BazeContext
        public StudentsController(BazeContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();  // Get all students
            return Ok(students);  // Return the list of students as a response
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();  // Return 404 if student not found
            }

            return Ok(student);  // Return the student
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);  // Add the student to the database
            await _context.SaveChangesAsync();  // Save changes

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);  // Return 201 with the created student details
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();  // Return 400 if the ID does not match
            }

            _context.Entry(student).State = EntityState.Modified;  // Mark the student as modified

            try
            {
                await _context.SaveChangesAsync();  // Save changes to the database
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();  // Return 404 if the student does not exist
                }
                else
                {
                    throw;  // Rethrow exception if a different error occurred
                }
            }

            return NoContent();  // Return 204 on success
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();  // Return 404 if student not found
            }

            _context.Students.Remove(student);  // Remove the student from the database
            await _context.SaveChangesAsync();  // Save changes

            return NoContent();  // Return 204 on success
        }

        // Helper method to check if a student exists
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);  // Check if student exists by ID
        }
    }
}