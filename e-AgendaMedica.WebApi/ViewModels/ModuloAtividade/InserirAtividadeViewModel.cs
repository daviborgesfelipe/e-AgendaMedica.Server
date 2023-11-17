using e_AgendaMedica.Dominio.ModuloAtividade;
using System.ComponentModel.DataAnnotations;

namespace e_AgendaMedica.WebApi.ViewModels.ModuloAtividade
{
    public class InserirAtividadeViewModel
    {

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public string HorarioTermino { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public string HorarioInicio { get; set; }

        public string Paciente { get; set; }
        public TipoAtividadeEnum TipoAtividade { get; set; }

        public List<Guid> ListaMedicos { get; set; }
    }
}
