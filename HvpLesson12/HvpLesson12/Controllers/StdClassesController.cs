using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HvpLesson12.Entities;
using HvpLesson12.Models;

namespace HvpLesson12.Controllers
{
    public class StdClassesController : Controller
    {
        private readonly StudentDbContext _context;

        public StdClassesController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: StdClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.StdClasses.Include(c => c.Students).ToListAsync());
        }

        // GET: StdClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StdClasses == null)
            {
                return NotFound();
            }

            var stdClass = await _context.StdClasses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stdClass == null)
            {
                return NotFound();
            }

            return View(stdClass);
        }

        // GET: StdClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StdClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassName")] StdClass stdClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stdClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stdClass);
        }

        // GET: StdClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StdClasses == null)
            {
                return NotFound();
            }

            var stdClass = await _context.StdClasses.FindAsync(id);
            if (stdClass == null)
            {
                return NotFound();
            }
            return View(stdClass);
        }

        // POST: StdClasses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassName")] StdClass stdClass)
        {
            if (id != stdClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stdClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StdClassExists(stdClass.Id))
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
            return View(stdClass);
        }

        // GET: StdClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StdClasses == null)
            {
                return NotFound();
            }

            var stdClass = await _context.StdClasses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stdClass == null)
            {
                return NotFound();
            }

            return View(stdClass);
        }

        // POST: StdClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StdClasses == null)
            {
                return Problem("Entity set 'StudentDbContext.StdClasses' is null.");
            }
            var stdClass = await _context.StdClasses.FindAsync(id);
            if (stdClass != null)
            {
                _context.StdClasses.Remove(stdClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StdClassExists(int id)
        {
            return (_context.StdClasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
