using e_AgendaMedica.Dominio.ModuloAtividade;
using System.Globalization;

namespace e_AgendaMedica.Dominio.Tests.ModuloAtividade
{
    [TestClass]
    public class ValidadorAtividadeTest
    {
        private Atividade atividade;

        public ValidadorAtividadeTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
            atividade = new Atividade();
        }

        [TestMethod]
        public void nome_do_paciente_da_atividade_deve_ser_obrigatorio()
        {
            //arrange
            atividade.Paciente = null;

            ValidadorAtividade validador = new ValidadorAtividade();

            //action
            var resultado = validador.Validate(atividade);

            //assert
            Assert.AreEqual("O campo paciente é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void data_da_atividade_deve_ser_obrigatorio()
        {
            //arrange
            atividade.Paciente = "Teste";
            atividade.Data = DateTime.MinValue;

            ValidadorAtividade validador = new ValidadorAtividade();

            //action
            var resultado = validador.Validate(atividade);

            //assert
            Assert.AreEqual("O campo data é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void horario_termino_deve_ser_maior_ou_igual_ao_horario_inicio()
        {
            // Arrange
            atividade.Paciente = "Teste";
            atividade.Data = DateTime.Today;
            atividade.HorarioInicio = new TimeSpan(10, 0, 0); 
            atividade.HorarioTermino = new TimeSpan(9, 0, 0);

            var validador = new ValidadorAtividade();

            // Act
            var resultado = validador.Validate(atividade);

            // Assert
            Assert.AreEqual("O campo horario de termino não pode ser maior que o horario de inicio é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void horario_termino_deve_ser_obrigatorio()
        {
            // Arrange
            atividade.Paciente = "Teste";
            atividade.Data = DateTime.Today;

            atividade.HorarioInicio =  TimeSpan.MinValue; 
            atividade.HorarioTermino = TimeSpan.MinValue; 

            var validador = new ValidadorAtividade();

            // Act
            var resultado = validador.Validate(atividade);

            // Assert
            Assert.AreEqual("O campo horario termino é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void horario_inicio_deve_ser_obrigatorio()
        {
            // Arrange
            atividade.Paciente = "Teste";
            atividade.Data = DateTime.Today;

            atividade.HorarioTermino = TimeSpan.MinValue;
            atividade.HorarioInicio = TimeSpan.MinValue; 

            var validador = new ValidadorAtividade();

            // Act
            var resultado = validador.Validate(atividade);

            // Assert
            Assert.AreEqual("O campo horario termino é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void lista_de_medicos_deve_ser_nao_nula_e_nao_vazia()
        {
            // Arrange
            atividade.Paciente = "Teste";
            atividade.Data = DateTime.Today;
            atividade.HorarioInicio = new TimeSpan(10, 0, 0); 
            atividade.HorarioTermino = new TimeSpan(11, 0, 0); 
            atividade.ListaMedicos = null; // Lista de médicos nula

            var validador = new ValidadorAtividade();

            // Act
            var resultado = validador.Validate(atividade);

            // Assert
            Assert.AreEqual("O campo lista de pacientes é obrigatório", resultado.Errors[0].ErrorMessage);
        }
    }
}
