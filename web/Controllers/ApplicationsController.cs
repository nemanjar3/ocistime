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
    }
}
