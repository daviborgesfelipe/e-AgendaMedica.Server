using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.WebApi.ViewModels.ModuloAtividade;

namespace e_AgendaMedica.WebApi.Config.AutoMapperConfig
{
    public class InserirAtividadesMappingAction : IMappingAction<InserirAtividadeViewModel, Atividade>
    {
        private readonly IRepositorioMedico repositorioMedico;

        public InserirAtividadesMappingAction(IRepositorioMedico repositorioMedico)
        {
            this.repositorioMedico = repositorioMedico;
        }

        public void Process(InserirAtividadeViewModel source, Atividade destination, ResolutionContext context)
        {
            destination.ListaMedicos = repositorioMedico.SelecionarMuitos(source.ListaMedicos);
        }
    }
}
