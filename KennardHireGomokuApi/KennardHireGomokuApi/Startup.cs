using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KennardHireGomokuApi.Implementations.BusinessLogics;
using KennardHireGomokuApi.Implementations.Repositories;
using KennardHireGomokuApi.Implementations.Services;
using KennardHireGomokuApi.Interfaces;

using KennardHireGomokuApiApi.Middlewares;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace KennardHireGomokuApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "KennardHireGomokuApi", Version = "v1" });
			});
			services.AddSingleton<IGomokuService, GomokuService>()
						.AddSingleton<IGomokuLogicEngine, GomokuLogicEngine>()
						.AddSingleton<IGomokuTemporalRepository, GomokuTemporalRepository>()
						.AddAutoMapper(typeof(Startup));
						
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMiddleware<TracingMiddleware>();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KennardHireGomokuApi v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
