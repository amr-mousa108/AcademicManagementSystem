using System.ComponentModel.DataAnnotations;

namespace AcademicManagementSystem.Models
{
    public class CrsResult
    {
        [Key]
        public int Id { get; set; }
        public int Degree { get; set; }

        // FKs
        public int Crs_Id { get; set; }
        public int Trainee_Id { get; set; }

        // Navigation
        public Course? Course { get; set; }
        public Trainee? Trainee { get; set; }
    }

}
