using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace AcademicManagementSystem.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public int Salary { get; set; }
        public string Address { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int? CrsId { get; set; }
        [ForeignKey("CrsId")]
        public Course? Course { get; set; }

    }
}
