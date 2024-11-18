
using Debusman.models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Debusman.Api.models
{
    public class Courses
    {
        [Key]
        public int Id { get; set; }

        public String Course_Name { get; set; } = string.Empty;
        [StringLength(25)]
        public string Course_Code { get; set; } = string.Empty;
        [JsonIgnore]

        public List<Lecturers>? Lecturers  { get; set; } 
    }
}
