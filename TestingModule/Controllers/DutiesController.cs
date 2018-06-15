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
  public class DutiesController : Controller
  {
    private readonly TestsContext _context;

    public DutiesController(TestsContext context)
    {
      _context = context;
    }

    // GET: Duties
    public async Task<IActionResult> Index(string bySubject, string byRubric)
    {
      var duties = from d in _context.Duties select d;
      var rubrics = _context.Rubrics.Where(r => true);
      if (!string.IsNullOrEmpty(bySubject))
        rubrics = rubrics.Where(r => r.Subject.ToString() == bySubject);
      if (!string.IsNullOrEmpty(byRubric))
        duties = duties.Where(r => r.Rubric.ToString() == byRubric);
      var testsContext = duties.Include(r => r.Rubric);
      ViewBag.SubjectId = new SelectList(_context.Subjects, "ID", "Name", bySubject);
      ViewBag.RubricId = new SelectList(rubrics, "ID", "Name", byRubric);
      return View(await testsContext.ToListAsync());
    }

    // GET: Duties/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var duty = await _context.Duties
          .Include(d => d.Rubric)
          .SingleOrDefaultAsync(m => m.ID == id);
      if (duty == null)
      {
        return NotFound();
      }

      return View(duty);
    }

    // GET: Duties/Create
    public IActionResult Create()
    {
      ViewData["RubricId"] = new SelectList(_context.Rubrics, "ID", "Name");
      return View();
    }

    // POST: Duties/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Name,RubricId,Description")] Duty duty)
    {
      if (ModelState.IsValid)
      {
        _context.Add(duty);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["RubricId"] = new SelectList(_context.Rubrics, "ID", "Name", duty.RubricId);
      return View(duty);
    }

    // GET: Duties/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var duty = await _context.Duties.SingleOrDefaultAsync(m => m.ID == id);
      if (duty == null)
      {
        return NotFound();
      }
      ViewData["RubricId"] = new SelectList(_context.Rubrics, "ID", "Name", duty.RubricId);
      ViewData["Description"] = duty.Description;
      return View(duty);
    }

    // POST: Duties/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Name,RubricId,Description")] Duty duty)
    {
      if (id != duty.ID)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(duty);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!DutyExists(duty.ID))
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
      ViewData["RubricId"] = new SelectList(_context.Rubrics, "ID", "Name", duty.RubricId);
      return View(duty);
    }

    // GET: Duties/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var duty = await _context.Duties
          .Include(d => d.Rubric)
          .SingleOrDefaultAsync(m => m.ID == id);
      if (duty == null)
      {
        return NotFound();
      }

      return View(duty);
    }

    // POST: Duties/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var duty = await _context.Duties.SingleOrDefaultAsync(m => m.ID == id);
      _context.Duties.Remove(duty);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool DutyExists(int id)
    {
      return _context.Duties.Any(e => e.ID == id);
    }

    public IActionResult UpdateRubric(int idSubject)
    {
      var rubrics = _context.Rubrics.Where(r => true);
      if (idSubject != 0)
        rubrics = rubrics.Where(r => r.SubjectId == idSubject);
      var rubricList = new List<Rubric>();
      foreach (Rubric r in rubrics)
        rubricList.Add(r);
      return Json(rubricList);
    }

    public class DutyItem
    {
      public int ID { get; set; }
      public string Name { get; set; }
      public string RubricName { get; set; }
      public int RubricId { get; set; }
      public int SubjectId { get; set; }

    }

    public IActionResult GetDuties(int idRubric)
    {
      var duties = from d in _context.Duties select d;
      if (idRubric != 0)
        duties = duties.Where(r => r.RubricId == idRubric);
      var testsContext = duties.Include(r => r.Rubric);
      var dutiesList = new List<DutyItem>();
      foreach (Duty d in testsContext)
      {
        var dutyItem = new DutyItem();
        dutyItem.ID = d.ID;
        dutyItem.Name = d.Name;
        dutyItem.RubricName = d.Rubric.Name;
        dutyItem.RubricId = d.RubricId;
        dutyItem.SubjectId = d.Rubric.SubjectId;
        dutiesList.Add(dutyItem);
      }
      return Json(dutiesList);
    }
  }
}
