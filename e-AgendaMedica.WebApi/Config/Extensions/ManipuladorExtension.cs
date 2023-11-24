using System.Text.Json;

namespace e_AgendaMedica.WebApi.Config
{
    public class ManipuladorExtension
    {
        private readonly RequestDelegate requestDelegate;

        public ManipuladorExtension(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await this.requestDelegate(ctx);
            }
            catch (Exception ex)
            {
                ctx.Response.StatusCode = 500;
                ctx.Response.ContentType = "application/json";

                var problema = new
                {
                    Sucesso = false,
                    Erros = new List<string> { ex.Message }
                };

                ctx.Response.WriteAsync(JsonSerializer.Serialize(problema));
            }
        }
    }
}
