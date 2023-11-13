using e_AgendaMedica.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.ModuloAtividade;
using System.Text.RegularExpressions;

namespace e_AgendaMedica.Dominio.ModuloMedico
{
    public class Medico : EntidadeBase<Medico>
    {
        private string _crm;

        public Medico()
        {

        }
        public Medico(string nome, string especialidade, string crm)
        {
            Nome = nome;
            Especialidade = especialidade;
            CRM = crm;
        }

        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public string CRM
        {
            get { return _crm; }
            set
            {
                if (ValidarCRM(value))
                    _crm = value;
                else
                    throw new ArgumentException("Formato do CRM inválido. O CRM deve ter 5 dígitos seguidos da sigla do estado (Exemplo: 12345-SP)");
            }
        }

        List<Atividade> ListaAtividades { get; set; }

        public bool ValidarCRM(string crm)
        {
            string[] siglasEstados = {
                "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
                "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ",
                "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };

            string[] partesCRM = crm.Split('-');

            if (partesCRM.Length == 2)
            {
                string siglaEstado = partesCRM[1].ToUpper();

                bool siglaValida = siglasEstados.Contains(siglaEstado);
                bool formatoValido = Regex.IsMatch(crm, @"\d{5}-[A-Z]{2}");

                return siglaValida && formatoValido;
            }

            return false;
        }
    }
}
