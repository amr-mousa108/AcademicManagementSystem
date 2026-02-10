using Microsoft.AspNetCore.Cors.Infrastructure;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace AcademicManagementSystem.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Range(40, 120, ErrorMessage = "Max Degree must be between 40 and 120")]
        public int Degree { get; set; }   // الدرجة النهائية

        [Required]
        [Range(20, 60, ErrorMessage = "Min Degree must be between 20 and 60")]
        public int MinDegree { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // Navigation
        public ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
        public ICollection<CrsResult> crsResults { get; set; } = new HashSet<CrsResult>();
    }


}
