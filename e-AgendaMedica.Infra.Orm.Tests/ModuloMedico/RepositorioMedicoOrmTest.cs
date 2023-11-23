using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.Infra.Orm.Compartilhado;
using e_AgendaMedica.Infra.Orm.ModuloMedico;
using Microsoft.EntityFrameworkCore;

namespace e_AgendaMedica.Infra.Orm.Tests.ModuloMedico
{
    [TestClass]
    public class RepositorioMedicoOrmTest
    {
        private eAgendaMedicaDbContext db;

        public RepositorioMedicoOrmTest()
        {
            var builder = new DbContextOptionsBuilder<eAgendaMedicaDbContext>();

            builder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=eAgendaMedica;Integrated Security=True");

            db = new eAgendaMedicaDbContext(builder.Options);

            db.Set<Medico>().RemoveRange(db.Set<Medico>());
            db.Set<Atividade>().RemoveRange(db.Set<Atividade>());

            db.SaveChanges();
        }

        [TestMethod]
        public void Deve_inserir_medico()
        {
            //arrange
            Medico novaMedico = new Medico();
            novaMedico.Nome = "Dra. Julia";
            novaMedico.Especialidade = "Cardiologista";
            novaMedico.CRM = "42342-SC";

            var repositorio = new RepositorioMedicoOrm(db);

            //action
            repositorio.InserirAsync(novaMedico);

            db.SaveChanges();

            //assert
            var medicoEncontrado = repositorio.ObterPorIdAsync(novaMedico.Id);

            Assert.IsNotNull(medicoEncontrado.Result);

            Assert.AreEqual(novaMedico.Id, medicoEncontrado.Result.Id);
            Assert.AreEqual(novaMedico.Nome, medicoEncontrado.Result.Nome);
            Assert.AreEqual(novaMedico.Especialidade, medicoEncontrado.Result.Especialidade);
            Assert.AreEqual(novaMedico.CRM, medicoEncontrado.Result.CRM);

            Assert.AreEqual(0, novaMedico.ListaAtividades.Count);
        }

        [TestMethod]
        public void Deve_editar_medico()
        {
            // Arrange
            Medico novoMedico = new Medico
            {
                Nome = "Dr. João",
                Especialidade = "Pediatra",
                CRM = "12345-SC"
            };

            var repositorio = new RepositorioMedicoOrm(db);
            repositorio.InserirAsync(novoMedico);
            db.SaveChanges();

            // Act
            novoMedico.Especialidade = "Neurologista";
            repositorio.EditarAsync(novoMedico);
            db.SaveChanges();

            // Assert
            var medicoEditado = repositorio.ObterPorIdAsync(novoMedico.Id).Result;

            Assert.IsNotNull(medicoEditado);
            Assert.AreEqual("Neurologista", medicoEditado.Especialidade);
        }

        [TestMethod]
        public void Deve_excluir_medico()
        {
            // Arrange
            Medico novoMedico = new Medico
            {
                Nome = "Dr. Carlos",
                Especialidade = "Oftalmologista",
                CRM = "54321-SC"
            };

            var repositorio = new RepositorioMedicoOrm(db);
            repositorio.InserirAsync(novoMedico);
            db.SaveChanges();

            // Act
            repositorio.ExcluirAsync(novoMedico);
            db.SaveChanges();

            // Assert
            var medicoExcluido = repositorio.ObterPorIdAsync(novoMedico.Id).Result;

            Assert.IsNull(medicoExcluido);
        }

        [TestMethod]
        public void Deve_obter_lista_de_medicos_por_ids()
        {
            // Arrange
            var medico1 = new Medico { Nome = "Dr. José", Especialidade = "Dermatologista", CRM = "11111-SC" };
            var medico2 = new Medico { Nome = "Dra. Maria", Especialidade = "Ginecologista", CRM = "22222-SC" };
            var medico3 = new Medico { Nome = "Dr. Roberto", Especialidade = "Ortopedista", CRM = "33333-SC" };

            var repositorio = new RepositorioMedicoOrm(db);
            repositorio.InserirAsync(medico1);
            repositorio.InserirAsync(medico2);
            repositorio.InserirAsync(medico3);
            db.SaveChanges();

            // Act
            var idsMedicosSelecionados = new List<Guid> { medico1.Id, medico3.Id };
            var medicosSelecionados = repositorio.ObterMuitos(idsMedicosSelecionados).Result;

            // Assert
            Assert.AreEqual(2, medicosSelecionados.Count);
            Assert.IsTrue(medicosSelecionados.Any(m => m.Id == medico1.Id));
            Assert.IsTrue(medicosSelecionados.Any(m => m.Id == medico3.Id));
        }
    }
}