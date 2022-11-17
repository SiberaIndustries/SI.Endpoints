using FluentValidation;
using Microsoft.OpenApi.Models;

namespace SI.Endpoints.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var mode = builder.Configuration.GetValue("mode", "swashbuckle").ToUpper();
            var useFeatures = builder.Configuration.GetValue("useFeatures", true);
            var ignoreNames = builder.Configuration.GetValue("ignoreNames", true);
            var replaceTags = builder.Configuration.GetValue("replaceTags", true);
            var prefix = builder.Configuration.GetValue("prefix", "");

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsRouting(x =>
            {
                x.UseFeatures(useFeatures);
                x.IgnoreEndpointNames(ignoreNames);
                x.WithPrefix(prefix);
            });
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            if (mode == "NSWAG")
            {
                builder.Services.AddOpenApiDocument(options => options.EnableFeatureFilter(replaceTags));
            }
            else
            {
                builder.Services.AddSwaggerGen(options =>
                {
                    options.EnableAnnotations();
                    options.EnableFeatureFilter(replaceTags);
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample endpoints", Version = "v1" });
                });
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapControllers();
            if (mode == "NSWAG")
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            else
            {
                SwaggerBuilderExtensions.UseSwagger(app); // app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Run();
        }
    }
}
