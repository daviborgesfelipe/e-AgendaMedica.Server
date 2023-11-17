using e_AgendaMedica.Dominio.Compartilhado.Interfaces;

namespace e_AgendaMedica.Dominio.ModuloMedico.Interfaces
{
    public interface IRepositorioMedico : IRepositorio<Medico>
    {
        Task<List<Medico>> ObterMuitos(List<Guid> idsMedicosSelecionados);
    }
}
