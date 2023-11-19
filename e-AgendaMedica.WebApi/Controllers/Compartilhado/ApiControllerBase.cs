using e_AgendaMedica.WebApi.ViewModels.ModuloAutenticacao;
using FluentResults;
using System.Security.Claims;

namespace e_AgendaMedica.WebApi.Controllers.Compartilhado
{
    public class ApiControllerBase : ControllerBase
    {
        private UsuarioTokenViewModel usuario;

        public UsuarioTokenViewModel UsuarioLogado
        {
            get
            {
                if (EstaAutenticado())
                {
                    usuario = new UsuarioTokenViewModel();

                    var id = Request?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    if (!string.IsNullOrEmpty(id))
                        usuario.Id = Guid.Parse(id);

                    var nome = Request?.HttpContext?.User?.FindFirst(ClaimTypes.GivenName)?.Value;

                    if (!string.IsNullOrEmpty(nome))
                        usuario.Nome = nome;

                    var email = Request?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

                    if (!string.IsNullOrEmpty(email))
                        usuario.Email = email;

                    return usuario;
                }

                return null;
            }
        }

        protected IActionResult ProcessarResultado(Result result, object viewModel = null)
        {
            if (result.IsFailed)
                return BadRequest(result.Errors);

            return this.Ok(viewModel);
        }

        public override OkObjectResult Ok(object? dados)
        {
            return base.Ok(new
            {
                Sucesso = true,
                Dados = dados
            });
        }

        public override NotFoundObjectResult NotFound(object objetoComErros)
        {
            IList<IError> erros = (List<IError>)objetoComErros;

            return base.NotFound(new
            {
                Sucesso = false,
                Erros = erros.Select(x => x.Message)
            });
        }

        public override BadRequestObjectResult BadRequest(object objetoComErros)
        {
            IList<IError> erros = (List<IError>)objetoComErros;

            return base.BadRequest(new
            {
                Sucesso = false,
                Erros = erros.Select(x => x.Message)
            });
        }

        private bool EstaAutenticado()
        {
            if (Request?.HttpContext?.User?.Identity != null)
                return true;

            return false;
        }
    }
}