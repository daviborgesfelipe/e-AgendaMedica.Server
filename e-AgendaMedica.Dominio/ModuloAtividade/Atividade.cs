using e_AgendaMedica.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.ModuloMedico;
using System.Text.Json.Serialization;

namespace e_AgendaMedica.Dominio.ModuloAtividade
{
    public class Atividade : EntidadeBase<Atividade>
    {
        private List<Medico> listaMedicos;

        public Atividade()
        {
            ListaMedicos = new List<Medico>();
        }

        public string Paciente { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HorarioTermino { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public TipoAtividadeEnum TipoAtividade { get; set; }

        public List<Medico> ListaMedicos { get; set; }

        public bool ConflitoCom(Atividade outraAtividade)
        {
            return this.Data == outraAtividade.Data 
                &&
                (
                    (
                        this.HorarioInicio <= outraAtividade.HorarioTermino 
                        && 
                        this.HorarioTermino >= outraAtividade.HorarioInicio
                    )
                ) 
                && outraAtividade.Id != this.Id;
        }

        public bool RegistrarMedico(Medico medico)
        {
            if (this.ListaMedicos.Any(_medico => _medico.Id == medico.Id))
            {
                return false;
            }
            if ((this.TipoAtividade == TipoAtividadeEnum.Cirurgia))
            {
                if (ListaMedicos.Count >= 1)
                {
                    return false;
                }

                medico.ListaAtividades.Add(this);
                ListaMedicos.Add(medico);

                return true;
            }
            if (this.TipoAtividade == TipoAtividadeEnum.Consulta)
            {
                medico.ListaAtividades.Add(this);
                ListaMedicos.Add(medico);

                return true;
            }


            return false;
        }

        public void RemoverMedico(Medico medico)
        {
            ListaMedicos.Remove(medico);
        }

        public override bool Equals(object obj)
        {
            return obj is Atividade atividade &&
                   Id == atividade.Id;
        }
    }
}




