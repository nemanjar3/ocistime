using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web.Models;
using System.Linq; // Needed for LINQ queries
using Microsoft.EntityFrameworkCore;
using web.Data; // Needed for Entity Framework

namespace web.Controllers;

public class SearchController : Controller
{
    private readonly AzureContext _context; // Use your actual DbContext class

    public SearchController(AzureContext context) // Dependency injection for DbContext
    {
        _context = context;
    }

    public IActionResult Search()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> SearchResults(string jobName)
    {
        var workers = await _context.Worker
                            .Include(w => w.Job) // Eager load the Job data
                            .Where(w => EF.Functions.Like(w.Job.JobName, $"%{jobName}%")) // Filter based on JobName
                            .ToListAsync();

        return View("SearchResults", workers); // Return the search results to the SearchResults view
    }
}
