using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloAtividade.Interfaces;
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
            if (atividadeViewModel == null)
                return BadRequest(new List<IError> { new Error("Nenhuma propriedade detectada para a atividade.") });

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
            var resultadoGetAll = await servicoAtividade.ObterTodosAsync();

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
            if (id == null)
                return BadRequest(new List<IError> { new Error("Nenhum id detectado para a atividade.") });

            var resultadoGet = await servicoAtividade.ObterPorIdAsync(id);

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
            if (id == null)
                return BadRequest(new List<IError> { new Error("Nenhum id detectado para a atividade.") });

            var resultadoGet = await servicoAtividade.ObterPorIdAsync(id);

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
            if (id == null)
                return BadRequest(new List<IError> { new Error("Nenhum id detectado para a atividade.") });

            if (atividadeViewModel == null)
                return BadRequest(new List<IError> { new Error("Nenhuma alteração detectada para a atividade.") });

            var resultadoGet = await servicoAtividade.ObterPorIdAsync(id);

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
            if (id == null)
                return BadRequest(new List<IError> { new Error("Nenhum id detectado para a atividade.") });

            var resultadoSelecao = await servicoAtividade.ObterPorIdAsync(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var resultadoDelete = await servicoAtividade.ExcluirAsync(resultadoSelecao.Value);

            return ProcessarResultado(resultadoDelete.ToResult());
        }

        [HttpGet("medicos-mais-horas-trabalhadas/{dataInicio:datetime}/{dataTermino:datetime}")]
        [ProducesResponseType(typeof(ListarMedicoViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> ObterMedicosMaisHorasTrabalhadas( DateTime dataInicio, DateTime dataTermino)//route constraint
        {
            var resultadoValidacao = ValidarDatas(dataInicio, dataTermino);

            if (resultadoValidacao.IsFailed)
                return BadRequest(resultadoValidacao.Errors);

            var resultadoGet = await servicoAtividade.ObterMedicosMaisHorasTrabalhadas(dataInicio, dataTermino);

            if (resultadoGet.IsFailed)
                return NotFound(resultadoGet.Errors);

            return Ok(mapeador.Map<List<MedicoComHorasVM>>(resultadoGet.Value));

        }
        private Result ValidarDatas(DateTime dataInicio, DateTime dataTermino)
        {
            if (dataInicio == DateTime.MinValue && dataTermino == DateTime.MinValue)
                return Result.Fail("Data de início e data de término estão ausentes.");

            if (dataInicio == DateTime.MinValue)
                return Result.Fail("Data de início está ausente.");

            if (dataTermino == DateTime.MinValue)
                return Result.Fail("Data de término está ausente.");

            return Result.Ok();
        }
    }
}
