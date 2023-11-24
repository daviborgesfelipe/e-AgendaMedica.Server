using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;

namespace e_AgendaMedica.Dominio.Tests.ModuloMedico
{
    [TestClass]
    public class ValidadorMedicoTest
    {
        private Medico medico;
        private Atividade atividade;

        public ValidadorMedicoTest()
        {
            medico = new Medico();
            atividade = new Atividade();
            atividade.Data = DateTime.Now;
            atividade.HorarioTermino = TimeSpan.Zero;
            atividade.HorarioInicio = TimeSpan.Zero;
            atividade.Paciente = "Will";
        }

        [TestMethod]
        public void Deve_Passar_Quando_Medico_Valido()
        {
            // Arrange
            var medico = new Medico { Nome = "Dr. Smith", Especialidade = "Cardiologista", CRM = "12345-SC" };
            var validador = new ValidadorMedico();

            // Act
            var resultado = validador.Validate(medico);

            // Assert
            Assert.IsTrue(resultado.IsValid);
        }

        [TestMethod]
        public void Deve_Falhar_Quando_Nome_Nulo()
        {
            // Arrange
            var medico = new Medico { Nome = null, Especialidade = "Cardiologista", CRM = "12345-SC" };
            var validador = new ValidadorMedico();

            // Act
            var resultado = validador.Validate(medico);

            // Assert
            Assert.IsFalse(resultado.IsValid);
            Assert.IsTrue(resultado.Errors.Any(x => x.PropertyName == nameof(Medico.Nome)));
        }

        [TestMethod]
        public void Deve_Falhar_Quando_CRM_Nulo()
        {
            // Arrange
            var medico = new Medico { Nome = "Dr. Smith", Especialidade = "Cardiologista", CRM = null };
            var validador = new ValidadorMedico();

            // Act
            var resultado = validador.Validate(medico);

            // Assert
            Assert.IsFalse(resultado.IsValid);
            Assert.IsTrue(resultado.Errors.Any(x => x.PropertyName == nameof(Medico.CRM)));
        }

        [TestMethod]
        public void Deve_Registrar_Atividade_Corretamente()
        {
            // Arrange
            var medico = new Medico { Nome = "Dr. Smith", Especialidade = "Cardiologista", CRM = "12345-SC" };
            var atividade = new Atividade();

            // Act
            medico.RegistrarAtividade(atividade);

            // Assert
            Assert.IsTrue(medico.ListaAtividades.Contains(atividade));
        }

        [TestMethod]
        public void Nao_Deve_Registrar_Atividade_Duplicada()
        {
            // Arrange
            var medico = new Medico { Nome = "Dr. Smith", Especialidade = "Cardiologista", CRM = "12345-SC" };
            var idDuplicado = Guid.NewGuid();
            atividade.Id = idDuplicado;
            // Act
            medico.RegistrarAtividade(atividade);
            medico.RegistrarAtividade(atividade);

            // Assert
            Assert.AreEqual(1, medico.ListaAtividades.Count);
        }

        [TestMethod]
        public void Deve_Falhar_Quando_CRM_Invalido()
        {
            // Arrange
            var medico = new Medico { Nome = "Dr. Smith", Especialidade = "Cardiologista", CRM = "123456-XX" };
            var validador = new ValidadorMedico();

            // Act
            var resultado = validador.Validate(medico);

            // Assert
            Assert.IsFalse(resultado.IsValid);
            Assert.IsTrue(resultado.Errors.Any(x => x.PropertyName == nameof(Medico.CRM)));
        }
    }
}
