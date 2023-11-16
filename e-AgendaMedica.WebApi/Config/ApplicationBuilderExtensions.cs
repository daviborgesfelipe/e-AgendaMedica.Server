using e_Agenda.Dominio.Compartilhado;
using e_AgendaMedica.Infra.MassaDados;

namespace e_AgendaMedica.WebApi.Config
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMassaDados(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IContextoPersistencia>() as DbContext;

                // Verifique se o banco de dados existe
                if (!dbContext.Database.CanConnect())
                {
                    // Se não existir, crie o banco de dados
                    dbContext.Database.EnsureCreated();

                    // Verifique se há migrações pendentes
                    if (dbContext.Database.GetPendingMigrations().Any())
                    {
                        // Se houver, aplique as migrações
                        dbContext.Database.Migrate();
                    }

                    // Agora, gere a massa de dados
                    var geradorMassaDados = scope.ServiceProvider.GetRequiredService<GeradorMassaDados>();
                    geradorMassaDados.GerarDadosAsync().Wait();
                }
            }

            return app;
        }
    }
}
