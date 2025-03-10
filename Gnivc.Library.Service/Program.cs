using System.Diagnostics;
using Newtonsoft.Json;
using Serilog;

namespace Gnivc.Test.Service
{
	internal static class Program
	{
		public static async Task Main(string[] args)
		{
			try
			{
				ThreadPool.GetMaxThreads(out int workerThreads, out int completionPortThreads);
				ThreadPool.SetMinThreads((int)(workerThreads * 0.1), (int)(completionPortThreads * 0.1));
				await CreateHostBuilder(args).Build().RunAsync();
			}
			catch (Exception ex)
			{
				var currentProcess = Process.GetCurrentProcess();
				Log.Fatal(ex, "Host terminated unexpectedly");
				Log.Fatal($"Host terminated unexpectedly {JsonConvert.SerializeObject(currentProcess)}");
				throw;
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
					webBuilder.UseDefaultServiceProvider((_, options) => { options.ValidateOnBuild = true; options.ValidateScopes = true; });
				});
	}
}