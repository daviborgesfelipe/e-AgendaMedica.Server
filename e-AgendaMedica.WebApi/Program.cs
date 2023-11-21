using FluentValidation;
using System.Globalization;

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

            builder.Services.AddCors(opt =>
                                     {
                                         opt.AddPolicy("Desenvolvimento",
                                         policy => policy.WithOrigins("http://localhost:4200")
                                                         .AllowAnyHeader()
                                                         .AllowAnyMethod());
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
   //CORS 
            // Verificar se o banco de dados existe
            app.CriarBancoDadosComMassaDados();

            app.UseHttpsRedirection();

            app.UseCors("Desenvolvimento");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}