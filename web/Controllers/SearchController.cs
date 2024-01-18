using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web.Models;
using System.Linq; // Needed for LINQ queries
using Microsoft.EntityFrameworkCore;
using web.Data;
using Microsoft.AspNetCore.Authorization;
using web.ViewModels; // Needed for Entity Framework
using Microsoft.AspNetCore.Mvc.Rendering;


namespace web.Controllers;

public class SearchController : Controller
{
    private readonly AzureContext _context; // Use your actual DbContext class

    public SearchController(AzureContext context) // Dependency injection for DbContext
    {
        _context = context;
    }


    [Authorize]
    public async Task<IActionResult> Index()
    {
        var viewModel = new SearchViewModel
        {
            Jobs = new SelectList(await _context.Jobs.ToListAsync(), "JobID", "JobName")
        };
        return View(viewModel);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> SearchResults(int SelectedJobID)
    {
        var workers = await _context.Worker
                                .Include(w => w.Job) // Eager load the Job data
                                .Where(w => w.Job.JobID == SelectedJobID) // Filter based on JobID
                                .ToListAsync();

        return View("SearchResults", workers); // Return the search results to the SearchResults view
    }
}
