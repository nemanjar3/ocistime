using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Data; // Assuming you have a DbContext class in the web.Data namespace
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq; // Add this if not already present
using web.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace web.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly AzureContext _context;

        public ApplicationsController(AzureContext context)
        {
            _context = context;
        }
        
        [Authorize]
        // GET: Applications
        public IActionResult Index()
        {
            var viewModel = new ApplicationViewModel
            {
                Applications = _context.Applications.ToList(),
                NewApplication = new Application(),
                Jobs = new SelectList(_context.Jobs.ToList(), "JobID", "JobName") // Populating the SelectList
            };
            return View(viewModel);
        }

        // POST: Applications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationViewModel application)
        {
                _context.Applications.Add(application.NewApplication);
                await _context.SaveChangesAsync();
                
                TempData["SuccessMessage"] = "Application submitted successfully!";
                return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Accept(int id)
        {
            var application = _context.Applications.FirstOrDefault(a => a.ApplicationID == id);
            if (application != null)
            {
                // Create a new Worker entry
                var worker = new Worker
                {
                    FirstMidName = application.FirstMidName,
                    LastName = application.LastName,
                    Mail = application.Mail,
                    PhoneNumber = application.PhoneNumber,
                    JobID = application.JobID
                };

                // Add the new worker to the Workers table
                _context.Worker.Add(worker);

                // Remove the application from the Applications table
                _context.Applications.Remove(application);

                // Save changes to the database
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Application accepted and moved to Workers.";
            }
            else
            {
                TempData["ErrorMessage"] = "Application not found.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Deny(int id)
        {
            var application = _context.Applications.FirstOrDefault(a => a.ApplicationID == id);
            if (application != null)
            {
                // Remove the application from the Applications table
                _context.Applications.Remove(application);

                // Save changes to the database
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Application denied and deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = "Application not found.";
            }

            return RedirectToAction(nameof(Index));
        }
    
    }
}
