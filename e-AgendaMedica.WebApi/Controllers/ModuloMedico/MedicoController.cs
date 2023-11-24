using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.Dominio.ModuloMedico.Interfaces;
using e_AgendaMedica.WebApi.Controllers.Compartilhado;
using e_AgendaMedica.WebApi.ViewModels.ModuloMedico;

namespace e_AgendaMedica.WebApi.Controllers.ModuloMedico
{
    [Route("api/medicos")]
    [ApiController]
    public class MedicoController : ApiControllerBase
    {
        private IMapper mapeador;
        private IServicoMedico servicoMedico;


        public MedicoController(IMapper mapeador, IServicoMedico servicoMedico)
        {
            this.mapeador = mapeador;
            this.servicoMedico = servicoMedico;
        }

        [HttpPost]
        [ProducesResponseType(typeof(InserirMedicoViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Post(InserirMedicoViewModel medicoViewModel)
        {
            var contatoMap = mapeador.Map<Medico>(medicoViewModel);

            var resultadoPost = await servicoMedico.InserirAsync(contatoMap);

            return ProcessarResultado(resultadoPost.ToResult(), contatoMap);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> GetAll()
        {
            var resultadoGetAll = await servicoMedico.ObterTodosAsync();

            if (resultadoGetAll.IsFailed)
                return NotFound(resultadoGetAll.Errors);

            return Ok(mapeador.Map<List<ListarMedicoViewModel>>(resultadoGetAll.Value));
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> GetCompleteById(Guid id)
        {
            var resultadoGet = await servicoMedico.ObterPorIdAsync(id);

            if (resultadoGet.IsFailed)
                return NotFound(resultadoGet.Errors);

            return Ok(mapeador.Map<VisualizarMedicoViewModel>(resultadoGet.Value));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ListarMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var resultadoGet = await servicoMedico.ObterPorIdAsync(id);

            if (resultadoGet.IsFailed)
                return NotFound(resultadoGet.Errors);

            return Ok(mapeador.Map<ListarMedicoViewModel>(resultadoGet.Value));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EditarMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Put(
            Guid id,
            EditarMedicoViewModel medicoViewModel
        )
        {
            var resultadoGet = await servicoMedico.ObterPorIdAsync(id);

            if (resultadoGet.IsFailed)
                return NotFound(resultadoGet.Errors);

            var medico = mapeador.Map(medicoViewModel, resultadoGet.Value);

            var resultadoPut = await servicoMedico.EditarAsync(medico);

            return ProcessarResultado(resultadoPut.ToResult(), medicoViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var resultadoSelecao = await servicoMedico.ObterPorIdAsync(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var resultadoDelete = await servicoMedico.ExcluirAsync(resultadoSelecao.Value);

            return ProcessarResultado(resultadoDelete.ToResult());
        }
    }
}
