using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using System.Threading.Tasks;
using web.ViewModels;
using Microsoft.AspNetCore.Authorization;

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
                Reviews = await _context.Review.ToListAsync(),
                NewReview = new Review()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel viewModel)
        {
            
                _context.Review.Add(viewModel.NewReview);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Review added successfully.";
                return RedirectToAction(nameof(Index));
            
            TempData["ErrorMessage"] = "Error adding review. Please check your input.";
            // If not valid, reload the Index view with existing reviews and the new review data
            viewModel.Reviews = await _context.Review.ToListAsync();
            return View("Index", viewModel);
        }
    }
}
