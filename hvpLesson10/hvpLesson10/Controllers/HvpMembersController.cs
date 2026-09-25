
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hvpLesson10.Models;

public class HvpMembersController : Controller
{
    private readonly HvpK24cnt2Lesson10efdbContext _context;

    public HvpMembersController(HvpK24cnt2Lesson10efdbContext context)
    {
        _context = context;
    }

    // GET: HVPMEMBERS
    public async Task<IActionResult> Index()    
    {
        var members = await _context.HvpMembers.ToListAsync();

        return View("~/Views/HvpMembers/Index.cshtml", members);
    }

    // GET: HVPMEMBERS/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hvpmember = await _context.HvpMembers
            .FirstOrDefaultAsync(m => m.Id == id);
        if (hvpmember == null)
        {
            return NotFound();
        }

        return View(hvpmember);
    }

    // GET: HVPMEMBERS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: HVPMEMBERS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,HvpUserName,HvpPassword,HvpFullname,HvpEmail,HvpPhone,HvpStatus")] HvpMember hvpmember)
    {
        if (ModelState.IsValid)
        {
            _context.Add(hvpmember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(hvpmember);
    }

    // GET: HVPMEMBERS/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hvpmember = await _context.HvpMembers.FindAsync(id);
        if (hvpmember == null)
        {
            return NotFound();
        }
        return View(hvpmember);
    }

    // POST: HVPMEMBERS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long? id, [Bind("Id,HvpUserName,HvpPassword,HvpFullname,HvpEmail,HvpPhone,HvpStatus")] HvpMember hvpmember)
    {
        if (id != hvpmember.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(hvpmember);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HvpMemberExists(hvpmember.Id))
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
        return View(hvpmember);
    }

    // GET: HVPMEMBERS/Delete/5
    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hvpmember = await _context.HvpMembers
            .FirstOrDefaultAsync(m => m.Id == id);
        if (hvpmember == null)
        {
            return NotFound();
        }

        return View(hvpmember);
    }

    // POST: HVPMEMBERS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long? id)
    {
        var hvpmember = await _context.HvpMembers.FindAsync(id);
        if (hvpmember != null)
        {
            _context.HvpMembers.Remove(hvpmember);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HvpMemberExists(long? id)
    {
        return _context.HvpMembers.Any(e => e.Id == id);
    }
}
