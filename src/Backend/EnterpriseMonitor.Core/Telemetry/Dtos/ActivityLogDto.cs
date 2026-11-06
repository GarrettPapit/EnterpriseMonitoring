using System;

namespace EnterpriseMonitor.Core.Telemetry.Dtos
{
    public class ActivityLogDto
    {
        public int Id { get; set; }
        public string MachineName { get; set; } = string.Empty;
        public string ActivityType { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}