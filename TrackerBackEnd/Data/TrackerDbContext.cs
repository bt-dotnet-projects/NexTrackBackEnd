using Microsoft.EntityFrameworkCore;
using TrackerBackEnd.Models;

namespace TrackerBackEnd.Data
{
    public class TrackerDbContext : DbContext
    {
        public TrackerDbContext(DbContextOptions<TrackerDbContext> options) : base(options)
        {
        }

        public DbSet<ActivityLog> ActivityLogs { get; set; } = null!;
    }
}
