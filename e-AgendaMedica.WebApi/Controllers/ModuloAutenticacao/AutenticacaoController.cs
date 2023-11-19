using e_AgendaMedica.Aplicacao.ModuloAutenticacao;
using e_AgendaMedica.Dominio.ModuloAutenticacao;
using e_AgendaMedica.WebApi.Controllers.Compartilhado;
using e_AgendaMedica.WebApi.ViewModels.ModuloAutenticacao;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace e_AgendaMedica.WebApi.Controllers.ModuloAutenticacao
{
    [Route("api/contas")]
    [ApiController]
    public class AutenticacaoController : ApiControllerBase
    {
        ServicoAutenticacao servicoAutenticacao;
        IMapper mapper;

        public AutenticacaoController(ServicoAutenticacao servicoAutenticacao, IMapper mapper)
        {
            this.servicoAutenticacao = servicoAutenticacao;
            this.mapper = mapper;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegistrarUsuarioViewModel viewModel) 
        {
            var usuarioMap = mapper.Map<Usuario>(viewModel);

            var usuarioResult = await servicoAutenticacao.RegistrarAsync(usuarioMap, viewModel.Senha);

            return Ok(new
            {
                sucesso = true,
                dados = GerarJwt(usuarioResult.Value)
            });
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar(AutenticarUsuarioViewModel viewModel)
        {
            var usuarioResult = await servicoAutenticacao.Autenticar(viewModel.Login, viewModel.Senha);

            return Ok(new
            {
                sucesso = true,
                dados = GerarJwt(usuarioResult.Value)
            });
        }

        [HttpPost("sair")]
        public async Task<IActionResult> Sair()
        {
            var usuarioResult = await servicoAutenticacao.Sair(UsuarioLogado.Email);

            return Ok(new
            {
                sucesso = true,
                dados = $"Sessão do usuário {UsuarioLogado.Email} removida com sucesso"
            });
        }
        private TokenViewModel GerarJwt(Usuario usuario)
        {
            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, usuario.Email));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.GivenName, usuario.Nome));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SegredoSuperSecretoDoeAgenda");
            DateTime dataExpiracao = DateTime.UtcNow.AddHours(8);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "eAgenda",
                Audience = "http://localhost",
                Subject = identityClaims,
                Expires = dataExpiracao,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new TokenViewModel
            {
                Chave = encodedToken,
                DataExpiracao = dataExpiracao,
                UsuarioToken = new UsuarioTokenViewModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                }
            };

            return response;
        }
    }
}
