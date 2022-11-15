using FluentValidation;
using Microsoft.OpenApi.Models;

namespace SI.Endpoints.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsRouting(x => x.UseFeatures());
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            builder.Services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.EnableFeatureFilter(builder.Configuration.GetValue("FeatureFilter", false));
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample endpoints", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapControllers();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Run();
        }
    }
}
