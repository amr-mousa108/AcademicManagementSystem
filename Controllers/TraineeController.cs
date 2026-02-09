using AcademicManagementSystem.Data;
using AcademicManagementSystem.Models;
using AcademicManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademicManagementSystem.Controllers
{
    public class TraineeController : Controller
    {
        private readonly AppDbContext _context;

        public TraineeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Trainee> trainees = _context.Trainees
                .Include(c => c.CrsResults)
                .Include(d => d.Department)
                .ToList();

            return View(trainees);
        }


        public IActionResult UpdateTraineeImages()
        {
            // نجيب كل المدرسين
            var trainees = _context.Trainees.ToList();
            var crsresult=_context.crsResults.FirstOrDefault(i => i.Id ==1);
            crsresult.Degree = 20;
            foreach (var trainee in trainees)
            {
                // هنا تحدد الصورة الصحيحة لكل مدرس
                // مثال بسيط حسب Id
                switch (trainee.Id)
                {
                    case 1:
                        trainee.ImgUrl = "0A3A2691.JPG";
                        break;
                    case 2:
                        trainee.ImgUrl = "IMG_1483.jpg";
                        break;
                    case 3:
                        trainee.ImgUrl = "IMG_٢٠٢٤١١٢١_٢٢١١٣٦.jpg";
                        break;
                    default:
                        trainee.ImgUrl = "default.jpg"; // صورة افتراضية
                        break;
                }
            }  // حفظ التغييرات في قاعدة البيانات
            _context.SaveChanges();

            return Content("Trainees images updated successfully!");
        }
      
        public IActionResult ShowResult(int id, int crsId)
        {
            var crsResult = _context.crsResults
                .Include(t => t.Trainee)
                .Include(c => c.Course)
                .FirstOrDefault(c => c.Trainee_Id == id && c.Crs_Id == crsId);

            if (crsResult == null)
            {
                return NotFound("No result found for this trainee in this course.");
            }

            TraineeInCourse traineeInCourse = new TraineeInCourse
            {
                CrsName = crsResult.Course?.Name ?? "No Course",
                TraineeName = crsResult.Trainee?.Name ?? "No Trainee",
                Degree = crsResult.Degree,
            };

            if (crsResult.Degree >= crsResult.Course.MinDegree)
            {
                traineeInCourse.Color = "green";
                traineeInCourse.Status = "Passed";
            }
            else
            {
                traineeInCourse.Color = "red";
                traineeInCourse.Status = "Failed";
            }

            return View(traineeInCourse);
        }

    
}
}
