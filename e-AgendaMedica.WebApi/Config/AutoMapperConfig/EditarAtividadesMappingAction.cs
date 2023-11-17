using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico.Interfaces;
using e_AgendaMedica.WebApi.ViewModels.ModuloAtividade;

namespace e_AgendaMedica.WebApi.Config.AutoMapperConfig
{
    public class EditarAtividadesMappingAction : IMappingAction<EditarAtividadeViewModel, Atividade>
    {
        private readonly IRepositorioMedico repositorioMedico;

        public EditarAtividadesMappingAction(IRepositorioMedico repositorioMedico)
        {
            this.repositorioMedico = repositorioMedico;
        }

        public void Process(EditarAtividadeViewModel source, Atividade destination, ResolutionContext context)
        {
            // Mapear os GUIDs dos médicos existentes
            var listaExistenteGuids = destination.ListaMedicos.Select(medico => medico.Id).ToList();

            // Adicionar apenas os novos GUIDs que não estão na lista existente
            var novosGuids = source.ListaMedicos.Except(listaExistenteGuids).ToList();

            // Adicionar novos médicos à lista existente

            if (destination.ListaMedicos != null)
            {
                foreach (var novoGuid in novosGuids)
                {
                    var novoMedico = repositorioMedico.ObterPorIdAsync(novoGuid);
                    if (novoMedico != null)
                    {
                        destination.ListaMedicos.Add(novoMedico.Result);
                    }
                }
            }
        }
    }
}