using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;

namespace e_AgendaMedica.Dominio.Tests.ModuloMedico
{
    [TestClass]
    public class MedicoTest
    {
        private Medico medico;
        private Atividade atividade;

        public MedicoTest()
        {
            medico = new Medico();
            atividade = new Atividade();
            atividade.Data = DateTime.Now;
            atividade.HorarioTermino = TimeSpan.Zero;
            atividade.HorarioInicio = TimeSpan.Zero;
            atividade.Paciente = "Will";
        }

        [TestMethod]
        public void medico_deve_registrar_atividade_na_lista_de_atividades()
        {
            // Arrange
            var atividade = new Atividade();
            var medico = new Medico();

            // Act
            medico.RegistrarAtividade(atividade);

            // Assert
            Assert.IsTrue(medico.ListaAtividades.Contains(atividade));
        }

        [TestMethod]
        public void lista_atividades_deve_inicializar_propriedades_corretamente_via_ctor()
        {
            // Arrange
            var nome = "Dr. Smith";
            var especialidade = "Cardiologista";
            var crm = "12345";

            // Act
            var medico = new Medico(nome, especialidade, crm);

            // Assert
            Assert.AreEqual(nome, medico.Nome);
            Assert.AreEqual(especialidade, medico.Especialidade);
            Assert.AreEqual(crm, medico.CRM);
            //Assert.IsNotNull(medico.ListaAtividades);
        }
        
        [TestMethod]
        public void lista_atividades_deve_inicializar_propriedades_corretamente_via_obj()
        {
            // Arrange
            var nome = "Dr. Smith";
            var especialidade = "Cardiologista";
            var crm = "12345";

            medico.Nome = nome;
            medico.Especialidade = especialidade;
            medico.CRM = crm;
            atividade.RegistrarMedico(medico);
            medico.RegistrarAtividade(atividade);

            // Assert
            Assert.AreEqual(nome, medico.Nome);
            Assert.AreEqual(especialidade, medico.Especialidade);
            Assert.AreEqual(crm, medico.CRM);
            Assert.IsNotNull(medico.ListaAtividades);
        }
        
        [TestMethod]
        public void deve_verificar_igualdade_entre_medicos()
        {
            // Arrange
            var id = Guid.NewGuid();
            var medico1 = new Medico { Id = id };
            var medico2 = new Medico { Id = id };
            var medico3 = new Medico { Id = Guid.NewGuid() };

            // Assert
            Assert.IsTrue(medico1.Equals(medico2));
            Assert.IsFalse(medico1.Equals(medico3));
        }
    }
}
