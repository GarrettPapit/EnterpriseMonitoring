using EnterpriseMonitor.Api.Hubs;
using EnterpriseMonitor.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Register CORS policy for the frontend dashboard client
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .SetIsOriginAllowed(_ => true)
              .AllowCredentials();
    });
});

// Register MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(EnterpriseMonitor.Core.Telemetry.Queries.GetTelemetryLogsQuery).Assembly);
});

// Register SignalR WebSockets & Background Worker
builder.Services.AddSignalR();
builder.Services.AddHostedService<TelemetryBackgroundService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS middleware
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();
app.MapHub<MonitoringHub>("/hubs/monitoring");

app.Run();