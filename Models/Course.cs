using Microsoft.AspNetCore.Cors.Infrastructure;

namespace AcademicManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // Navigation
        public ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
        public ICollection<CrsResult> crsResults { get; set; } = new HashSet<CrsResult>();
    }

}
