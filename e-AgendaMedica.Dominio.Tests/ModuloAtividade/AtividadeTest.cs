using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;

namespace e_AgendaMedica.Dominio.Tests.ModuloAtividade
{
    [TestClass]
    public class AtividadeTest
    {

        [TestMethod]
        public void Deve_adicionar_somente_um_medico_na_lista_de_atividade_do_tipo_cirurgia()
        {
            //arrange
            var ativ = new Atividade();
            ativ.TipoAtividade = TipoAtividadeEnum.Cirurgia;
            ativ.ListaMedicos = new List<Medico>();

            var medico01 = new Medico();
            var medico02 = new Medico();
            var medico03 = new Medico();
            var medico04 = new Medico();

            //action
            ativ.RegistrarMedico(medico01);
            ativ.RegistrarMedico(medico02);
            ativ.RegistrarMedico(medico03);
            ativ.RegistrarMedico(medico04);

            //assert
            Assert.AreEqual(1, ativ.ListaMedicos.Count);
        }

        [TestMethod]
        public void Deve_adicionar_varios_medicos_na_lista_de_atividade_do_tipo_consulta()
        {
            //arrange
            var ativ = new Atividade();
            ativ.TipoAtividade = TipoAtividadeEnum.Consulta;
            ativ.ListaMedicos = new List<Medico>();

            var medico01 = new Medico();
            var medico02 = new Medico();
            var medico03 = new Medico();
            var medico04 = new Medico();

            //action
            ativ.RegistrarMedico(medico01);
            ativ.RegistrarMedico(medico02);
            ativ.RegistrarMedico(medico03);
            ativ.RegistrarMedico(medico04);

            //assert
            Assert.AreEqual(4, ativ.ListaMedicos.Count);
        }

        [TestMethod]
        public void Deve_dar_conflito_entre_duas_atividades_com_data_e_horarios_iguais()
        {
            //arrange
            var atividade = new Atividade();

            var outraAtividade = new Atividade();
            //action
            var resultado = atividade.ConflitoCom(outraAtividade);

            //assert
            Assert.AreEqual(true, resultado);
        }

        [TestMethod]
        public void Deve_dar_conflito_entre_duas_atividades_com_data_e_horario_convergente()
        {
            //arrange
            var atividade = new Atividade();
            atividade.Data = new DateTime(2023, 11, 23);
            atividade.HorarioInicio = new TimeSpan(10, 10, 0);
            atividade.HorarioTermino = new TimeSpan(11, 10, 0);

            var outraAtividade = new Atividade();
            outraAtividade.Data = new DateTime(2023, 11, 23);
            outraAtividade.HorarioInicio = new TimeSpan(10, 50, 0);
            outraAtividade.HorarioTermino = new TimeSpan(11, 50, 0);

            //action
            var resultado = atividade.ConflitoCom(outraAtividade);

            //assert
            Assert.AreEqual(true, resultado);
        }

        [TestMethod]
        public void Nao_deve_dar_conflito_entre_duas_atividades_com_data_diferentes()
        {
            //arrange
            var atividade = new Atividade();
            atividade.Data = new DateTime(2023, 11, 23);

            var outraAtividade = new Atividade();
            outraAtividade.Data = new DateTime(2023, 11, 22);

            //action
            var resultado = atividade.ConflitoCom(outraAtividade);

            //assert
            Assert.AreEqual(false, resultado);
        }

        [TestMethod]
        public void Nao_deve_dar_conflito_entre_duas_atividades_com_data_convergentes_e_horario_divergentes()
        {
            //arrange
            var atividade = new Atividade();
            atividade.Data = new DateTime(2023, 11, 23);
            atividade.HorarioInicio = new TimeSpan(10, 10, 0);
            atividade.HorarioTermino = new TimeSpan(11, 10, 0);

            var outraAtividade = new Atividade();
            outraAtividade.Data = new DateTime(2023, 11, 23);
            outraAtividade.HorarioInicio = new TimeSpan(08, 10, 0);
            outraAtividade.HorarioTermino = new TimeSpan(09, 10, 0);

            //action
            var resultado = atividade.ConflitoCom(outraAtividade);

            //assert
            Assert.AreEqual(false, resultado);
        }

        [TestMethod]
        public void Nao_deve_adicionar_medicos_duplicados_na_atividade()
        {
            //arrange
            var ativ = new Atividade();

            var idDuplicado = Guid.NewGuid();

            var medico01 = new Medico();
            medico01.Id = idDuplicado;

            var medico02 = new Medico();
            medico02.Id = idDuplicado;

            var medico03 = new Medico();
            medico03.Id = idDuplicado;

            var medico04 = new Medico();

            //action
            ativ.RegistrarMedico(medico01);
            ativ.RegistrarMedico(medico02);
            ativ.RegistrarMedico(medico03);
            ativ.RegistrarMedico(medico04);

            //assert
            Assert.AreEqual(2, ativ.ListaMedicos.Count);
        }

    }
}