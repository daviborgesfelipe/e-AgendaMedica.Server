using e_AgendaMedica.WebApi.Filters;
using e_AgendaMedica.WebApi.Config.Converters;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace e_AgendaMedica.WebApi.Config
{
    public static class ControllersConfigExtension
    {
        public static void ConfigurarControllers(this IServiceCollection services)
        {
            services.AddControllers(config => { config.Filters.Add<SerilogActionFilter>(); })
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter());
                });
            ;
        }
    }
}
