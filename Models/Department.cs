namespace AcademicManagementSystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }

        public ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        public ICollection<Trainee> Trainees { get; set; } = new HashSet<Trainee>();
    }
}
