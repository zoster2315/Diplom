﻿using System;
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
  public class UnitTestsController : Controller
  {
    private readonly TestsContext _context;

    public UnitTestsController(TestsContext context)
    {
      _context = context;
    }


    public async Task<IActionResult> IndexForStudent(string bySubject, string byRubric, string byDuty)
    {
      var tests = from t in _context.UnitTests select t;
      var rubrics = _context.Rubrics.Where(r => true);
      if (!string.IsNullOrEmpty(bySubject))
        rubrics = rubrics.Where(r => r.Subject.ToString() == bySubject);

      var duties = _context.Duties.Where(r => true);
      if (!string.IsNullOrEmpty(byRubric))
        duties = duties.Where(r => r.Rubric.ToString() == byRubric);

      if (!string.IsNullOrEmpty(byDuty))
        tests = tests.Where(t => t.Duty.ToString() == byDuty);
      var testsContext = tests.Include(u => u.Duty);


      ViewBag.SubjectId = new SelectList(_context.Subjects, "ID", "Name", bySubject);
      ViewBag.RubricId = new SelectList(rubrics, "ID", "Name", byRubric);
      ViewBag.DutyId = new SelectList(duties, "ID", "Name", byDuty);
      return View(await testsContext.ToListAsync());
    }

    // GET: UnitTests
    public async Task<IActionResult> Index(string bySubject, string byRubric, string byDuty)
    {
      var tests = from t in _context.UnitTests select t;
      var rubrics = _context.Rubrics.Where(r => true);
      if (!string.IsNullOrEmpty(bySubject))
        rubrics = rubrics.Where(r => r.Subject.ToString() == bySubject);

      var duties = _context.Duties.Where(r => true);
      if (!string.IsNullOrEmpty(byRubric))
        duties = duties.Where(r => r.Rubric.ToString() == byRubric);

      if (!string.IsNullOrEmpty(byDuty))
        tests = tests.Where(t => t.Duty.ToString() == byDuty);
      var testsContext = tests.Include(u => u.Duty);


      ViewBag.SubjectId = new SelectList(_context.Subjects, "ID", "Name", bySubject);
      ViewBag.RubricId = new SelectList(rubrics, "ID", "Name", byRubric);
      ViewBag.DutyId = new SelectList(duties, "ID", "Name", byDuty);
      return View(await testsContext.ToListAsync());
    }

    // GET: UnitTests/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var unitTest = await _context.UnitTests
          .Include(u => u.Duty)
          .SingleOrDefaultAsync(m => m.ID == id);
      if (unitTest == null)
      {
        return NotFound();
      }

      return View(unitTest);
    }

    // GET: UnitTests/Create
    public IActionResult Create(int byDuty)
    {
      try
      {
        ViewBag.Description = _context.Duties.First(d => d.ID == byDuty).Description;
      }
      catch (Exception e)
      {
        ViewBag.Description = string.Empty;
      }

      var bySubject = 0;
      var byRubric = 0;

      if (byDuty != 0)
      {
        var duty = _context.Duties.Include(d => d.Rubric).Where(d => d.ID == byDuty).First();
        bySubject = duty.Rubric.SubjectId;
        byRubric = duty.RubricId;

      }
      var rubrics = _context.Rubrics.Where(r => true);
      if (bySubject != 0)
        rubrics = rubrics.Where(r => r.SubjectId == bySubject);

      var duties = _context.Duties.Where(r => true);
      if (byRubric != 0)
        duties = duties.Where(r => r.RubricId == byRubric);

      ViewBag.SubjectId = new SelectList(_context.Subjects, "ID", "Name", bySubject);
      ViewBag.RubricId = new SelectList(rubrics, "ID", "Name", byRubric);
      ViewBag.DutyId = new SelectList(_context.Duties, "ID", "Name", byDuty);
      return View();
    }

    // POST: UnitTests/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Args")] List<string> args, [Bind("Values")] List<string> values, [Bind("DutyId")] int dutyId, [Bind("ID")] int id)
    {

      UnitTest unitTest = new UnitTest();

      ChangeUnitTest(unitTest, args, values, dutyId, id);

      if (ModelState.IsValid)
      {
        _context.Add(unitTest);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["DutyId"] = new SelectList(_context.Duties, "ID", "Name", unitTest.DutyId);
      return View(unitTest);
    }

    // GET: UnitTests/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      ViewBag.Description = _context.Duties.First(d => d.ID == id).Description;

      var unitTest = await _context.UnitTests.SingleOrDefaultAsync(m => m.ID == id);
      if (unitTest == null)
      {
        return NotFound();
      }
      var bySubject = 0;
      var byRubric = 0;

      if (id != 0)
      {
        var duty = _context.Duties.Include(d => d.Rubric).Where(d => d.ID == id).First();
        bySubject = duty.Rubric.SubjectId;
        byRubric = duty.RubricId;

      }
      var rubrics = _context.Rubrics.Where(r => true);
      if (bySubject != 0)
        rubrics = rubrics.Where(r => r.SubjectId == bySubject);

      var duties = _context.Duties.Where(r => true);
      if (byRubric != 0)
        duties = duties.Where(r => r.RubricId == byRubric);

      ViewBag.SubjectId = new SelectList(_context.Subjects, "ID", "Name", bySubject);
      ViewBag.RubricId = new SelectList(rubrics, "ID", "Name", byRubric);
      ViewBag.DutyId = new SelectList(_context.Duties, "ID", "Name", unitTest.DutyId);
      SetArgsAndValues(unitTest);
      return View(unitTest);
    }

    // POST: UnitTests/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Bind("Args")] List<string> args, [Bind("Values")] List<string> values, [Bind("DutyId")] int dutyId, [Bind("ID")] int id)
    {
      UnitTest unitTest = _context.UnitTests.First(t => t.ID == id);

      ChangeUnitTest(unitTest, args, values, dutyId, id);

      if (ModelState.IsValid)
      {
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["DutyId"] = new SelectList(_context.Duties, "ID", "Name", unitTest.DutyId);
      return View(unitTest);
    }

    // GET: UnitTests/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var unitTest = await _context.UnitTests
          .Include(u => u.Duty)
          .SingleOrDefaultAsync(m => m.ID == id);
      if (unitTest == null)
      {
        return NotFound();
      }

      return View(unitTest);
    }

    // POST: UnitTests/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var unitTest = await _context.UnitTests.SingleOrDefaultAsync(m => m.ID == id);
      _context.UnitTests.Remove(unitTest);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool UnitTestExists(int id)
    {
      return _context.UnitTests.Any(e => e.ID == id);
    }

    public IActionResult GetDescription(int DutyID)
    {
      var duty = _context.Duties.First(d => d.ID == DutyID);
      var j = Json(duty.Description);
      return j;
    }

    public void SetArgsAndValues(UnitTest unitTest)
    {
      var args = new List<string>();
      var arguments = unitTest.Arguments;
      while (arguments.Length > 0)
      {
        var argument = arguments.Substring(0, arguments.IndexOf(';') + 1);
        args.Add(argument.Substring(0, argument.IndexOf(';')));
        arguments = arguments.Remove(0, argument.Length);
      }

      var values = new List<string>();
      var allValues = unitTest.Value;
      while (allValues.Length > 0)
      {
        var value = allValues.Substring(0, allValues.IndexOf(';') + 1);
        values.Add(value.Substring(0, value.IndexOf(';')));
        allValues = allValues.Remove(0, value.Length);
      }

      ViewBag.Args = args;
      ViewBag.Values = values;
    }

    public void ChangeUnitTest(UnitTest unitTest, List<string> args, List<string> values, int dutyId, int id)
    {
      var arguments = string.Empty;
      for (int i = 0; i < args.Count; i++)
        arguments += string.Format("{0};", args[i]);
      unitTest.Arguments = arguments;
      var value = string.Empty;
      for (int i = 0; i < values.Count; i++)
        value += string.Format("{0};", values[i]);
      unitTest.Value = value;
      unitTest.ID = id;
      unitTest.DutyId = dutyId;
    }

    public class TestItem
    {
      public int ID { get; set; }
      public string Arguments { get; set; }
      public string DutyName { get; set; }
      public string Values { get; set; }

    }

    public IActionResult GetTests(int idDuty)
    {
      var tests = from t in _context.UnitTests select t;
      if (idDuty != 0)
        tests = tests.Where(r => r.DutyId == idDuty);
      var testsContext = tests.Include(r => r.Duty);
      var dutiesList = new List<TestItem>();
      foreach (UnitTest d in testsContext)
      {
        var testItem = new TestItem();
        testItem.ID = d.ID;
        testItem.Arguments = d.Arguments;
        testItem.Values = d.Value;
        testItem.DutyName = d.Duty.Name;
        dutiesList.Add(testItem);
      }
      return Json(dutiesList);
    }
  }
}
