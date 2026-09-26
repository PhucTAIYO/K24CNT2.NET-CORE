
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hvp2410900061_exam.Models;

public class HvpEmployeesController : Controller
{
    private readonly HvpEmployee2410900061DbContext _context;

    public HvpEmployeesController(HvpEmployee2410900061DbContext context)
    {
        _context = context;
    }

    // GET: HVPEMPLOYEES
    public async Task<IActionResult> Index()    
    {
        var members = await _context.HvpEmployees.ToListAsync();

        return View("~/Views/HvpEmployee/Index.cshtml", members);
    }

    // GET: HVPEMPLOYEES/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hvpemployee = await _context.HvpEmployees
            .FirstOrDefaultAsync(m => m.Id == id);
        if (hvpemployee == null)
        {
            return NotFound();
        }

        return View(hvpemployee);
    }

    // GET: HVPEMPLOYEES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: HVPEMPLOYEES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,HvpName,HvpGender,HvpBirthDay,HvpEmail,HvpPhone,HvpActive")] HvpEmployee hvpemployee)
    {
        if (ModelState.IsValid)
        {
            _context.Add(hvpemployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(hvpemployee);
    }

    // GET: HVPEMPLOYEES/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hvpemployee = await _context.HvpEmployees.FindAsync(id);
        if (hvpemployee == null)
        {
            return NotFound();
        }
        return View(hvpemployee);
    }

    // POST: HVPEMPLOYEES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long? id, [Bind("Id,HvpName,HvpGender,HvpBirthDay,HvpEmail,HvpPhone,HvpActive")] HvpEmployee hvpemployee)
    {
        if (id != hvpemployee.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(hvpemployee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HvpEmployeeExists(hvpemployee.Id))
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
        return View(hvpemployee);
    }

    // GET: HVPEMPLOYEES/Delete/5
    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hvpemployee = await _context.HvpEmployees
            .FirstOrDefaultAsync(m => m.Id == id);
        if (hvpemployee == null)
        {
            return NotFound();
        }

        return View(hvpemployee);
    }

    // POST: HVPEMPLOYEES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long? id)
    {
        var hvpemployee = await _context.HvpEmployees.FindAsync(id);
        if (hvpemployee != null)
        {
            _context.HvpEmployees.Remove(hvpemployee);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HvpEmployeeExists(long? id)
    {
        return _context.HvpEmployees.Any(e => e.Id == id);
    }
}
