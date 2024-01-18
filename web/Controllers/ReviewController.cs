using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using System.Threading.Tasks;
using web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using web.ViewModels;


namespace web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly AzureContext _context;

        public ReviewController(AzureContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Review
        public async Task<IActionResult> Index()
        {
            var viewModel = new ReviewViewModel
            {
                Reviews = await _context.Review.Include(r => r.Worker).ToListAsync(), // Include Worker data
                NewReview = new Review(),
                Workers = new SelectList(await _context.Worker.ToListAsync(), "WorkerID", "WorkerID") // Populate Workers SelectList
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel viewModel)
        {
        
                // Set the WorkerID of the new review based on the selected WorkerID from the dropdown
                if (viewModel.SelectedWorkerID.HasValue) // Ensure there's a selected WorkerID
                {
                    viewModel.NewReview.WorkerID = viewModel.SelectedWorkerID.Value;
                }
                else
                {
                    TempData["ErrorMessage"] = "Error adding review. Worker must be selected.";
                    viewModel.Reviews = await _context.Review.Include(r => r.Worker).ToListAsync(); // Include Worker data
                    viewModel.Workers = new SelectList(await _context.Worker.ToListAsync(), "WorkerID", "WorkerID"); // Repopulate Workers SelectList
                    return View("Index", viewModel);
                }

                _context.Review.Add(viewModel.NewReview);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Review added successfully.";
                return RedirectToAction(nameof(Index));

            TempData["ErrorMessage"] = "Error adding review. Please check your input.";
            // If not valid, reload the Index view with existing reviews and the new review data
            viewModel.Reviews = await _context.Review.Include(r => r.Worker).ToListAsync(); // Include Worker data
            viewModel.Workers = new SelectList(await _context.Worker.ToListAsync(), "WorkerID", "WorkerID"); // Repopulate Workers SelectList
            return View("Index", viewModel);
        }
    }
}
