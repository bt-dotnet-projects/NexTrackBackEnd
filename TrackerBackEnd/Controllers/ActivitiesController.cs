using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackerBackEnd.Data;
using TrackerBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackerBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly TrackerDbContext _context;

        public ActivitiesController(TrackerDbContext context)
        {
            _context = context;
        }

        // POST: api/Activities
        [HttpPost]
        public async Task<IActionResult> UploadActivities([FromBody] ActivityUploadDto uploadDto)
        {
            if (uploadDto == null || uploadDto.Activities == null || !uploadDto.Activities.Any())
            {
                return BadRequest(new { Message = "No activities provided." });
            }

            var activityLogs = new List<ActivityLog>();

            foreach (var act in uploadDto.Activities)
            {
                var log = new ActivityLog
                {
                    MachineName = act.MachineId ?? string.Empty,
                    UserId = act.EmployeeId ?? string.Empty,
                    ProcessName = act.AppName ?? string.Empty,
                    ApplicationName = act.AppName ?? string.Empty,
                    WindowTitle = act.WindowTitle ?? string.Empty,
                    StartTime = act.StartTimeUtc,
                    EndTime = act.EndTimeUtc,
                    Duration = act.DurationSeconds,
                    SyncTime = DateTime.UtcNow,
                    Created = DateTime.UtcNow
                };

                activityLogs.Add(log);
            }

            await _context.ActivityLogs.AddRangeAsync(activityLogs);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"{activityLogs.Count} activities processed successfully." });
        }
    }

    public class ActivityUploadDto
    {
        public List<ActivityDto> Activities { get; set; } = new();
    }

    public class ActivityDto
    {
        public string Id { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string MachineId { get; set; } = string.Empty;
        public string AppName { get; set; } = string.Empty;
        public string WindowTitle { get; set; } = string.Empty;
        public DateTime StartTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public int DurationSeconds { get; set; }
    }
}
