using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HvpLesson12.Entities;
using HvpLesson12.Models;

namespace HvpLesson12.Controllers
{
    public class MarksController : Controller
    {
        private readonly StudentDbContext _context;

        public MarksController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: Marks
        public async Task<IActionResult> Index()
        {
            var marks = _context.Marks.Include(m => m.Student).Include(m => m.Subject);
            return View(await marks.ToListAsync());
        }

        // GET: Marks/Details?subjectId=1&studentId=1
        public async Task<IActionResult> Details(int? subjectId, int? studentId)
        {
            if (subjectId == null || studentId == null || _context.Marks == null)
            {
                return NotFound();
            }

            var mark = await _context.Marks
                .Include(m => m.Student)
                .Include(m => m.Subject)
                .FirstOrDefaultAsync(m => m.SubjectId == subjectId && m.StudentId == studentId);
            if (mark == null)
            {
                return NotFound();
            }

            return View(mark);
        }

        // GET: Marks/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentName");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "SubjectName");
            return View();
        }

        // POST: Marks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,StudentId,Score")] Marks mark)
        {
            if (await _context.Marks.AnyAsync(m => m.SubjectId == mark.SubjectId && m.StudentId == mark.StudentId))
            {
                ModelState.AddModelError("", "Sinh viên này đã có điểm môn học này rồi. Vui lòng chọn chỉnh sửa điểm thay vì tạo mới.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(mark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentName", mark.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "SubjectName", mark.SubjectId);
            return View(mark);
        }

        // GET: Marks/Edit?subjectId=1&studentId=1
        public async Task<IActionResult> Edit(int? subjectId, int? studentId)
        {
            if (subjectId == null || studentId == null || _context.Marks == null)
            {
                return NotFound();
            }

            var mark = await _context.Marks
                .Include(m => m.Student)
                .Include(m => m.Subject)
                .FirstOrDefaultAsync(m => m.SubjectId == subjectId && m.StudentId == studentId);
            if (mark == null)
            {
                return NotFound();
            }
            return View(mark);
        }

        // POST: Marks/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int subjectId, int studentId, [Bind("SubjectId,StudentId,Score")] Marks mark)
        {
            if (subjectId != mark.SubjectId || studentId != mark.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarksExists(mark.SubjectId, mark.StudentId))
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
            mark.Student = await _context.Students.FindAsync(studentId);
            mark.Subject = await _context.Subjects.FindAsync(subjectId);
            return View(mark);
        }

        // GET: Marks/Delete?subjectId=1&studentId=1
        public async Task<IActionResult> Delete(int? subjectId, int? studentId)
        {
            if (subjectId == null || studentId == null || _context.Marks == null)
            {
                return NotFound();
            }

            var mark = await _context.Marks
                .Include(m => m.Student)
                .Include(m => m.Subject)
                .FirstOrDefaultAsync(m => m.SubjectId == subjectId && m.StudentId == studentId);
            if (mark == null)
            {
                return NotFound();
            }

            return View(mark);
        }

        // POST: Marks/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int subjectId, int studentId)
        {
            if (_context.Marks == null)
            {
                return Problem("Entity set 'StudentDbContext.Marks' is null.");
            }
            var mark = await _context.Marks.FindAsync(subjectId, studentId);
            if (mark != null)
            {
                _context.Marks.Remove(mark);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarksExists(int subjectId, int studentId)
        {
            return (_context.Marks?.Any(e => e.SubjectId == subjectId && e.StudentId == studentId)).GetValueOrDefault();
        }
    }
}
