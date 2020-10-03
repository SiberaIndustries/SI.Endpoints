using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SI.Endpoints.Sample
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvcCore().AddFluentValidation(x => x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));
            services.AddEndpointsRouting(x => x.UseFeatures());
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.EnableFeatureFilter(Configuration.GetValue("FeatureFilter", false));
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample endpoints", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample endpoints V1"));
        }
    }
}
