using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Implementations.BusinessLogics;
using KennardHireGomokuApi.Implementations.Repositories;
using KennardHireGomokuApi.Implementations.Services;
using KennardHireGomokuApi.Interfaces;
using KennardHireGomokuApi.Interfaces.RuleLogic;

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
						.AddSingleton<IGomokuLogicEngine, GomokuLogicEngine>(x=>{

									//Validators
									var list =	from t in Assembly
													.GetExecutingAssembly()
													.GetTypes()
												where t.GetInterfaces()
														.Contains(typeof(IDirectionalLogicValidator))
												select t;

									IDictionary<ValidatorType, IDirectionalLogicValidator> validatorLookup = new Dictionary<ValidatorType, IDirectionalLogicValidator>();

									foreach (Type validator in list)
									{
										var instance = Activator.CreateInstance(validator) as IDirectionalLogicValidator;
										validatorLookup[instance.Type] = instance;
									}

									// Rule Checks
									var rules = from t in Assembly
													.GetExecutingAssembly()
													.GetTypes()
												where t.GetInterfaces()
													.Contains(typeof(IRuleChecker))
												select t;

									IList<IRuleChecker> rulesLookup = new List<IRuleChecker>();

									foreach (Type ruleChecker in rules)
									{
										var instance = Activator.CreateInstance(ruleChecker, validatorLookup) as IRuleChecker;
										rulesLookup.Add(instance);
									}

									//General Rule Validators
									IDictionary<ValidatorType, IGeneralRuleValidator> generalRulesValidatorLookup = new Dictionary<ValidatorType, IGeneralRuleValidator>();
									var generalRuleList= from t in Assembly
															.GetExecutingAssembly()
															.GetTypes()
															where t.GetInterfaces()
																	.Contains(typeof(IGeneralRuleValidator))
															select t;
									foreach (Type validator in generalRuleList)
									{
										var instance = Activator.CreateInstance(validator) as IGeneralRuleValidator;
										generalRulesValidatorLookup[instance.Type] = instance;
									}
							var logger = x.GetRequiredService<ILogger<GomokuLogicEngine>>();
							return new GomokuLogicEngine(logger
															, generalRulesValidatorLookup
															, validatorLookup
															, rulesLookup);
						})
						.AddSingleton<IGomokuTemporalRepository, GomokuTemporalRepository>()
						.AddAutoMapper(typeof(Startup));
			services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);

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
