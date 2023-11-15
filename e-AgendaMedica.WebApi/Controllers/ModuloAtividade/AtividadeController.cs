using e_AgendaMedica.Aplicacao.ModuloAtividade;
using e_AgendaMedica.Aplicacao.ModuloMedico;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.WebApi.Controllers.Compartilhado;
using e_AgendaMedica.WebApi.ViewModels.ModuloAtividade;
using e_AgendaMedica.WebApi.ViewModels.ModuloMedico;

namespace e_AgendaMedica.WebApi.Controllers.ModuloAtividade
{
    [Route("api/atividades")]
    [ApiController]
    public class AtividadeController : ApiControllerBase
    {
        private IMapper mapeador;
        private IServicoAtividade servicoAtividade;

        public AtividadeController(IMapper mapeador, IServicoAtividade servicoMedico)
        {
            this.mapeador = mapeador;
            this.servicoAtividade = servicoMedico;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Post([FromBody] InserirAtividadeViewModel atividadeViewModel)
        {
            var atividadeMap = mapeador.Map<Atividade>(atividadeViewModel);

            var resultadoPost = await servicoAtividade.InserirAsync(atividadeMap);

            return ProcessarResultado(resultadoPost.ToResult(), atividadeMap);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarAtividadesViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> GetAll()
        {
            var resultadoGetAll = await servicoAtividade.SelecionarTodosAsync();

            if (resultadoGetAll.IsFailed)
                return NotFound(resultadoGetAll.Errors);

            return Ok(mapeador.Map<List<ListarAtividadesViewModel>>(resultadoGetAll.Value));
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarAtividadeViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> GetCompleteById(Guid id)
        {
            var resultadoGet = await servicoAtividade.SelecionarPorIdAsync(id);

            if (resultadoGet.IsFailed)
                return NotFound(resultadoGet.Errors);

            return Ok(mapeador.Map<VisualizarAtividadeViewModel>(resultadoGet.Value));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ListarAtividadesViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var resultadoGet = await servicoAtividade.SelecionarPorIdAsync(id);

            if (resultadoGet.IsFailed)
                return NotFound(resultadoGet.Errors);

            return Ok(mapeador.Map<ListarAtividadesViewModel>(resultadoGet.Value));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EditarAtividadeViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Put(
                Guid id,
            EditarAtividadeViewModel atividadeViewModel
        )
        {
            var resultadoGet = await servicoAtividade.SelecionarPorIdAsync(id);

            if (resultadoGet.IsFailed)
                return NotFound(resultadoGet.Errors);

            Atividade atividade = mapeador.Map(atividadeViewModel, resultadoGet.Value);

            var resultadoPut = await servicoAtividade.EditarAsync(atividade);

            return ProcessarResultado(resultadoPut.ToResult(), atividadeViewModel);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var resultadoSelecao = await servicoAtividade.SelecionarPorIdAsync(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var resultadoDelete = await servicoAtividade.ExcluirAsync(resultadoSelecao.Value);

            return ProcessarResultado(resultadoDelete.ToResult());
        }
    }
}
