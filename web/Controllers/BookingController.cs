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
        public async Task<IActionResult> Book(int workerId, DateTime bookingDate, TimeSpan bookingTime, string paymentMethod)
        {
            var worker = await _context.Worker.FindAsync(workerId);

            if (worker == null)
            {
                TempData["ErrorMessage"] = "Worker not found.";
                return RedirectToAction("Index", "Search");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var booking = new Booking
            {
                WorkerID = workerId,
                UserID = userId,
                BookingDate = bookingDate,
                BookingTime = bookingTime,
                PaymentMethod = paymentMethod
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Booking successful!";
            return RedirectToAction("Index", "Search");
        }

        // GET: Booking/MyBookings
        public async Task<IActionResult> MyBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bookings = await _context.Bookings
                .Include(b => b.Worker)
                .ThenInclude(w => w.Job)
                .Where(b => b.UserID == userId)
                .ToListAsync();

            return View(bookings);
        }

        // POST: Booking/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                TempData["ErrorMessage"] = "Booking not found.";
                return RedirectToAction(nameof(MyBookings));
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Booking deleted successfully!";
            return RedirectToAction(nameof(MyBookings));
        }
    }
}
