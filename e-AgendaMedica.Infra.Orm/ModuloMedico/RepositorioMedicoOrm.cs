using e_AgendaMedica.Dominio.Compartilhado.Interfaces;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.Dominio.ModuloMedico.Interfaces;
using e_AgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace e_AgendaMedica.Infra.Orm.ModuloMedico
{
    public class RepositorioMedicoOrm : RepositorioBase<Medico>, IRepositorioMedico
    {
        public RepositorioMedicoOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {

        }
        public async Task<List<Medico>> ObterMuitos(List<Guid> idsMedicosSelecionados)
        {
            return await registros.Where(medicos => idsMedicosSelecionados.Contains(medicos.Id)).ToListAsync();
        }

        public override async Task<Medico> ObterPorIdAsync(Guid id)
        {
            return await registros
                .Include(x => x.ListaAtividades)
                .SingleOrDefaultAsync(x => x.Id == id);
        }


    }
}
