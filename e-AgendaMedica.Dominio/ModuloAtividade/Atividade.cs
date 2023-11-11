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

        public List<Medico> ListaMedicos { get; set; }
    }
    //


    //BUSCAR ultima atividade do medico atual

    //SE TipoAtividade for CIRURGIA Permite somente Um Medico na Lista E SELECIONAR MEDICO

    //SE TipoAtividade for CONSULTA Permite um ou uma lista de medicos E SELECIONAR MEDICO

    //SE TipoAtividade for CONSULTA e for uma lista de medicos TEM QUE SELECIONAR o MEDICO com o HorarioTermino MAIS RECENTE E SELECIONAR MEDICO


    //COM o MEDICO SELECIONADO usar o COMPARAR DESCANSO com as prop HorarioInicio da atividade atual com o HorarioTermino com a ULTIMA ATIVIDADE do MEDICO SELECIONADO

    // COMPARAR DESCANSO

    //Se ATIVIDADE for CIRURGIA,
    //  HorarioInicio deve ter um intervalo de 4 HORAS para HorarioTermino ultima atividade do medico

    //Se ATIVIDADE for CONSULTA
    //  HorarioInicio deve ter um intervalo de 20 MINUTOS para HorarioTermino ultima atividade do medico

}
