using e_AgendaMedica.Dominio.Compartilhado.Interfaces;
using e_AgendaMedica.Dominio.ModuloMedico;
using FluentResults;

namespace e_AgendaMedica.Dominio.ModuloAtividade.Interfaces
{
    public interface IServicoAtividade : IServico<Atividade>
    {
        Task<Result<List<MedicoComHorasVM>>> ObterMedicosMaisHorasTrabalhadas(DateTime dataInicio, DateTime dataFim);
    }
}
