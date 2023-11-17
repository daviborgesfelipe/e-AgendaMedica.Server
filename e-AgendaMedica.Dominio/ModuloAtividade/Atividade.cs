using e_AgendaMedica.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.ModuloMedico;
using System.Text.Json.Serialization;

namespace e_AgendaMedica.Dominio.ModuloAtividade
{
    public class Atividade : EntidadeBase<Atividade>
    {
        public Atividade()
        {
                
        }
        public string Paciente { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HorarioTermino { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public TipoAtividadeEnum TipoAtividade { get; set; }

        public List<Medico> ListaMedicos { get; set; }

        public bool ConflitoCom(Atividade outraAtividade)
        {
            return this.Data == outraAtividade.Data &&
                   ((this.HorarioInicio <= outraAtividade.HorarioTermino 
                                        &&
                     this.HorarioTermino >= outraAtividade.HorarioInicio));
        }
    }
}




