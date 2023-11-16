using e_Agenda.Dominio.Compartilhado;
using e_AgendaMedica.Infra.MassaDados;
using FluentValidation;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text.Json.Serialization;

namespace e_AgendaMedica.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");
            
            builder.Services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.ConfigurarLogger(builder.Logging);

            builder.Services.ConfigurarAutoMapper();

            builder.Services.ConfigurarInjecaoDependencia(builder.Configuration);

            builder.Services.ConfigurarSwagger();

            builder.Services.ConfigurarControllers();

            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
   
            // Verificar se o banco de dados existe
            app.UseMassaDados();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}