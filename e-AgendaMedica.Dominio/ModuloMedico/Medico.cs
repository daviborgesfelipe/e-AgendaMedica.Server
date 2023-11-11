using e_AgendaMedica.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.ModuloAtividade;

namespace e_AgendaMedica.Dominio.ModuloMedico
{
    public class Medico : EntidadeBase<Medico>
    {
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public string CRM { get; set; }

        List<Atividade> ListaAtividades { get; set; }

        public Medico(List<Atividade> listaAtividades)
        {
            ListaAtividades = new List<Atividade>();
        }
    }
}
