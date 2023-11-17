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
            // Gerar alguns médicos
            var medicos = new List<Medico>
            {
                new Medico("Dr. Joel Lio", "Ortopedista", "67890-SC"),
                new Medico("Dr. Pen Samento", "Neurologista", "12345-SP"),
                new Medico("Dr. Lucas Mendes", "Oftalmologista", "22222-PR"),
                new Medico("Dra. Camila Rocha", "Dermatologista", "33333-RS"),
                new Medico("Dra. Maria Silva", "Cardiologista", "54321-RJ"),
                new Medico("Dr. André Santos", "Pneumologista", "98765-MG"),
            };

            foreach (var medico in medicos)
            {
                await _servicoMedico.InserirAsync(medico);
            }

            // Gerar algumas atividades
            var atividades = new List<Atividade>
            {
                new Atividade
                {
                    Paciente = "Jordan",
                    Data = DateTime.Now.AddDays(1),
                    HorarioInicio = TimeSpan.FromHours(9),
                    HorarioTermino = TimeSpan.FromHours(11),
                    TipoAtividade = TipoAtividadeEnum.Cirurgia,
                    ListaMedicos = medicos
                },
                new Atividade
                {
                    Paciente = "Ronaldo",
                    Data = DateTime.Now.AddDays(2),
                    HorarioInicio = TimeSpan.FromHours(14),
                    HorarioTermino = TimeSpan.FromHours(16),
                    TipoAtividade = TipoAtividadeEnum.Consulta,
                    ListaMedicos = medicos.GetRange(0, 1) 
                },
                new Atividade
                {
                    Paciente = "Fernanda",
                    Data = DateTime.Now.AddDays(3),
                    HorarioInicio = TimeSpan.FromHours(10),
                    HorarioTermino = TimeSpan.FromHours(12),
                    TipoAtividade = TipoAtividadeEnum.Consulta,
                    ListaMedicos = medicos.GetRange(1, 1) 
                },
                new Atividade
                {
                    Paciente = "Lucas",
                    Data = DateTime.Now.AddDays(4),
                    HorarioInicio = TimeSpan.FromHours(15),
                    HorarioTermino = TimeSpan.FromHours(17),
                    TipoAtividade = TipoAtividadeEnum.Consulta,
                    ListaMedicos = medicos.GetRange(2, 1) 
                },
                new Atividade
                {
                    Paciente = "Gabriela",
                    Data = DateTime.Now.AddDays(5),
                    HorarioInicio = TimeSpan.FromHours(11),
                    HorarioTermino = TimeSpan.FromHours(13),
                    TipoAtividade = TipoAtividadeEnum.Consulta,
                    ListaMedicos = medicos.GetRange(3, 1)
                },
                new Atividade
                {
                    Paciente = "Anderson",
                    Data = DateTime.Now.AddDays(6),
                    HorarioInicio = TimeSpan.FromHours(16),
                    HorarioTermino = TimeSpan.FromHours(18),
                    TipoAtividade = TipoAtividadeEnum.Cirurgia,
                    ListaMedicos = medicos.GetRange(4, 1) 
                },
                new Atividade
                {
                    Paciente = "Mariana",
                    Data = DateTime.Now.AddDays(7),
                    HorarioInicio = TimeSpan.FromHours(13),
                    HorarioTermino = TimeSpan.FromHours(15),
                    TipoAtividade = TipoAtividadeEnum.Consulta,
                    ListaMedicos = medicos.GetRange(0, 1) // 
                }
            };

            foreach (var atividade in atividades)
            {
                await _servicoAtividade.InserirAsync(atividade);
            }
        }
    }
}