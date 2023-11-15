using e_AgendaMedica.Dominio.Compartilhado;

namespace e_AgendaMedica.Dominio.ModuloAtividade
{
    public interface IRepositorioAtividade : IRepositorio<Atividade>
    {
       Task<List<Atividade>> SelecionarAtividadesCirurgicasComMedicoAsync(Guid Id);
    }
}
