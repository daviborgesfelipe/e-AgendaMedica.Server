namespace e_AgendaMedica.Dominio.Compartilhado.Interfaces
{
    public interface IRepositorio<T> where T : EntidadeBase<T>
    {
        Task<bool> EditarAsync(T registro);

        Task<bool> ExcluirAsync(T registro);

        Task<bool> InserirAsync(T novoRegistro);

        Task<List<T>> ObterTodosAsync();

        Task<T> ObterPorIdAsync(Guid numero);
    }
}
