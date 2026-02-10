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
            var crsresult = _context.crsResults.FirstOrDefault(i => i.Id == 1);
            crsresult.Degree = 20;
            foreach (var trainee in trainees)
            {
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
            } 
            _context.SaveChanges();

            return Content("Trainees images updated successfully!");
        }

        public IActionResult ShowResult(int id)
        {
            var trainee = _context.Trainees
                .Include(t => t.CrsResults)
                    .ThenInclude(cr => cr.Course)
                .FirstOrDefault(t => t.Id == id);

            if (trainee == null || !trainee.CrsResults.Any())
            {
                return NotFound("No results found for this trainee.");
            }

            var traineeResults = trainee.CrsResults.Select(crsResult => new TraineeInCourse
            {
                TraineeId = id,
                TraineeName = trainee.Name,
                CrsId = crsResult.Crs_Id,
                CrsName = crsResult.Course?.Name ?? "No Course",
                Degree = crsResult.Degree,
                MinDegree = crsResult.Course?.MinDegree ?? 0,
                MaxDegree = crsResult.Course?.Degree ?? 100,
                Percentage = crsResult.Course?.Degree > 0
                    ? Math.Round((double)crsResult.Degree / crsResult.Course.Degree * 100, 2)
                    : 0,
                Color = crsResult.Degree >= crsResult.Course?.MinDegree ? "green" : "red",
                Status = crsResult.Degree >= crsResult.Course?.MinDegree ? "Passed" : "Failed",
                CrsResultId = crsResult.Id  
            }).ToList();

            return View(traineeResults);
        }


        // GET: Trainee/Create
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: Trainee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trainee trainee, IFormFile? imageFile)
        {
            // إزالة validation للحقول اللي مش مطلوبة
            ModelState.Remove(nameof(trainee.Department));
            ModelState.Remove(nameof(trainee.CrsResults));
            ModelState.Remove(nameof(trainee.ImgUrl));

            if (ModelState.IsValid)
            {
                // رفع الصورة
                if (imageFile != null && imageFile.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(imageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("imageFile", "Please upload a valid image (jpg, jpeg, png, gif)");
                        ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", trainee.DepartmentId);
                        return View(trainee);
                    }

                    // استخدام timestamp في اسم الملف
                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var fileName = $"{timestamp}_{Guid.NewGuid()}{extension}";
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/trainees");

                    // إنشاء المجلد إذا لم يكن موجوداً
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream); // استخدم async
                    }

                    trainee.ImgUrl = fileName;
                }
                else
                {
                    trainee.ImgUrl = "default.jpg";
                }

                _context.Trainees.Add(trainee);
                await _context.SaveChangesAsync(); 

                TempData["Success"] = "Trainee created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", trainee.DepartmentId);
            return View(trainee);
        }

            // GET: Trainee/Edit/5
            public IActionResult Edit(int id)
        {
            var trainee = _context.Trainees.Find(id);
            if (trainee == null)
            {
                return NotFound();
            }

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", trainee.DepartmentId);
            return View(trainee);
        }

        // POST: Trainee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trainee trainee, IFormFile? imageFile)
        {
            if (id != trainee.Id)
            {
                return BadRequest();
            }

            ModelState.Remove(nameof(trainee.Department));
            ModelState.Remove(nameof(trainee.CrsResults));
            ModelState.Remove(nameof(trainee.ImgUrl));

            if (ModelState.IsValid)
            {
                try
                {
                    var existingTrainee = await _context.Trainees
                        .AsNoTracking()
                        .FirstOrDefaultAsync(t => t.Id == id);

                    if (existingTrainee == null)
                    {
                        return NotFound();
                    }

                    // الاحتفاظ بالصورة القديمة
                    trainee.ImgUrl = existingTrainee.ImgUrl;

                    // رفع صورة جديدة
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        var extension = Path.GetExtension(imageFile.FileName).ToLower();

                        if (!allowedExtensions.Contains(extension))
                        {
                            ModelState.AddModelError("imageFile", "Please upload a valid image (jpg, jpeg, png, gif)");
                            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", trainee.DepartmentId);
                            return View(trainee);
                        }

                        // حذف الصورة القديمة (إلا إذا كانت default)
                        if (!string.IsNullOrEmpty(existingTrainee.ImgUrl) && existingTrainee.ImgUrl != "default.jpg")
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/trainees", existingTrainee.ImgUrl);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // استخدام timestamp في اسم الملف الجديد
                        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                        var fileName = $"{timestamp}_{Guid.NewGuid()}{extension}";
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/trainees");

                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        trainee.ImgUrl = fileName;
                    }

                    _context.Update(trainee);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Trainee updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Trainees.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", trainee.DepartmentId);
            return View(trainee);
        }
    }

}