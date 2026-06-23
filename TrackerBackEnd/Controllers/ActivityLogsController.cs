using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackerBackEnd.Data;
using TrackerBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrackerBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityLogsController : ControllerBase
    {
        private readonly TrackerDbContext _context;

        public ActivityLogsController(TrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/ActivityLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityLog>>> GetActivityLogs()
        {
            return await _context.ActivityLogs.ToListAsync();
        }

        // GET: api/ActivityLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityLog>> GetActivityLog(int id)
        {
            var activityLog = await _context.ActivityLogs.FindAsync(id);

            if (activityLog == null)
            {
                return NotFound();
            }

            return activityLog;
        }

        // PUT: api/ActivityLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityLog(int id, ActivityLog activityLog)
        {
            if (id != activityLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(activityLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityLogExists(id))
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

        // POST: api/ActivityLogs
        [HttpPost]
        public async Task<ActionResult<ActivityLog>> PostActivityLog(ActivityLog activityLog)
        {
            _context.ActivityLogs.Add(activityLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetActivityLog), new { id = activityLog.Id }, activityLog);
        }

        // DELETE: api/ActivityLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityLog(int id)
        {
            var activityLog = await _context.ActivityLogs.FindAsync(id);
            if (activityLog == null)
            {
                return NotFound();
            }

            _context.ActivityLogs.Remove(activityLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ActivityLogs/seed
        [HttpPost("seed")]
        public async Task<IActionResult> SeedActivityLogs()
        {
            var sampleLog = new ActivityLog
            {
                MachineName = "MACM3",
                UserId = "BT001",
                ProcessName = "msedge",
                WindowTitle = "Swagger UI - Profile 1 - Microsoft Edge",
                ApplicationName = "Microsoft Edge",
                StartTime = DateTime.Parse("2026-06-19 10:41:05.8642152"),
                EndTime = DateTime.Parse("2026-06-19 10:41:19.8365889"),
                Duration = 13,
                IdleTime = 0,
                Keystrokes = 4,
                MouseClicks = 1,
                MouseScrolls = 0,
                SyncTime = DateTime.Parse("2026-06-19 10:41:19.8434386")
            };

            _context.ActivityLogs.Add(sampleLog);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Sample data seeded successfully!", Data = sampleLog });
        }

        private bool ActivityLogExists(int id)
        {
            return _context.ActivityLogs.Any(e => e.Id == id);
        }
    }
}
