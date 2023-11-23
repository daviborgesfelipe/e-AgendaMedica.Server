using e_AgendaMedica.Dominio.Compartilhado.Interfaces;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloAtividade.Interfaces;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace e_AgendaMedica.Infra.Orm.ModuloAtividade
{
    public class RepositorioAtividadeOrm : RepositorioBase<Atividade>, IRepositorioAtividade
    {
        public RepositorioAtividadeOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }
        public override async Task<Atividade> ObterPorIdAsync(Guid id)
        {
            return await registros
                .Include(x => x.ListaMedicos)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Atividade>> ObterAtividadesNoPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            return registros
                .Where(atividade => 
                        atividade.Data >= dataInicio 
                        && 
                        atividade.Data <= dataFim)
                .ToList();
        }

        #region Conferir Conflito 

        public async Task<List<Atividade>> ObterAtividadesDoMedicoAsync(Guid Id)
        {
            return registros
                .Where(x => x.ListaMedicos.Any(x => x.Id == Id))
                .ToList();
        }

        public async Task<List<Atividade>> ObterAtividadesDoMedicoAsync(List<Medico> medicos)
        {
            var idsMedicos = medicos.Select(medico => medico.Id).ToList();

            return await registros
                .Where(atividade => 
                        atividade.ListaMedicos.Any(medico => 
                            idsMedicos.Contains(medico.Id)))
                .ToListAsync();
        }

        public async Task<List<Atividade>> ObterAtividadesCirurgicasComMedicoAsync(Guid medicoId)
        {
            return await registros
                .Where(atividade => 
                       atividade.ListaMedicos.Any(medico =>
                                medico.Id == medicoId)  
                                && 
                                atividade.TipoAtividade == TipoAtividadeEnum.Cirurgia)
                .ToListAsync();
        }
        #endregion

    }
}
