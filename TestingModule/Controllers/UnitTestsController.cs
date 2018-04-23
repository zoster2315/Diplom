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
    public class UnitTestsController : Controller
    {
        private readonly TestsContext _context;

        public UnitTestsController(TestsContext context)
        {
            _context = context;
        }

        // GET: UnitTests
        public async Task<IActionResult> Index()
        {
            var testsContext = _context.UnitTests.Include(u => u.Duty);
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
        public IActionResult Create()
        {
            ViewData["DutyId"] = new SelectList(_context.Duties, "ID", "Name");
            return View();
        }

        // POST: UnitTests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Arguments,Value,DutyId")] UnitTest unitTest)
        {
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

            var unitTest = await _context.UnitTests.SingleOrDefaultAsync(m => m.ID == id);
            if (unitTest == null)
            {
                return NotFound();
            }
            ViewData["DutyId"] = new SelectList(_context.Duties, "ID", "Name", unitTest.DutyId);
            return View(unitTest);
        }

        // POST: UnitTests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Arguments,Value,DutyId")] UnitTest unitTest)
        {
            if (id != unitTest.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unitTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitTestExists(unitTest.ID))
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
    }
}
