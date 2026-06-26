using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgPart2
{
    public class ActivityLogger
    {
        private readonly List<ActivityEntry> logs = new List<ActivityEntry>();
        private readonly int maxLogs = 15; // Keep last 15 actions

        public void LogAction(string action, string details = "")
        {
            var entry = new ActivityEntry
            {
                Timestamp = DateTime.Now,
                Action = action,
                Details = details
            };

            logs.Add(entry);

            // Keep only the latest logs
            if (logs.Count > maxLogs)
                logs.RemoveAt(0);
        }

        public List<string> GetRecentLogs(int count = 8)
        {
            var recent = new List<string>();
            var sortedLogs = logs.OrderByDescending(l => l.Timestamp).Take(count);

            foreach (var log in sortedLogs)
            {
                recent.Add($"[{log.Timestamp:HH:mm:ss}] {log.Action} - {log.Details}");
            }

            if (recent.Count == 0)
                recent.Add("No activities logged yet.");

            return recent;
        }

        public void ClearLogs()
        {
            logs.Clear();
        }
    }

    public class ActivityEntry
    {
        public DateTime Timestamp { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
    }
}