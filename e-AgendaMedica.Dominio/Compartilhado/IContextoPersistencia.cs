namespace e_Agenda.Dominio.Compartilhado
{
    public interface IContextoPersistencia
    {
        Task<bool> GravarDadosAsync();
    }
}
