using System.Diagnostics;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Gnivc.Test.Service.Infrastructure
{
	public class SystemHealthCheck : IHealthCheck
	{
		private readonly IOptionsMonitor<HealthOptions> _options;

		public SystemHealthCheck(IOptionsMonitor<HealthOptions> options)
		{
			_options = options;
		}

		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
			CancellationToken cancellationToken = default)
		{
			var healthOptions = _options.CurrentValue;

			var currentProcess = Process.GetCurrentProcess();
			var threadCount = currentProcess.Threads.Count;

			var result = threadCount < healthOptions.MaxThreadsCount
				? HealthCheckResult.Healthy($"threadCount {threadCount}")
				: HealthCheckResult.Unhealthy($"threadCount {threadCount}");
			return Task.FromResult(result);
		}
	}
}