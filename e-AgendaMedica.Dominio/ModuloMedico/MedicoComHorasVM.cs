namespace e_AgendaMedica.Dominio.ModuloAtividade
{
    public class MedicoComHorasVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public string CRM { get; set; }
        public TimeSpan TotalHorasTrabalhadas { get; set; }
    }
}
