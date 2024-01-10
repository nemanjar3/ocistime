using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers
{
    public class AppUserController : Controller
    {
        private readonly AzureContext _context;

        public AppUserController(AzureContext context)
        {
            _context = context;
        }

        // GET: AppUser
        public async Task<IActionResult> Index()
        {
              return _context.AppUser != null ? 
                          View(await _context.AppUser.ToListAsync()) :
                          Problem("Entity set 'AzureContext.AppUser'  is null.");
        }

        // GET: AppUser/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AppUser == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.AppUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: AppUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationUser);
        }

        // GET: AppUser/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AppUser == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.AppUser.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: AppUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
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
            return View(applicationUser);
        }

        // GET: AppUser/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AppUser == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.AppUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: AppUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AppUser == null)
            {
                return Problem("Entity set 'OcistimeContext.AppUser'  is null.");
            }
            var applicationUser = await _context.AppUser.FindAsync(id);
            if (applicationUser != null)
            {
                _context.AppUser.Remove(applicationUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(string id)
        {
          return (_context.AppUser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
