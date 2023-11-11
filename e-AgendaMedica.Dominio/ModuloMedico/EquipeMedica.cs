using e_AgendaMedica.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.ModuloAtividade;

namespace e_AgendaMedica.Dominio.ModuloMedico
{
    public class EquipeMedica : EntidadeBase<EquipeMedica>
    {
        public string Time { get; set; }

        public List<Medico> Medicos;

        public EquipeMedica()
        {
            Medicos = new List<Medico>();
        }
    }
}
