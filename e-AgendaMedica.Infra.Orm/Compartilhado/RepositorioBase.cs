using e_AgendaMedica.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.Compartilhado.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_AgendaMedica.Infra.Orm.Compartilhado
{
    public abstract class RepositorioBase<TEntity> where TEntity : EntidadeBase<TEntity>
    {
        protected DbSet<TEntity> registros;
        private readonly eAgendaMedicaDbContext dbContext;

        public RepositorioBase(IContextoPersistencia contextoPersistencia)
        {
            dbContext = (eAgendaMedicaDbContext)contextoPersistencia;
            registros = dbContext.Set<TEntity>();
        }
        public virtual async Task<bool> InserirAsync(TEntity novoRegistro)
        {
            await registros.AddAsync(novoRegistro);
            return true;
        }

        public virtual async Task<bool> EditarAsync(TEntity registro)
        {
            registros.Update(registro);
            return true;
        }

        public virtual async Task<bool> ExcluirAsync(TEntity registro)
        {
            registros.Remove(registro);
            return true;
        }
        
        public virtual async Task<TEntity> ObterPorIdAsync(Guid id)
        {
            return await registros
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<List<TEntity>> ObterTodosAsync()
        {
            return await registros.ToListAsync();
        }
    }
}
