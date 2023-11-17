using e_AgendaMedica.Dominio.ModuloAtividade;

namespace e_AgendaMedica.WebApi.ViewModels.ModuloAtividade
{
    public class ListarAtividadesViewModel
    {
        public Guid Id { get; set; }
        public string Paciente { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HorarioTermino { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public TipoAtividadeEnum TipoAtividade { get; set; }
    }
}
