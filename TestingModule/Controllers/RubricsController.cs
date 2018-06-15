using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestingModule.Data;
using TestingModule.Models;

namespace TestingModule.Controllers
{
  public class RubricsController : Controller
  {
    private readonly TestsContext _context;

    public RubricsController(TestsContext context)
    {
      _context = context;
    }

    // GET: Rubrics
    public async Task<IActionResult> Index(int? bySubject)
    {
      var rubrics = from r in _context.Rubrics select r;
      if (bySubject != null)
        rubrics = rubrics.Where(r => r.SubjectId == bySubject);
      var testsContext = rubrics.Include(r => r.Subject);
      ViewBag.SubjectId = new SelectList(_context.Subjects, "ID", "Name", bySubject);
      return View(await testsContext.AsNoTracking().ToListAsync());
    }

    // GET: Rubrics/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var rubric = await _context.Rubrics
          .Include(r => r.Subject)
          .SingleOrDefaultAsync(m => m.ID == id);
      if (rubric == null)
      {
        return NotFound();
      }

      return View(rubric);
    }

    // GET: Rubrics/Create
    public IActionResult Create()
    {
      ViewData["SubjectId"] = new SelectList(_context.Subjects, "ID", "Name");
      return View();
    }

    // POST: Rubrics/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Name,SubjectId")] Rubric rubric)
    {
      if (ModelState.IsValid)
      {
        _context.Add(rubric);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["SubjectId"] = new SelectList(_context.Subjects, "ID", "Name", rubric.SubjectId);
      return View(rubric);
    }

    // GET: Rubrics/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var rubric = await _context.Rubrics.SingleOrDefaultAsync(m => m.ID == id);
      if (rubric == null)
      {
        return NotFound();
      }
      ViewData["SubjectId"] = new SelectList(_context.Subjects, "ID", "Name", rubric.SubjectId);
      return View(rubric);
    }

    // POST: Rubrics/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Name,SubjectId")] Rubric rubric)
    {
      if (id != rubric.ID)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(rubric);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!RubricExists(rubric.ID))
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
      ViewData["SubjectId"] = new SelectList(_context.Subjects, "ID", "Name", rubric.SubjectId);
      return View(rubric);
    }

    // GET: Rubrics/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var rubric = await _context.Rubrics
          .Include(r => r.Subject)
          .SingleOrDefaultAsync(m => m.ID == id);
      if (rubric == null)
      {
        return NotFound();
      }

      return View(rubric);
    }

    // POST: Rubrics/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var rubric = await _context.Rubrics.SingleOrDefaultAsync(m => m.ID == id);
      _context.Rubrics.Remove(rubric);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool RubricExists(int id)
    {
      return _context.Rubrics.Any(e => e.ID == id);
    }

    public class RubricItem
    {
      public int ID { get; set; }
      public string Name { get; set; }
      public string SubjectName { get; set; }
      public int SubjectId { get; set; }
    }

    public IActionResult GetRubrics(int idSubject)
    {
      var rubrics = from r in _context.Rubrics select r;
      if (idSubject != 0)
        rubrics = rubrics.Where(r => r.SubjectId == idSubject);
      var testsContext = rubrics.Include(r => r.Subject);
      var rubricList = new List<RubricItem>();
      foreach (Rubric r in testsContext)
      {
        var rubricItem = new RubricItem();
        rubricItem.ID = r.ID;
        rubricItem.Name = r.Name;
        rubricItem.SubjectName = r.Subject.Name;
        rubricItem.SubjectId = r.SubjectId;
        rubricList.Add(rubricItem);
      }
      return Json(rubricList);
    }
  }
}
