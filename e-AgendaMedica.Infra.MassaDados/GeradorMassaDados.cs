using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloAtividade.Interfaces;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.Dominio.ModuloMedico.Interfaces;

namespace e_AgendaMedica.Infra.MassaDados
{
    public class GeradorMassaDados
    {
        private readonly IServicoMedico _servicoMedico;
        private readonly IServicoAtividade _servicoAtividade;

        public GeradorMassaDados(IServicoMedico servicoMedico, IServicoAtividade servicoAtividade)
        {
            _servicoMedico = servicoMedico;
            _servicoAtividade = servicoAtividade;
        }

        public async Task GerarDadosAsync()
        {
            // Gerar alguns médicos atribuindo o valor das prop via ctor
            var medicos = new List<Medico>
            {
                new Medico("Joel Lio", "Ortopedista", "67890-SC"),
                new Medico("Pen Samento", "Neurologista", "12345-SP"),
                new Medico("Lucas Mendes", "Oftalmologista", "22222-PR"),
                new Medico("Camila Rocha", "Dermatologista", "33333-RS"),
                new Medico("Maria Silva", "Cardiologista", "54321-RJ"),
                new Medico("André Santos", "Pneumologista", "98765-MG"),
            };

            foreach (var medico in medicos)
            {
                await _servicoMedico.InserirAsync(medico);
            }

            // Gerar algumas atividades atribuindo o via inicializador de objeto
            var atividades = new List<Atividade>
            {
                new Atividade
                {
                    Paciente = "Ronaldo",
                    Data = DateTime.Today.AddDays(2),
                    HorarioInicio = TimeSpan.FromHours(14),
                    HorarioTermino = TimeSpan.FromHours(16),
                    TipoAtividade = TipoAtividadeEnum.Consulta,
                    ListaMedicos = medicos.GetRange(0, 1) 
                },
                new Atividade
                {
                    Paciente = "Fernanda",
                    Data = DateTime.Today.AddDays(3),
                    HorarioInicio = TimeSpan.FromHours(10),
                    HorarioTermino = TimeSpan.FromHours(12),
                    TipoAtividade = TipoAtividadeEnum.Cirurgia,
                    ListaMedicos = medicos.GetRange(1, 1) 
                },
                new Atividade
                {
                    Paciente = "Lucas",
                    Data = DateTime.Today.AddDays(4),
                    HorarioInicio = TimeSpan.FromHours(15),
                    HorarioTermino = TimeSpan.FromHours(17),
                    TipoAtividade = TipoAtividadeEnum.Consulta,
                    ListaMedicos = medicos.GetRange(2, 1) 
                },
                new Atividade
                {
                    Paciente = "Gabriela",
                    Data = DateTime.Today.AddDays(5),
                    HorarioInicio = TimeSpan.FromHours(11),
                    HorarioTermino = TimeSpan.FromHours(13),
                    TipoAtividade = TipoAtividadeEnum.Consulta,
                    ListaMedicos = medicos.GetRange(3, 1)
                },
                new Atividade
                {
                    Paciente = "Anderson",
                    Data = DateTime.Today.AddDays(6),
                    HorarioInicio = TimeSpan.FromHours(16),
                    HorarioTermino = TimeSpan.FromHours(18),
                    TipoAtividade = TipoAtividadeEnum.Cirurgia,
                    ListaMedicos = medicos.GetRange(4, 1) 
                },
                new Atividade
                {
                    Paciente = "Mariana",
                    Data = DateTime.Today.AddDays(12),
                    HorarioInicio = TimeSpan.FromHours(13),
                    HorarioTermino = TimeSpan.FromHours(16),
                    TipoAtividade = TipoAtividadeEnum.Consulta,
                    ListaMedicos = medicos.GetRange(0, 1) // 
                },
                    new Atividade
                {
                    Paciente = "Laura",
                    Data = DateTime.Today.AddDays(8),
                    HorarioInicio = TimeSpan.FromHours(16),
                    HorarioTermino = TimeSpan.FromHours(18),
                    TipoAtividade = TipoAtividadeEnum.Consulta,
                    ListaMedicos = medicos.GetRange(1, 1)
                },
                new Atividade
                {
                    Paciente = "Gustavo",
                    Data = DateTime.Today.AddDays(11),
                    HorarioInicio = TimeSpan.FromHours(07),
                    HorarioTermino = TimeSpan.FromHours(08),
                    TipoAtividade = TipoAtividadeEnum.Consulta,
                    ListaMedicos = medicos.GetRange(0, 1)
                },
                new Atividade
                {
                    Paciente = "Leticia",
                    Data = DateTime.Today.AddDays(10),
                    HorarioInicio = TimeSpan.FromHours(10),
                    HorarioTermino = TimeSpan.FromHours(11),
                    TipoAtividade = TipoAtividadeEnum.Consulta,
                    ListaMedicos = medicos.GetRange(0, 1)
                },
                new Atividade
                {
                    Paciente = "Bruna",
                    Data = DateTime.Today.AddDays(15),
                    HorarioInicio = TimeSpan.FromHours(10),
                    HorarioTermino = TimeSpan.FromHours(11),
                    TipoAtividade = TipoAtividadeEnum.Consulta,
                    ListaMedicos = medicos.GetRange(0, 2)
                }
            };

            foreach (var atividade in atividades)
            {
                await _servicoAtividade.InserirAsync(atividade);
            }
        }
    }
}