namespace Gnivc.Test.Service.Infrastructure.RequestLimiter
{
	/// <summary>
	/// Extensions
	/// </summary>
	public static class AddLimiterExtensions
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="services"></param>
		public static void AddRequestLimiter(this IServiceCollection services)
		{
			services.AddTransient<EmptyRequestsFilter>();
		}
	}
}