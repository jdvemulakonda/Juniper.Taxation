using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Juniper.Infrastrutcure.ExternalCommunication.Service;
using Juniper.Taxation.Core.Application;
using Juniper.Taxation.Core.Application.Interfaces;
using Juniper.Taxation.Filters;
using Juniper.Taxation.Infrastructure.Providers.TaxJar;
using Juniper.Taxation.Mappers;
using Juniper.Taxation.Middleware;

namespace Juniper.Taxation
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
            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.OperationFilter<CustomFilters>();
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Juniper Tax Api Swagger Documentation", Version = "v1" });
            });

            services.AddHttpClient("TaxJarHttpClient");

            services.AddMvcCore(options => options.AllowEmptyInputInBodyModelBinding = true)
                .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddScoped<IHttpClientAdapter, HttpClientAdapter>();

            services.AddScoped<ITaxProviderService, TaxJarService>();
            services.AddScoped<ITaxCalculationService, TaxCalculationService>();

            services.AddHttpContextAccessor();
            services.AddOptions();
            ConsumerKeyProviderConfiguration opts = new ConsumerKeyProviderConfiguration();
            Configuration.GetSection("ConsumerKeyProviderConfiguration").Bind(opts);
            services.Configure<ConsumerKeyProviderConfiguration>(Configuration.GetSection("ConsumerKeyProviderConfiguration"));

            services.AddAutoMapper(typeof(Startup));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Documentation");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void CreateAutomapperConfiguration()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<TaxProfile>();
                }
            );

            config.AssertConfigurationIsValid();
            config.CompileMappings();
        }
    }
}
