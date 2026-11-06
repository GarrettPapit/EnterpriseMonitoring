using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace EnterpriseMonitor.Api.Hubs
{
    public class MonitoringHub : Hub
    {
        // Clients can connect and listen to 'ReceiveTelemetryUpdate' and 'ReceiveActivityAlert'
        public async Task BroadcastMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveActivityAlert", user, message);
        }
    }
}