using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HvpLesson12.Entities;
using HvpLesson12.Models;

namespace HvpLesson12.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentDbContext _context;

        public StudentsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = _context.Students.Include(s => s.StdClass);
            return View(await students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.StdClass)
                .Include(s => s.Marks)
                    .ThenInclude(m => m.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.StdClasses, "Id", "ClassName");
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentName,StudentEmail,StudentPhone,StudentAddress,StudentBirthday,ClassId")] Student student)
        {
            // Check unique email
            if (await _context.Students.AnyAsync(s => s.StudentEmail == student.StudentEmail))
            {
                ModelState.AddModelError("StudentEmail", "Email này đã được sử dụng bởi sinh viên khác.");
            }
            // Check unique phone
            if (await _context.Students.AnyAsync(s => s.StudentPhone == student.StudentPhone))
            {
                ModelState.AddModelError("StudentPhone", "Số điện thoại này đã được sử dụng bởi sinh viên khác.");
            }

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                    var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Student");
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    var path = Path.Combine(folder, fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    student.StudentAvatar = fileName;
                }
                else
                {
                    student.StudentAvatar = "avatar1.png"; // default fallback
                }

                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.StdClasses, "Id", "ClassName", student.ClassId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.StdClasses, "Id", "ClassName", student.ClassId);
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentName,StudentEmail,StudentPhone,StudentAddress,StudentAvatar,StudentBirthday,ClassId")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            // Check unique email excluding current student
            if (await _context.Students.AnyAsync(s => s.StudentEmail == student.StudentEmail && s.Id != id))
            {
                ModelState.AddModelError("StudentEmail", "Email này đã được sử dụng bởi sinh viên khác.");
            }
            // Check unique phone excluding current student
            if (await _context.Students.AnyAsync(s => s.StudentPhone == student.StudentPhone && s.Id != id))
            {
                ModelState.AddModelError("StudentPhone", "Số điện thoại này đã được sử dụng bởi sinh viên khác.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0 && files[0].Length > 0)
                    {
                        var file = files[0];
                        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Student");
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }
                        var path = Path.Combine(folder, fileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        student.StudentAvatar = fileName;
                    }
                    else
                    {
                        var existing = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
                        if (existing != null && string.IsNullOrEmpty(student.StudentAvatar))
                        {
                            student.StudentAvatar = existing.StudentAvatar;
                        }
                    }

                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.StdClasses, "Id", "ClassName", student.ClassId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.StdClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'StudentDbContext.Students' is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
