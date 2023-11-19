using e_AgendaMedica.Aplicacao.ModuloAtividade;
using e_AgendaMedica.Aplicacao.ModuloAutenticacao;
using e_AgendaMedica.Aplicacao.ModuloMedico;
using e_AgendaMedica.Dominio.Compartilhado.Interfaces;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloAtividade.Interfaces;
using e_AgendaMedica.Dominio.ModuloAutenticacao;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.Dominio.ModuloMedico.Interfaces;
using e_AgendaMedica.Infra.MassaDados;
using e_AgendaMedica.Infra.Orm.Compartilhado;
using e_AgendaMedica.Infra.Orm.ModuloAtividade;
using e_AgendaMedica.Infra.Orm.ModuloMedico;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            services.AddTransient<IServicoMedico, ServicoMedico>();

            services.AddTransient<IRepositorioAtividade, RepositorioAtividadeOrm>();
            services.AddTransient<IValidadorAtividade, ValidadorAtividade>();
            services.AddTransient<IServicoAtividade, ServicoAtividade>();

            services.AddTransient<InserirAtividadesMappingAction>();
            services.AddTransient<EditarAtividadesMappingAction>();

            services.AddTransient<GeradorMassaDados>();

            services.AddIdentity<Usuario, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<eAgendaMedicaDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<MensagensPortuguesBrIdentity>();

            services.AddTransient<ServicoAutenticacao>();
            services.AddTransient<UserManager<Usuario>>();
            services.AddTransient<SignInManager<Usuario>>();
        }
    }
}
