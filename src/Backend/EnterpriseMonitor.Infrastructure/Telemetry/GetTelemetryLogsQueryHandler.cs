using Dapper;
using EnterpriseMonitor.Core.Telemetry.Dtos;
using EnterpriseMonitor.Core.Telemetry.Queries;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseMonitor.Infrastructure.Telemetry
{
    public class GetTelemetryLogsQueryHandler : IRequestHandler<GetTelemetryLogsQuery, IEnumerable<ActivityLogDto>>
    {
        private readonly string _connectionString;

        public GetTelemetryLogsQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                                ?? "Server=sqlserver,1433;Database=ActivityMonitorDb;User Id=sa;Password=YourStrong@Password2026!;TrustServerCertificate=True;";
        }

        public async Task<IEnumerable<ActivityLogDto>> Handle(GetTelemetryLogsQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            
            var sql = @"
                SELECT Id, MachineName, ActivityType, Timestamp 
                FROM ActivityLogs 
                ORDER BY Timestamp DESC 
                OFFSET @Offset ROWS 
                FETCH NEXT @PageSize ROWS ONLY;";

            var offset = (request.PageNumber - 1) * request.PageSize;

            var logs = await connection.QueryAsync<ActivityLogDto>(sql, new { Offset = offset, PageSize = request.PageSize });
            return logs;
        }
    }
}