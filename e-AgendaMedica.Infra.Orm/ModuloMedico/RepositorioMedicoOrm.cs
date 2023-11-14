using e_Agenda.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace e_AgendaMedica.Infra.Orm.ModuloMedico
{
    public class RepositorioMedicoOrm : RepositorioBase<Medico>, IRepositorioMedico
    {
        public RepositorioMedicoOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {

        }
        public List<Medico> SelecionarMuitos(List<Guid> idsMedicosSelecionados)
        {
            return registros.Where(medicos => idsMedicosSelecionados.Contains(medicos.Id)).ToList();
        }

        public override async Task<Medico> SelecionarPorIdAsync(Guid id)
        {
            return await registros
                .Include(x => x.ListaAtividades)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
