using e_AgendaMedica.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.ModuloAtividade;
using System.Text.Json.Serialization;

namespace e_AgendaMedica.Dominio.ModuloMedico
{
    public class Medico : EntidadeBase<Medico>
    {
        public Medico()
        {
            
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
    }
}
