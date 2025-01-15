using Microsoft.AspNetCore.Mvc.Filters;

namespace Gnivc.Test.Service.Infrastructure.RequestLimiter
{
	/// <summary>
	/// Фильтр реализующий rate limiter.
	/// </summary>
	public class EmptyRequestsFilter : IAsyncActionFilter
	{
		/// <inheritdoc />
		public async Task OnActionExecutionAsync(
			ActionExecutingContext context,
			ActionExecutionDelegate next)
		{
			await next();
		}
	}
}