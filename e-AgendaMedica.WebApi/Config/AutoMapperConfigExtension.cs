namespace e_AgendaMedica.WebApi.Config.AutoMapperConfig
{
    public static class AutoMapperConfigExtension
    {
        public static void ConfigurarAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(opt =>
            {

            });
        }
    }
}
