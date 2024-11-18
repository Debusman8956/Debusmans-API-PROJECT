using System.ComponentModel.DataAnnotations;

namespace Debusman.models
{
    public class Student
    {


        [Key]
        public int Id { get; set; }
        [Required]
        public String StudentName { get; set; }

        public string StudentEmail { get; set; }

        public string StudentPhone { get; set; }

        public DateTime AdmissionDate { get; set; }

        
        public string StudentPhoneNumber { get; set; }





    }
}
