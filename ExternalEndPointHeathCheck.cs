using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace HelloDotNet5
{
    public class ExternalEndPointHeathCheck :IHealthCheck
    {
        private readonly ServiceSettings _settings;


        public ExternalEndPointHeathCheck(IOptions<ServiceSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping ping = new();
            var reply = await ping.SendPingAsync(_settings.OpenWeatherHost);
            if(reply.Status != IPStatus.Success)
            {
                return HealthCheckResult.Unhealthy();
            }
            return HealthCheckResult.Healthy();
        }
    }
}
