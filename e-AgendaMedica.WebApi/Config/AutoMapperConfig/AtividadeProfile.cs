using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.WebApi.ViewModels.ModuloAtividade;
using Microsoft.OpenApi.Extensions;

namespace e_AgendaMedica.WebApi.Config.AutoMapperConfig
{
    public class AtividadeProfile : Profile
    {
        public AtividadeProfile()
        {
            CreateMap<InserirAtividadeViewModel, Atividade>()
                .ForMember(destino => destino.Data, opt => opt.MapFrom(origem => origem.Data.ToUniversalTime()))
                .ForMember(destino => destino.ListaMedicos, opt => opt.Ignore())
                .AfterMap<InserirAtividadesMappingAction>();


            CreateMap<Atividade, ListarAtividadesViewModel>()
                 .ForMember(destino => destino.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()))
                 .ForMember(destino => destino.HorarioInicio, opt => opt.MapFrom(origem => origem.HorarioInicio.ToString(@"hh\:mm")))
                 .ForMember(destino => destino.HorarioTermino, opt => opt.MapFrom(origem => origem.HorarioTermino.ToString(@"hh\:mm")))
                 .ForMember(destino => destino.TipoAtividade, opt => opt.MapFrom(origem => origem.TipoAtividade.GetDisplayName()));

            CreateMap<Atividade, VisualizarAtividadeViewModel>()
                 .ForMember(destino => destino.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()))
                 .ForMember(destino => destino.HorarioInicio, opt => opt.MapFrom(origem => origem.HorarioInicio.ToString(@"hh\:mm")))
                 .ForMember(destino => destino.HorarioTermino, opt => opt.MapFrom(origem => origem.HorarioTermino.ToString(@"hh\:mm")))
                 .ForMember(destino => destino.TipoAtividade, opt => opt.MapFrom(origem => origem.TipoAtividade.GetDisplayName()));
        }
    }
}
