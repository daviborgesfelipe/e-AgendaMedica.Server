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
        public void nome_do_medico_deve_conter_no_minimo_dois_char()
        {
            //arrange
            medico = new Medico { Nome = "D", Especialidade = "Cardiologista", CRM = "12345-SC" };

            ValidadorMedico validador = new ValidadorMedico();

            //action
            var resultado = validador.Validate(medico);

            //assert
            Assert.AreEqual("O campo nome deve conter no minimo 2 caracteres", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void nome_nulo_deve_falhar()
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
        public void especialidade_do_medico_deve_conter_no_minimo_dois_char()
        {
            //arrange
            medico = new Medico { Nome = "Davi", Especialidade = "Ca", CRM = "12345-SC" };

            ValidadorMedico validador = new ValidadorMedico();

            //action
            var resultado = validador.Validate(medico);

            //assert
            Assert.AreEqual("O campo especialidade deve conter no minimo 3 caracteres", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void crm_nao_deve_ser_nulo()
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
        public void crm_deve_falhar_formato_invalido()
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

        [TestMethod]
        public void nao_deve_dar_erro_registrar_atividade_corretamente_no_medico()
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
        public void nao_deve_registrar_atividade_duplicada()
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
        public void nao_deve_dar_erro_quando_medico_valido()
        {
            // Arrange
            var medico = new Medico { Nome = "Dr. Smith", Especialidade = "Cardiologista", CRM = "12345-SC" };
            var validador = new ValidadorMedico();

            // Act
            var resultado = validador.Validate(medico);

            // Assert
            Assert.IsTrue(resultado.IsValid);
        }

    }
}
