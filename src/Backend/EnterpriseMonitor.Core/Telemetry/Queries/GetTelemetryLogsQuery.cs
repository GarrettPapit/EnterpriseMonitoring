using MediatR;
using System.Collections.Generic;
using EnterpriseMonitor.Core.Telemetry.Dtos;

namespace EnterpriseMonitor.Core.Telemetry.Queries
{
    public record GetTelemetryLogsQuery(int PageNumber, int PageSize) : IRequest<IEnumerable<ActivityLogDto>>;
}