using e_AgendaMedica.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.ModuloAtividade;
using System.Text.Json.Serialization;

namespace e_AgendaMedica.Dominio.ModuloMedico
{
    public class Medico : EntidadeBase<Medico>
    {
        public Medico()
        {
            ListaAtividades = new List<Atividade>();
        }
        public Medico( string nome, string especialidade, string crm )
        {
            Nome = nome;
            Especialidade = especialidade;
            CRM = crm;
        }
        
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public string CRM { get; set; }

        [JsonIgnore]
        public List<Atividade> ListaAtividades { get; set; }

        public bool RegistrarAtividade(Atividade atividade)
        {
            if (this.ListaAtividades.Any(_ativ => _ativ.Id == atividade.Id))
            {
                return false;
            }

            if (ListaAtividades.Contains(atividade) == false)
            {
                atividade.RegistrarMedico(this);

                return true;
            }

            return false;
        }
        public override bool Equals(object obj)
        {
            return obj is Medico medico &&
                   Id == medico.Id;
        }
    }
}
