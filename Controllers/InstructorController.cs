using AcademicManagementSystem.Data;
using AcademicManagementSystem.Models;
using AcademicManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.Metrics;
using System.ComponentModel;

namespace AcademicManagementSystem.Controllers
{
    public class InstructorController : Controller
    {
        private readonly AppDbContext _context;

        public InstructorController(AppDbContext context)  // <-- Injection
        {
            _context = context;
        }
        public IActionResult UpdateInstructorImages()
        {
            // نجيب كل المدرسين
            var instructors = _context.Instructors.ToList();

            foreach (var instructor in instructors)
            {
                // هنا تحدد الصورة الصحيحة لكل مدرس
                // مثال بسيط حسب Id
                switch (instructor.Id)
                {
                    case 1:
                        instructor.ImgUrl = "0A3A2691.JPG";
                        break;
                    case 2:
                        instructor.ImgUrl = "IMG_1483.jpg";
                        break;
                    case 3:
                        instructor.ImgUrl = "IMG_٢٠٢٤١١٢١_٢٢١١٣٦.jpg";
                        break;
                    default:
                        instructor.ImgUrl = "default.jpg"; // صورة افتراضية
                        break;
                }
            }

            // حفظ التغييرات في قاعدة البيانات
            _context.SaveChanges();

            return Content("Instructor images updated successfully!");
        }
        public IActionResult Index()
        {
            var ins = _context.Instructors
               .Include(c=> c.Course)
              .Include(d=> d.Department)
               .ToList();
            return View(ins);
        }

        public IActionResult Details(int id) 
        {
            Instructor? ins = _context.Instructors?
                .Include(c => c.Course)
                .Include(d => d.Department)
                .FirstOrDefault(c => c.Id == id);
          
            if (ins == null)
            {
                return NotFound();
            }

            return View(ins);

        }

        [HttpGet]
        public IActionResult New()
        {
            var viewModel = new AddInstructors
            {
                Department = _context.Departments.ToList(),
                Course = _context.Courses.ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(AddInstructors model)
        {
            if (!ModelState.IsValid)
            {
               
                model.Department = _context.Departments.ToList();
                model.Course = _context.Courses.ToList();
                return View("New", model);
            }

            var instructor = new Instructor
            {
                Name = model.InstructorName,
                ImgUrl = model.ImgUrl ?? "", // Nullable
                Salary = model.Salary,
                Address = model.Address,
                DepartmentId = model.SelectedDepartmentId,
                CrsId = model.SelectedCourseId
            };

            _context.Instructors.Add(instructor);
            _context.SaveChanges();

            return RedirectToAction("Index"); 
        }

        
        // GET: Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var instructor = _context.Instructors.SingleOrDefault(i => i.Id == id);
            if (instructor == null)
                return NotFound();

            var viewModel = new EditInstructors
            {
                Id = instructor.Id,
                InstructorName = instructor.Name,
                ImgUrl = instructor.ImgUrl,
                Salary = instructor.Salary,
                Address = instructor.Address,
                SelectedDepartmentId = instructor.DepartmentId,
                SelectedCourseId = instructor.CrsId,
                Department = _context.Departments.ToList(),
                Course = _context.Courses.ToList()
            };

            return View(viewModel);
        }
        //post :Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(EditInstructors model)
        {
            if (ModelState.IsValid)
            {
                Instructor ins = _context.Instructors.SingleOrDefault(i => i.Id == model.Id);
                if (ins == null)
                    return NotFound();
            //edit
                ins.Salary = model.Salary;
                ins.Address = model.Address;
                ins.Name = model.InstructorName;
                ins.DepartmentId = model.SelectedDepartmentId;
                ins.CrsId = model.SelectedCourseId;
                ins.ImgUrl = model.ImgUrl;
               
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {   // ارجع القيم للـ DropDownLists
                model.Department = _context.Departments.ToList();
                model.Course = _context.Courses.ToList();
                return View(model);
            }
        }
    }
}
