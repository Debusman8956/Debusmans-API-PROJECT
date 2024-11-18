using Debusman.Api.models;
using Debusman.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Debusman.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly BazeContext _context;

        // Constructor that accepts BazeContext
        public CoursesController(BazeContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courses>>> GetCourses()
        {
            var courses = await _context.Courses.Include(c => c.Lecturers).ToListAsync();  // Include related Lecturers if needed
            return Ok(courses);  // Return the list of courses as a response
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Courses>> GetCourse(int id)
        {
            var course = await _context.Courses.Include(c => c.Lecturers).FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();  // Return 404 if course not found
            }

            return Ok(course);  // Return the course
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<Courses>> PostCourse(Courses course)
        {
            _context.Courses.Add(course);  // Add the course to the database
            await _context.SaveChangesAsync();  // Save changes

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);  // Return 201 with the created course details
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Courses course)
        {
            if (id != course.Id)
            {
                return BadRequest();  // Return 400 if the ID does not match
            }

            _context.Entry(course).State = EntityState.Modified;  // Mark the course as modified

            try
            {
                await _context.SaveChangesAsync();  // Save changes to the database
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();  // Return 404 if the course does not exist
                }
                else
                {
                    throw;  // Rethrow exception if a different error occurred
                }
            }

            return NoContent();  // Return 204 on success
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();  // Return 404 if course not found
            }

            _context.Courses.Remove(course);  // Remove the course from the database
            await _context.SaveChangesAsync();  // Save changes

            return NoContent();  // Return 204 on success
        }

        // Helper method to check if a course exists
        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}