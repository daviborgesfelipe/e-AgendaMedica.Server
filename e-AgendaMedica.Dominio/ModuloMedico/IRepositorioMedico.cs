using e_AgendaMedica.Dominio.Compartilhado;

namespace e_AgendaMedica.Dominio.ModuloMedico
{
    public interface IRepositorioMedico : IRepositorio<Medico>
    {
        List<Medico> SelecionarMuitos(List<Guid> idsMedicosSelecionados);
    }
}
