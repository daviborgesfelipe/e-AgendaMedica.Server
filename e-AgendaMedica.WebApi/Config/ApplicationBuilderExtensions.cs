using e_AgendaMedica.Dominio.Compartilhado.Interfaces;
using e_AgendaMedica.Infra.MassaDados;

namespace e_AgendaMedica.WebApi.Config
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder CriarBancoDadosComMassaDados(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IContextoPersistencia>() as DbContext;

                if (!dbContext.Database.CanConnect())
                {
                    dbContext.Database.EnsureCreated();

                    var geradorMassaDados = scope.ServiceProvider.GetRequiredService<GeradorMassaDados>();
                    geradorMassaDados.GerarDadosAsync().Wait();
                }
            }

            return app;
        }
    }
}
