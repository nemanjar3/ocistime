using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
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

        // GET: Review
        public async Task<IActionResult> Index()
        {
            var reviews = await _context.Review.ToListAsync();
            return View(reviews);
        }

        // GET: Review/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewID,WorkerID,Description,Grade")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // Additional actions for Edit and Delete can be added similarly.
    }
}
