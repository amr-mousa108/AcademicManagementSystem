using AcademicManagementSystem.Data;
using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers
{
    public class CrsResultController : Controller
    {
        private readonly AppDbContext _context;

        public CrsResultController(AppDbContext context)  // <-- Injection
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CrsResult> crsResult = _context.crsResults
                .Include(t => t.Trainee)
                .Include(c => c.Course)
                .ThenInclude(d => d.Department)
                .ToList();
                return View(crsResult);
        }

        public IActionResult Details(int id) {

            CrsResult crsResult = _context.crsResults
                 .Include(c => c.Course)
                        .ThenInclude(d => d.Department)
                 .Include(c => c.Course)
                        .ThenInclude(i => i.Instructors)
                 .Include(t => t.Trainee)
                   .FirstOrDefault(c => c.Id == id);

            if (crsResult == null)
            {
                return NotFound();
            }

            return View(crsResult);
        }

    }
}
