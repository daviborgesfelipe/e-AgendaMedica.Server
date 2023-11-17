using e_AgendaMedica.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.WebApi.ViewModels.ModuloAtividade;

namespace e_AgendaMedica.WebApi.Config.AutoMapperConfig
{
    public class AtividadeProfile : Profile
    {
        public AtividadeProfile()
        {
            CreateMap<InserirAtividadeViewModel, Atividade>()
                .ForMember(destino => destino.Data, opt => opt.MapFrom(origem => origem.Data.ToString(@"dd/MM/yyyy")))
                .ForMember(destino => destino.ListaMedicos, opt => opt.Ignore())
                .AfterMap<InserirAtividadesMappingAction>();

            CreateMap<EditarAtividadeViewModel, Atividade>()
                .ForMember(destino => destino.ListaMedicos, opt => opt.Ignore())
                .AfterMap<EditarAtividadesMappingAction>().ReverseMap();


            CreateMap<Atividade, ListarAtividadesViewModel>()
                 .ForMember(destino => destino.Data, opt => opt.MapFrom(origem => origem.Data.ToString(@"dd/MM/yyyy")))
                 .ForMember(destino => destino.HorarioInicio, opt => opt.MapFrom(origem => origem.HorarioInicio.ToString(@"hh\:mm")))
                 .ForMember(destino => destino.HorarioTermino, opt => opt.MapFrom(origem => origem.HorarioTermino.ToString(@"hh\:mm")))
                 .ForMember(destino => destino.TipoAtividade, opt => opt.MapFrom(origem => origem.TipoAtividade.GetDescription()));

            CreateMap<Atividade, VisualizarAtividadeViewModel>()
                 .ForMember(destino => destino.Data, opt => opt.MapFrom(origem => origem.Data.ToString(@"dd/MM/yyyy")))
                 .ForMember(destino => destino.HorarioInicio, opt => opt.MapFrom(origem => origem.HorarioInicio.ToString(@"hh\:mm")))
                 .ForMember(destino => destino.HorarioTermino, opt => opt.MapFrom(origem => origem.HorarioTermino.ToString(@"hh\:mm")))
                 .ForMember(destino => destino.TipoAtividade, opt => opt.MapFrom(origem => origem.TipoAtividade.GetDescription()));
        }
    }
}
