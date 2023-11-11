using System.ComponentModel;

namespace e_AgendaMedica.Dominio.ModuloAtividade
{
    public enum TipoAtividadeEnum
    {
        [Description("Consulta")]
        Consulta = 0,

        [Description("Cirurgia")]
        Cirurgia = 1
    }
}