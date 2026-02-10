using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace AcademicManagementSystem.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public string? ImgUrl { get; set; }

        [Range(3000, 50000)]
        public int Salary { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        public int? DepartmentId { get; set; }// Relationship is configured by EF Core conventions
        public Department? Department { get; set; }

        public int? CrsId { get; set; }

        [ForeignKey("CrsId")]
        public Course? Course { get; set; }
    }


}
