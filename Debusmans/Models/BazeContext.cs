using Debusman.Api.models;
using Microsoft.EntityFrameworkCore;

namespace Debusman.models
{
    public class BazeContext: DbContext
    {
        internal object Student;

        public DbSet<Student>Students{ get; set; }

        public DbSet<Courses> Courses { get; set; }

        public DbSet<Lecturers> Lecturers { get; set; }
        

        public BazeContext(DbContextOptions<BazeContext> options) : base(options) { }

    }
}
