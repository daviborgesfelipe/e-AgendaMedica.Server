using FluentResults;

namespace e_AgendaMedica.Dominio.Compartilhado
{
    public interface IServico<T> where T : EntidadeBase<T>
    {
        Task<Result<T>> EditarAsync(T registro);

        Task<Result<T>> ExcluirAsync(T registro);

        Task<Result<T>> InserirAsync(T novoRegistro);

        Task<Result<List<T>>> SelecionarTodosAsync();

        Task<Result<T>> SelecionarPorIdAsync(Guid numero);
    }
}
