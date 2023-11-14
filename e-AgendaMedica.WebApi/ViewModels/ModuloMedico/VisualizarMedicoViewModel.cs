using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;

namespace e_AgendaMedica.WebApi.ViewModels.ModuloMedico
{
    public class VisualizarMedicoViewModel
    {
        public VisualizarMedicoViewModel()
        {
            ListaAtividades = new List<ListarAtividadesViewModel>();
        }

        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public string CRM { get; set; }
        public List<ListarAtividadesViewModel> ListaAtividades { get; set; }

    }

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
