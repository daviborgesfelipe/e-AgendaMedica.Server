using e_AgendaMedica.Dominio.Compartilhado.Interfaces;
using e_AgendaMedica.Dominio.ModuloMedico;

namespace e_AgendaMedica.Dominio.ModuloAtividade.Interfaces
{
    public interface IRepositorioAtividade : IRepositorio<Atividade>
    {
        Task<List<Atividade>> ObterAtividadesCirurgicasComMedicoAsync(Guid Id);

        Task<List<Atividade>> ObterAtividadesNoPeriodoAsync(DateTime dataInicio, DateTime dataFim);

        Task<List<Atividade>> ObterAtividadesDoMedicoAsync(List<Medico> medicos);
    }
}

