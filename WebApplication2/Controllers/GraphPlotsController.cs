using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroupChatDemo;
using GroupChatDemo.Models;
using GroupChatDemo.DTOs;

namespace GroupChatDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphPlotsController : ControllerBase
    {
        private readonly GroupChatDbContext _context;

        public GraphPlotsController(GroupChatDbContext context)
        {
            _context = context;
        }

        // GET: api/GraphPlots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GraphPlot>>> GetGraphPlots()
        {
          if (_context.GraphPlots == null)
          {
              return NotFound();
          }
            return await _context.GraphPlots.ToListAsync();
        }

        // GET: api/GraphPlots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GraphPlot>> GetGraphPlot(int id)
        {
          if (_context.GraphPlots == null)
          {
              return NotFound();
          }
            var graphPlot = await _context.GraphPlots.FindAsync(id);

            if (graphPlot == null)
            {
                return NotFound();
            }

            return graphPlot;
        }

        // PUT: api/GraphPlots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGraphPlot(int id, GraphPlot graphPlot)
        {
            if (id != graphPlot.Id)
            {
                return BadRequest();
            }

            _context.Entry(graphPlot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GraphPlotExists(id))
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

        // POST: api/GraphPlots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GraphPlot>> PostGraphPlot(CreatePlotCommand command)
        {
          if (_context.GraphPlots == null)
          {
              return Problem("Entity set 'GroupChatDbContext.GraphPlots'  is null.");
          }
          GraphPlot graphPlot = new GraphPlot { GroupId=command.GroupId, Name=command.Name };
            _context.GraphPlots.Add(graphPlot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGraphPlot", new { id = graphPlot.Id }, graphPlot);
        }

        // DELETE: api/GraphPlots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGraphPlot(int id)
        {
            if (_context.GraphPlots == null)
            {
                return NotFound();
            }
            var graphPlot = await _context.GraphPlots.FindAsync(id);
            if (graphPlot == null)
            {
                return NotFound();
            }

            _context.GraphPlots.Remove(graphPlot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GraphPlotExists(int id)
        {
            return (_context.GraphPlots?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
