using AcademicManagementSystem.Data;
using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace AcademicManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        //Get ALl Courses
        public async Task<IActionResult> Index()
        {
            List<Course> courses = await _context.Courses
                .Include(d => d.Department)
                .OrderBy(c => c.Name)
                .ToListAsync();
            return View(courses);
        }


        // GEt: Course/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Instructors)
                .Include(c => c.crsResults)
                    .ThenInclude(r => r.Trainee)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();

            return View(course);
        }
        //GEt:Course/Create
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments.OrderBy(d => d.Name), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Course '{course.Name}' created successfully!";
                return RedirectToAction("Index");
            }

            ViewBag.Departments = new SelectList(_context.Departments.OrderBy(d => d.Name), "Id", "Name", course.DepartmentId);
            return View(course);

        }
        //GEt:Course/Edit/4
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            ViewBag.Departments = new SelectList(_context.Departments.OrderBy(d => d.Name), "Id", "Name", course.DepartmentId);
            return View(course);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.Id) { return BadRequest(); }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"Course '{course.Name}' updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Courses.Any(c => c.Id == id))
                        return NotFound();
                    throw;
                }

            }
            ViewBag.Departments = new SelectList(_context.Departments.OrderBy(d => d.Name), "Id", "Name", course.DepartmentId);
            return View(course);
        }

        // GEt: Course/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Course? course = await _context.Courses
                .Include(c => c.crsResults)
                .Include(c => c.Instructors)
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();
           
            // Check for related entities before deletion
            if (course.crsResults.Any() || course.Instructors.Any())
            {
                TempData["Error"] = $"Cannot delete course '{course.Name}' because it has related results or instructors. Please remove them first.";
                return RedirectToAction(nameof(Index));
            }

            return View(course);

        }

        //Post :Course/delete/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Course? course = await _context.Courses.FindAsync( );
            if (course == null) return NotFound();

            _context.Remove(course);
            await _context.SaveChangesAsync();
           
            TempData["Success"] = $"Course '{course.Name}' deleted successfully!";

            return RedirectToAction("Index");

        }

    }
}