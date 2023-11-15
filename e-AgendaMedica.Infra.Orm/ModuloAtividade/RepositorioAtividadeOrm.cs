using e_Agenda.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.ModuloAtividade;
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
        public override async Task<Atividade> SelecionarPorIdAsync(Guid id)
        {
            return await registros
                .Include(x => x.ListaMedicos)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Atividade>> SelecionarAtividadesCirurgicasComMedicoAsync(Guid medicoId)
        {
            return await registros
                .Where(atividade => atividade.ListaMedicos.Any(medico => medico.Id == medicoId) && atividade.TipoAtividade == TipoAtividadeEnum.Cirurgia)
                .ToListAsync();
        }
    }
}
