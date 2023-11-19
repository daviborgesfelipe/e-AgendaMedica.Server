using e_AgendaMedica.Dominio.ModuloAutenticacao;
using e_AgendaMedica.WebApi.ViewModels.ModuloAutenticacao;

namespace e_AgendaMedica.WebApi.Config.AutoMapperConfig
{
    public class UsuarioProfille : Profile
    {
        public UsuarioProfille()
        {
            CreateMap<RegistrarUsuarioViewModel, Usuario>()
                .ForMember(destino => destino.UserName, opt => opt.MapFrom(src => src.Login));
        }
    }
}
