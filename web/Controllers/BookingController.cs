using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using web.Data;
using web.Models;
using Microsoft.EntityFrameworkCore;

namespace web.Controllers
{
    public class BookingController : Controller
    {
        private readonly AzureContext _context;

        public BookingController(AzureContext context)
        {
            _context = context;
        }

        // POST: Booking/Book
        [HttpPost]
        public async Task<IActionResult> Book(int workerId)
        {
            // Retrieve the worker from the database based on the provided workerId
            var worker = await _context.Worker.FindAsync(workerId); // Note: Ensure the DbSet is named Workers

            if (worker == null)
            {
                // If the worker doesn't exist, handle the error accordingly
                TempData["ErrorMessage"] = "Worker not found.";
                
            }

            // Now that you have the worker details, you can proceed with creating the booking
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Assuming you're using ASP.NET Core Identity

            var booking = new Booking
            {
                WorkerID = workerId,
                UserID = userId, // Use the currently logged-in user's ID
                BookingDate = DateTime.Now // Set the booking date as needed
            };

            // Add the booking to the context and save changes
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Booking successful!";
            return RedirectToAction("Index", "Search"); // Redirect back to search results or another appropriate page
        }

        // GET: Booking/MyBookings
        public async Task<IActionResult> MyBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve the bookings for the currently logged-in user
            var bookings = await _context.Bookings
                .Include(b => b.Worker)
                .ThenInclude(w => w.Job)
                .Where(b => b.UserID == userId)
                .ToListAsync();

            return View(bookings);
        }
    }
}
