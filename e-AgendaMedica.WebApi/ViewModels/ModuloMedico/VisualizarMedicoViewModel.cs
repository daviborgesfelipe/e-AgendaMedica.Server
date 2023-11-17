using e_AgendaMedica.WebApi.ViewModels.ModuloAtividade;

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
}
