using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bookedWorkers = await _context.Bookings
                .Where(b => b.UserID == userId)
                .Select(b => b.Worker)
                .Distinct()
                .ToListAsync();

            var viewModel = new ReviewViewModel
            {
                Reviews = await _context.Review
                    .Include(r => r.Worker)
                    .Where(r => r.UserID == userId)
                    .ToListAsync(), // Include only reviews by the logged-in user
                NewReview = new Review(),
                Workers = new SelectList(bookedWorkers, "WorkerID", "FirstMidName") // Populate Workers SelectList with booked workers
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if the user has a booking with the selected worker
            var hasBooking = await _context.Bookings
                .AnyAsync(b => b.UserID == userId && b.WorkerID == viewModel.SelectedWorkerID);

            if (!hasBooking)
            {
                TempData["ErrorMessage"] = "Error adding review. You can only review workers you have booked.";
                viewModel.Reviews = await _context.Review
                    .Include(r => r.Worker)
                    .Where(r => r.UserID == userId)
                    .ToListAsync(); // Include only reviews by the logged-in user
                var bookedWorkers = await _context.Bookings
                    .Where(b => b.UserID == userId)
                    .Select(b => b.Worker)
                    .Distinct()
                    .ToListAsync();
                viewModel.Workers = new SelectList(bookedWorkers, "WorkerID", "FirstMidName"); // Repopulate Workers SelectList
                return View("Index", viewModel);
            }

            // Set the WorkerID and UserID of the new review
            viewModel.NewReview.WorkerID = viewModel.SelectedWorkerID.Value;
            viewModel.NewReview.UserID = userId;

            if (ModelState.IsValid)
            {
                _context.Review.Add(viewModel.NewReview);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Review added successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Error adding review. Please check your input.";
            // If not valid, reload the Index view with existing reviews and the new review data
            viewModel.Reviews = await _context.Review
                .Include(r => r.Worker)
                .Where(r => r.UserID == userId)
                .ToListAsync(); // Include only reviews by the logged-in user
            var bookedWorkersForReload = await _context.Bookings
                .Where(b => b.UserID == userId)
                .Select(b => b.Worker)
                .Distinct()
                .ToListAsync();
            viewModel.Workers = new SelectList(bookedWorkersForReload, "WorkerID", "FirstMidName"); // Repopulate Workers SelectList
            return View("Index", viewModel);
        }
    }
}
