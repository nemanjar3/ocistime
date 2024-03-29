using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers_Api
{
    [Route("api/v1/Worker")]
    [ApiController]
    public class WorkerApiController : ControllerBase
    {
        private readonly AzureContext _context;

        public WorkerApiController(AzureContext context)
        {
            _context = context;
        }

        // GET: api/WorkerApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Worker>>> GetWorker()
        {
          if (_context.Worker == null)
          {
              return NotFound();
          }
            return await _context.Worker.ToListAsync();
        }

        // GET: api/WorkerApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Worker>> GetWorker(int id)
        {
          if (_context.Worker == null)
          {
              return NotFound();
          }
            var worker = await _context.Worker.FindAsync(id);

            if (worker == null)
            {
                return NotFound();
            }

            return worker;
        }

        // PUT: api/WorkerApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorker(int id, Worker worker)
        {
            if (id != worker.WorkerID)
            {
                return BadRequest();
            }

            _context.Entry(worker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WorkerApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Worker>> PostWorker(Worker worker)
        {
          if (_context.Worker == null)
          {
              return Problem("Entity set 'AzureContext.Worker'  is null.");
          }
            _context.Worker.Add(worker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorker", new { id = worker.WorkerID }, worker);
        }

        // DELETE: api/WorkerApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            if (_context.Worker == null)
            {
                return NotFound();
            }
            var worker = await _context.Worker.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            _context.Worker.Remove(worker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkerExists(int id)
        {
            return (_context.Worker?.Any(e => e.WorkerID == id)).GetValueOrDefault();
        }
    }
}
