using e_AgendaMedica.Aplicacao.ModuloAtividade;
using e_AgendaMedica.Aplicacao.ModuloMedico;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.WebApi.Controllers.Compartilhado;
using e_AgendaMedica.WebApi.ViewModels.ModuloAtividade;
using e_AgendaMedica.WebApi.ViewModels.ModuloMedico;

namespace e_AgendaMedica.WebApi.Controllers.ModuloAtividade
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadeController : ApiControllerBase
    {
        private IMapper mapeador;
        private ServicoAtividade servicoAtividade;

        public AtividadeController(IMapper mapeador, ServicoAtividade servicoMedico)
        {
            this.mapeador = mapeador;
            this.servicoAtividade = servicoMedico;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Post(InserirAtividadeViewModel atividadeViewModel)
        {
            var atividadeMap = mapeador.Map<Atividade>(atividadeViewModel);

            var resultadoPost = await servicoAtividade.InserirAsync(atividadeMap);

            return ProcessarResultado(resultadoPost.ToResult(), atividadeMap);
        }
    }
}
