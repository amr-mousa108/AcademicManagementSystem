using AcademicManagementSystem.Data;
using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagementSystem.Controllers
{
    public class CrsResultController : Controller
    {
        private readonly AppDbContext _context;

        public CrsResultController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CrsResult
        public IActionResult Index()
        {
            var crsResults = _context.crsResults
                 .Include(t => t.Trainee)
                 .Include(c => c.Course)
                 .OrderBy(cr => cr.Course.Name)    
                 .ThenBy(cr => cr.Trainee.Name)    
                 .ToList();


            return View(crsResults);
        }

        // GET: CrsResult/Details/5
        public IActionResult Details(int id)
        {
            CrsResult? crsResult = _context.crsResults
                .Include(c => c.Course)
                    .ThenInclude(d => d.Department)
                .Include(c => c.Course)
                    .ThenInclude(i => i.Instructors)
                .Include(t => t.Trainee)
                    .ThenInclude(d => d.Department)
                .FirstOrDefault(c => c.Id == id);

            if (crsResult == null)
            {
                return NotFound();
            }

            return View(crsResult);
        }

        // GET: CrsResult/Create
        public IActionResult Create()
        {
            ViewBag.Trainees = new SelectList(_context.Trainees.OrderBy(t => t.Name), "Id", "Name");
            ViewBag.Courses = new SelectList(_context.Courses.OrderBy(c => c.Name), "Id", "Name");
            return View();
        }

        // POST: CrsResult/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrsResult crsResult)
        {
            ModelState.Remove(nameof(crsResult.Trainee));
            ModelState.Remove(nameof(crsResult.Course));

            if (ModelState.IsValid)
            {
                var existingResult = await _context.crsResults
                    .FirstOrDefaultAsync(cr => cr.Trainee_Id == crsResult.Trainee_Id && cr.Crs_Id == crsResult.Crs_Id);

                if (existingResult != null)
                {
                    ModelState.AddModelError("", "This trainee already has a result for this course.");
                    ViewBag.Trainees = new SelectList(_context.Trainees.OrderBy(t => t.Name), "Id", "Name", crsResult.Trainee_Id);
                    ViewBag.Courses = new SelectList(_context.Courses.OrderBy(c => c.Name), "Id", "Name", crsResult.Crs_Id);
                    return View(crsResult);
                }

                // التحقق من الدرجة
                var course = await _context.Courses.FindAsync(crsResult.Crs_Id);
                if (course != null && crsResult.Degree > course.Degree)
                {
                    ModelState.AddModelError("Degree", $"Degree cannot exceed course maximum degree ({course.Degree})");
                    ViewBag.Trainees = new SelectList(_context.Trainees.OrderBy(t => t.Name), "Id", "Name", crsResult.Trainee_Id);
                    ViewBag.Courses = new SelectList(_context.Courses.OrderBy(c => c.Name), "Id", "Name", crsResult.Crs_Id);
                    return View(crsResult);
                }

                _context.crsResults.Add(crsResult);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Course result created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Trainees = new SelectList(_context.Trainees.OrderBy(t => t.Name), "Id", "Name", crsResult.Trainee_Id);
            ViewBag.Courses = new SelectList(_context.Courses.OrderBy(c => c.Name), "Id", "Name", crsResult.Crs_Id);
            return View(crsResult);
        }

        // GET: CrsResult/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var crsResult = await _context.crsResults.FindAsync(id);
            if (crsResult == null)
            {
                return NotFound();
            }

            ViewBag.Trainees = new SelectList(_context.Trainees.OrderBy(t => t.Name), "Id", "Name", crsResult.Trainee_Id);
            ViewBag.Courses = new SelectList(_context.Courses.OrderBy(c => c.Name), "Id", "Name", crsResult.Crs_Id);
            return View(crsResult);
        }

        // POST: CrsResult/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CrsResult crsResult)
        {
            if (id != crsResult.Id)
            {
                return BadRequest();
            }

            ModelState.Remove(nameof(crsResult.Trainee));
            ModelState.Remove(nameof(crsResult.Course));

            if (ModelState.IsValid)
            {
                try
                {
                    // التحقق من الدرجة
                    var course = await _context.Courses.FindAsync(crsResult.Crs_Id);
                    if (course != null && crsResult.Degree > course.Degree)
                    {
                        ModelState.AddModelError("Degree", $"Degree cannot exceed course maximum degree ({course.Degree})");
                        ViewBag.Trainees = new SelectList(_context.Trainees.OrderBy(t => t.Name), "Id", "Name", crsResult.Trainee_Id);
                        ViewBag.Courses = new SelectList(_context.Courses.OrderBy(c => c.Name), "Id", "Name", crsResult.Crs_Id);
                        return View(crsResult);
                    }

                    _context.Update(crsResult);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Course result updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.crsResults.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }

            ViewBag.Trainees = new SelectList(_context.Trainees.OrderBy(t => t.Name), "Id", "Name", crsResult.Trainee_Id);
            ViewBag.Courses = new SelectList(_context.Courses.OrderBy(c => c.Name), "Id", "Name", crsResult.Crs_Id);
            return View(crsResult);
        }

        // GET: CrsResult/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var crsResult = await _context.crsResults
                .Include(t => t.Trainee)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (crsResult == null)
            {
                return NotFound();
            }

            return View(crsResult);
        }

        // POST: CrsResult/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crsResult = await _context.crsResults.FindAsync(id);
            if (crsResult != null)
            {
                _context.crsResults.Remove(crsResult);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Course result deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

      
    }
}
