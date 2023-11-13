using e_Agenda.Dominio.Compartilhado;
using e_AgendaMedica.Aplicacao.ModuloMedico;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.Infra.Orm.Compartilhado;
using e_AgendaMedica.Infra.Orm.ModuloMedico;
using Microsoft.EntityFrameworkCore;

namespace e_AgendaMedica.WebApi.Config
{
    public static class InjecaoDependenciaConfigExtension
    {
        public static void ConfigurarInjecaoDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");

            services.AddDbContext<IContextoPersistencia, eAgendaMedicaDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString);
            });

            services.AddTransient<IRepositorioMedico, RepositorioMedicoOrm>();
            services.AddTransient<IValidadorMedico, ValidadorMedico>();
            services.AddTransient<ServicoMedico>();
        }
    }
}
