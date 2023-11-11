using e_AgendaMedica.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.ModuloMedico;

namespace e_AgendaMedica.Dominio.ModuloAtividade
{
    public class Atividade : EntidadeBase<Atividade>
    {
        public string Paciente { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HorarioTermino { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public TipoAtividadeEnum TipoAtividade { get; set; }

        public Medico MedicoResponsavel { get; set; }
        public List<Medico> ListaMedicos { get; set; }
    }
}
