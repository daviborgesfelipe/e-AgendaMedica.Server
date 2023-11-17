using AutoMapper;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.WebApi.ViewModels.ModuloMedico;

namespace e_AgendaMedica.WebApi.Config.AutoMapperConfig
{
    public class MedicoProfile : Profile
    {
        public MedicoProfile()
        {
            CreateMap<InserirMedicoViewModel, Medico>();
            CreateMap<EditarMedicoViewModel, Medico>();

            CreateMap<Medico, MedicoComHorasVM>();
            CreateMap<Medico, ListarMedicoViewModel>();
            CreateMap<Medico, VisualizarMedicoViewModel>();
        }
    }
}
