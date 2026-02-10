using System.ComponentModel.DataAnnotations;

namespace AcademicManagementSystem.Models
{
    public class Trainee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public string? ImgUrl { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "Academic Level must be between 1 and 4")]
        public int AcademicLevel { get; set; }

        [Required]   
        public int DepartmentId { get; set; } // Relationship is configured by EF Core conventions
        public Department Department { get; set; }

        public ICollection<CrsResult> CrsResults { get; set; } = new HashSet<CrsResult>();
    }

}
