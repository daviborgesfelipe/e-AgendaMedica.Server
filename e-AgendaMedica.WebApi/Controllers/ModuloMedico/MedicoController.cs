using AutoMapper;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.WebApi.ViewModels.ModuloMedico;
using Microsoft.AspNetCore.Mvc;

namespace e_AgendaMedica.WebApi.Controllers.ModuloMedico
{
    [Route("api/medicos")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private IMapper mapeador;

        public MedicoController(IMapper mapeador)
        {
            this.mapeador = mapeador;
        }

        [HttpPost]
        [ProducesResponseType(typeof(InserirMedicoViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Post(InserirMedicoViewModel medicoViewModel)
        {
            var contatoMap = mapeador.Map<Medico>(medicoViewModel);

            return Ok(contatoMap);
        }
    }
}
