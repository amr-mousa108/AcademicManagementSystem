using AcademicManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace AcademicManagementSystem.ViewModel
{
    public class AddInstructors
    {
        [Required(ErrorMessage = "Instructor Name is required.")]
        public string InstructorName { get; set; }

        public string? ImgUrl { get; set; } // Nullable دلوقتي

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        // القيم للـ DropDownLists
        public List<Department> Department { get; set; } = new List<Department>();
        public List<Course> Course { get; set; } = new List<Course>();

        // القيم المختارة
        [Required(ErrorMessage = "Please select a Department.")]
        public int? SelectedDepartmentId { get; set; }

        [Required(ErrorMessage = "Please select a Course.")]
        public int? SelectedCourseId { get; set; }
    }
}
