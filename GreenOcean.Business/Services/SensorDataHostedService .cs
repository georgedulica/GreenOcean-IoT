using GreenOcean.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GreenOcean.Business.Services;

public class SensorDataHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public SensorDataHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var time = DateTime.UtcNow;
            
            // Wait for 30 minutes before checking again
            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);

            using (var scope = _serviceProvider.CreateScope())
            {
                var sensorDataVerification = scope.ServiceProvider.GetRequiredService<ISensorDataVerification>();
                await sensorDataVerification.CheckSensorData(time);
            }
        }
    }

}
