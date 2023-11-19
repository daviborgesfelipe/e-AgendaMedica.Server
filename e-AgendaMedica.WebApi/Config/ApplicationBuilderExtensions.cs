using e_AgendaMedica.Dominio.Compartilhado.Interfaces;
using e_AgendaMedica.Infra.MassaDados;
using e_AgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace e_AgendaMedica.WebApi.Config
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder CriarBancoDadosComMassaDados(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<eAgendaMedicaDbContext>();

                dbContext.Database.Migrate();

                if (!dbContext.Database.CanConnect())
                {

                    //var migrator = dbContext.Database.GetService<IMigrator>();

                    //migrator.Migrate();

                    dbContext.Database.EnsureCreated();

                    var geradorMassaDados = scope.ServiceProvider.GetRequiredService<GeradorMassaDados>();
                    geradorMassaDados.GerarDadosAsync().Wait();
                }
            }

            return app;
        }
    }
}
