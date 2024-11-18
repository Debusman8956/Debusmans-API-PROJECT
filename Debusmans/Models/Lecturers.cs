using Debusman.Api.models;
using System.ComponentModel.DataAnnotations;

namespace Debusman.models
{
    public class Lecturers
    { 
            [Key]
            public int Id { get; set; }

            public string Name { get; set; }
            public string Email { get; set; }
            public string Department { get; set; }

            // Navigation property to related courses
            public ICollection<Courses> CoursesTaught { get; set; }
        }
    }

