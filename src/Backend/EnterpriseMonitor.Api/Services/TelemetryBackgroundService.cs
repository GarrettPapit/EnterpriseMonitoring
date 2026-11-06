using EnterpriseMonitor.Api.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseMonitor.Api.Services
{
    public class TelemetryBackgroundService : BackgroundService
    {
        private readonly IHubContext<MonitoringHub> _hubContext;
        private readonly ILogger<TelemetryBackgroundService> _logger;

        public TelemetryBackgroundService(IHubContext<MonitoringHub> hubContext, ILogger<TelemetryBackgroundService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Telemetry Background Worker started successfully.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var telemetryPayload = new
                {
                    MachineName = "ENTERPRISE-NODE-01",
                    ActivityType = "SYSTEM_TELEMETRY_HEARTBEAT",
                    Timestamp = DateTime.UtcNow,
                    CpuUsagePercent = Random.Shared.Next(12, 78),
                    ActiveConnections = Random.Shared.Next(100, 550)
                };

                // Broadcast real-time packet to all connected SignalR WebSocket clients
                await _hubContext.Clients.All.SendAsync("ReceiveTelemetryUpdate", telemetryPayload, stoppingToken);

                // Tick every 3 seconds
                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}