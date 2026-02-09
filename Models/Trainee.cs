namespace AcademicManagementSystem.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public string Address { get; set; }

        public int Grade { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<CrsResult> CrsResults { get; set; } = new HashSet<CrsResult>();
    }
}
