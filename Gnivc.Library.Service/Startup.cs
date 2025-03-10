using Asp.Versioning;
using Gnivc.Library.Data.Shared.Interfaces;
using Gnivc.Test.Data;
using Gnivc.Test.Service.Infrastructure;
using Microsoft.Extensions.Primitives;
using Gnivc.Test.Service.Infrastructure.RequestLimiter;
using Gnivc.Test.Service.Interfaces;
using Gnivc.Test.Service.Services.Mark;
using Microsoft.OpenApi.Models;
using Gnivc.Test;
using Gnivc.Test.Exceptions;
using Gnivc.Test.Service.Infrastructure.Middleware;
using Swashbuckle.AspNetCore.SwaggerUI;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Gnivc.Test.Service
{
	public class Startup
	{
		public Startup(IWebHostEnvironment environment, IConfiguration configuration)
		{
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<ILogger, DefaultLogger>();
			services.AddRequestLimiter();

			services.AddHttpClient();

			services
				.AddControllers()
				.AddNewtonsoftJson();

			services.AddTransient<ILibraryService, LibraryService>(); //TODO Scoped
			services.AddSingleton<ILibraryContext, LibraryContext>();
			
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibraryAPI", Version = "v1" });
				c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
			});
			services.AddEndpointsApiExplorer();
			services.AddApiVersioning(options =>
				{
					options.DefaultApiVersion = new ApiVersion(1);
					options.AssumeDefaultVersionWhenUnspecified = true;
					options.ReportApiVersions = true;
					options.ApiVersionReader = new UrlSegmentApiVersionReader();
				})
				.AddMvc()
				.AddApiExplorer(options =>
				{
					options.GroupNameFormat = "'v'VVV";
					options.SubstituteApiVersionInUrl = true;
				});
			
			services.AddProblemDetails();
			services.AddExceptionHandler<LibraryBookNotFoundExceptionHandler>();
			services.AddExceptionHandler<LibraryBookQueryExceptionHandler>();
			services.AddExceptionHandler<LibraryBookUpdateOrCreateQueryExceptionHandler>();
			services.AddExceptionHandler<DefaultExceptionHandler>();
		}
		
		public void Configure(IApplicationBuilder app)
		{
			app.UseRouting();
			app.UseResponseCaching();
			app.UseExceptionHandler();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			
			app.UseStaticFiles();
			app.UseSwagger();
			app.UseSwaggerUI((Action<SwaggerUIOptions>)(c =>
			{
				c.SwaggerEndpoint("./v1/swagger.json", "Library v1");
				c.InjectStylesheet("/swagger-ui/SwaggerDark.css");

				c.DisplayRequestDuration();
			}));
		}
	}
}
