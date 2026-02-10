using System.ComponentModel.DataAnnotations;

namespace AcademicManagementSystem.Models
{
    public class CrsResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Degree must be between 0 and 100")]
        public int Degree { get; set; }

        // FKs
        [Required]
        public int Crs_Id { get; set; }

        [Required]
        public int Trainee_Id { get; set; }

        // Navigation
        public Course? Course { get; set; }
        public Trainee? Trainee { get; set; }
    }


}
