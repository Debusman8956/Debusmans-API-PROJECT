
using Debusman.Api.models;
using Debusman.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Debusman.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturersController : ControllerBase
    {
        private readonly BazeContext _context;

        // Constructor that accepts BazeContext
        public LecturersController(BazeContext context)
        {
            _context = context;
        }

        // GET: api/Lecturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lecturers>>> GetLecturers()
        {
            var lecturers = await _context.Lecturers.Include(l => l.CoursesTaught).ToListAsync();  // Include related Courses
            return Ok(lecturers);  // Return list of lecturers
        }

        // GET: api/Lecturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lecturers>> GetLecturer(int id)
        {
            var lecturer = await _context.Lecturers
                                         .Include(l => l.CoursesTaught)  // Include related Courses
                                         .FirstOrDefaultAsync(l => l.Id == id);

            if (lecturer == null)
            {
                return NotFound();  // Return 404 if lecturer not found
            }

            return Ok(lecturer);  // Return the lecturer
        }

        // POST: api/Lecturers
        [HttpPost]
        public async Task<ActionResult<Lecturers>> PostLecturer(Lecturers lecturer)
        {
            _context.Lecturers.Add(lecturer);  // Add the new lecturer to the database
            await _context.SaveChangesAsync();  // Save changes

            return CreatedAtAction(nameof(GetLecturer), new { id = lecturer.Id }, lecturer);  // Return 201 Created with lecturer details
        }

        // PUT: api/Lecturers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLecturer(int id, Lecturers lecturer)
        {
            if (id != lecturer.Id)
            {
                return BadRequest();  // Return 400 if IDs do not match
            }

            _context.Entry(lecturer).State = EntityState.Modified;  // Mark the lecturer as modified

            try
            {
                await _context.SaveChangesAsync();  // Save changes to the database
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturerExists(id))
                {
                    return NotFound();  // Return 404 if the lecturer doesn't exist
                }
                else
                {
                    throw;  // Rethrow the exception if it's a different error
                }
            }

            return NoContent();  // Return 204 No Content if update is successful
        }

        // DELETE: api/Lecturers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecturer(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();  // Return 404 if lecturer not found
            }

            _context.Lecturers.Remove(lecturer);  // Remove the lecturer from the database
            await _context.SaveChangesAsync();  // Save changes

            return NoContent();  // Return 204 No Content after successful deletion
        }

        // Helper method to check if a lecturer exists
        private bool LecturerExists(int id)
        {
            return _context.Lecturers.Any(e => e.Id == id);  // Check if the lecturer exists by ID
        }
    }
}