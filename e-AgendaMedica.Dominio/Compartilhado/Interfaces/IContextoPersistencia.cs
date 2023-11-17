namespace e_AgendaMedica.Dominio.Compartilhado.Interfaces
{
    public interface IContextoPersistencia
    {
        Task<bool> GravarDadosAsync();
    }
}
