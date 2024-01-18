using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using Microsoft.AspNetCore.Authorization;


namespace web.Controllers
{
    public class WorkerController : Controller
    {
        private readonly AzureContext _context;

        public WorkerController(AzureContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Worker
        public async Task<IActionResult> Index()
        {
        if (_context.Worker == null)
        {
            return Problem("Entity set 'AzureContext.Worker' is null.");
        }

        // Include the Job data for each worker
        var workersWithJobs = await _context.Worker
            .Include(w => w.Job) // Make sure to include the Job data
            .ToListAsync();

        return View(workersWithJobs);
        }

        // GET: Worker/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Worker == null)
            {
                return NotFound();
            }

            var worker = await _context.Worker
                .FirstOrDefaultAsync(m => m.WorkerID == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // GET: Worker/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Worker/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkerID,LastName,FirstMidName,Mail,PhoneNumber")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(worker);
        }

        // GET: Worker/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Worker == null)
            {
                return NotFound();
            }

            var worker = await _context.Worker.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            return View(worker);
        }

        // POST: Worker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkerID,LastName,FirstMidName,Mail,Password")] Worker worker)
        {
            if (id != worker.WorkerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerExists(worker.WorkerID))
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
            return View(worker);
        }

        // GET: Worker/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Worker == null)
            {
                return NotFound();
            }

            var worker = await _context.Worker
                .FirstOrDefaultAsync(m => m.WorkerID == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // POST: Worker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Worker == null)
            {
                return Problem("Entity set 'OcistimeContext.Worker'  is null.");
            }
            var worker = await _context.Worker.FindAsync(id);
            if (worker != null)
            {
                _context.Worker.Remove(worker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerExists(int id)
        {
          return (_context.Worker?.Any(e => e.WorkerID == id)).GetValueOrDefault();
        }
    }
}
