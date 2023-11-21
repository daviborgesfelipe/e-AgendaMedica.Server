using e_AgendaMedica.Dominio.ModuloAtividade;
using System.ComponentModel.DataAnnotations;

namespace e_AgendaMedica.WebApi.ViewModels.ModuloAtividade
{
    public class InserirAtividadeViewModel
    {

        public DateTime Data { get; set; }

        public string HorarioTermino { get; set; }

        public string HorarioInicio { get; set; }

        public string Paciente { get; set; }
        public TipoAtividadeEnum TipoAtividade { get; set; }

        public List<Guid> ListaMedicos { get; set; }
    }
}
