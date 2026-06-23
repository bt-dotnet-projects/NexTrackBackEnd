using System;

namespace TrackerBackEnd.Models
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public string MachineName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string ProcessName { get; set; } = string.Empty;
        public string WindowTitle { get; set; } = string.Empty;
        public string ApplicationName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public int IdleTime { get; set; }
        public int Keystrokes { get; set; }
        public int MouseClicks { get; set; }
        public int MouseScrolls { get; set; }
        public DateTime SyncTime { get; set; }
        public DateTime Created { get; set;  }
    }
}
