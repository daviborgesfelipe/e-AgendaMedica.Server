﻿using e_Agenda.Dominio.Compartilhado;
using e_AgendaMedica.Aplicacao.ModuloAtividade;
using e_AgendaMedica.Aplicacao.ModuloMedico;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.Infra.MassaDados;
using e_AgendaMedica.Infra.Orm.Compartilhado;
using e_AgendaMedica.Infra.Orm.ModuloAtividade;
using e_AgendaMedica.Infra.Orm.ModuloMedico;
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
                //optionsBuilder.UseSqlServer(connectionString);

                optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });

                //optionsBuilder.UseSqlServer(connectionString, options =>
                //{
                //    options.EnableRetryOnFailure();
                //});
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
        }
    }
}
