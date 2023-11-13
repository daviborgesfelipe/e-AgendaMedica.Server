using e_AgendaMedica.Aplicacao.ModuloMedico;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.WebApi.Controllers.Compartilhado;
using e_AgendaMedica.WebApi.ViewModels.ModuloMedico;

namespace e_AgendaMedica.WebApi.Controllers.ModuloMedico
{
    [Route("api/medicos")]
    [ApiController]
    public class MedicoController : ApiControllerBase
    {
        private IMapper mapeador;
        private ServicoMedico servicoMedico;


        public MedicoController(IMapper mapeador, ServicoMedico servicoMedico)
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
    }
}
