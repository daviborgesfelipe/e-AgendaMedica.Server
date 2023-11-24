using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace e_AgendaMedica.WebApi.Config
{
    public static class SwaggerConfigExtension
    {
        public static void ConfigurarSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });

                c.MapType<DateTime>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString(DateTime.Now.ToString("yyyy-MM-dd"))
                });
            });
        }
    }
}