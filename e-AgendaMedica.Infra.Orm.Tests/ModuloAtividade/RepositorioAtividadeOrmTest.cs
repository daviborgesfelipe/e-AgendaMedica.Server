using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;
using e_AgendaMedica.Infra.Orm.Compartilhado;
using e_AgendaMedica.Infra.Orm.ModuloAtividade;
using Microsoft.EntityFrameworkCore;

namespace e_AgendaMedica.Infra.Orm.Tests.ModuloAtividade
{
    [TestClass]
    public class RepositorioAtividadeOrmTest
    {
        private eAgendaMedicaDbContext db;

        public RepositorioAtividadeOrmTest()
        {
            var builder = new DbContextOptionsBuilder<eAgendaMedicaDbContext>();
            builder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=eAgendaMedica;Integrated Security=True");
            db = new eAgendaMedicaDbContext(builder.Options);

            // Limpa os dados existentes antes de cada teste
            db.Set<Atividade>().RemoveRange(db.Set<Atividade>());
            db.Set<Medico>().RemoveRange(db.Set<Medico>());

            db.SaveChanges();
        }

        [TestMethod]
        public void Deve_inserir_atividade()
        {
            //arrange
            Atividade novaAtividade = new Atividade
            {
                Data = DateTime.Now,
                Paciente = "Will",
                HorarioInicio = new TimeSpan(10, 0, 0),
                HorarioTermino = new TimeSpan(12, 0, 0),
                TipoAtividade = TipoAtividadeEnum.Consulta,
                ListaMedicos = new List<Medico>
                {
                    new Medico { Nome = "Dr. Silva", Especialidade = "Cardiologista", CRM = "12345-SC" }
                }
            };

            var repositorioAtividade = new RepositorioAtividadeOrm(db);

            //action
            repositorioAtividade.InserirAsync(novaAtividade);
            db.SaveChanges();

            //assert
            var atividadeEncontrada = repositorioAtividade.ObterPorIdAsync(novaAtividade.Id);

            Assert.IsNotNull(atividadeEncontrada.Result);
            Assert.AreEqual(novaAtividade.Id, atividadeEncontrada.Result.Id);
            Assert.AreEqual(novaAtividade.TipoAtividade, atividadeEncontrada.Result.TipoAtividade);
        }
        
        [TestMethod]
        public void Deve_editar_atividade()
        {
            // Arrange
            var atividade = new Atividade
            {
                Paciente = "Carlos",
                Data = DateTime.Now,
                TipoAtividade = TipoAtividadeEnum.Consulta
            };

            var repositorioAtividade = new RepositorioAtividadeOrm(db);

            repositorioAtividade.InserirAsync(atividade);
            db.SaveChanges();

            // Act
            atividade.Paciente = "Carlos Silva";
            atividade.TipoAtividade = TipoAtividadeEnum.Consulta;

            repositorioAtividade.EditarAsync(atividade);
            db.SaveChanges();

            // Assert
            var atividadeEditada = repositorioAtividade.ObterPorIdAsync(atividade.Id).Result;

            Assert.IsNotNull(atividadeEditada);
            Assert.AreEqual("Carlos Silva", atividadeEditada.Paciente);
            Assert.AreEqual(TipoAtividadeEnum.Consulta, atividadeEditada.TipoAtividade);
        }

        [TestMethod]
        public void Deve_excluir_atividade()
        {
            // Arrange
            var atividade = new Atividade
            {
                Paciente = "Maria",
                Data = DateTime.Now,
                TipoAtividade = TipoAtividadeEnum.Cirurgia
            };

            var repositorioAtividade = new RepositorioAtividadeOrm(db);

            repositorioAtividade.InserirAsync(atividade);
            db.SaveChanges();

            // Act
            repositorioAtividade.ExcluirAsync(atividade);
            db.SaveChanges();

            // Assert
            var atividadeExcluida = repositorioAtividade.ObterPorIdAsync(atividade.Id).Result;

            Assert.IsNull(atividadeExcluida);
        }

        [TestMethod]
        public void Deve_obter_atividades_no_periodo()
        {
            //arrange
            DateTime dataInicio = DateTime.Now;
            DateTime dataFim = DateTime.Now.AddDays(7);

            var atividade1 = new Atividade { Paciente = "Will", Data = DateTime.Now, TipoAtividade = TipoAtividadeEnum.Consulta };
            var atividade2 = new Atividade { Paciente = "Bruna", Data = DateTime.Now.AddDays(5), TipoAtividade = TipoAtividadeEnum.Cirurgia };
            var atividade3 = new Atividade { Paciente = "Tiago", Data = DateTime.Now.AddDays(10), TipoAtividade = TipoAtividadeEnum.Consulta };

            var repositorioAtividade = new RepositorioAtividadeOrm(db);

            repositorioAtividade.InserirAsync(atividade1);
            repositorioAtividade.InserirAsync(atividade2);
            repositorioAtividade.InserirAsync(atividade3);

            db.SaveChanges();

            //action
            var atividadesNoPeriodo = repositorioAtividade.ObterAtividadesNoPeriodoAsync(dataInicio, dataFim).Result;

            //assert
            Assert.AreEqual(2, atividadesNoPeriodo.Count);
            Assert.IsTrue(atividadesNoPeriodo.Any(a => a.TipoAtividade == TipoAtividadeEnum.Consulta));
            Assert.IsTrue(atividadesNoPeriodo.Any(a => a.TipoAtividade == TipoAtividadeEnum.Cirurgia));
        }

        [TestMethod]
        public void Deve_obter_atividades_do_medico_por_id()
        {
            // Arrange
            var medico = new Medico { Nome = "Dr. José", Especialidade = "Dermatologista", CRM = "11111-SC" };
            var atividade1 = new Atividade { Paciente = "Maria", Data = DateTime.Now, TipoAtividade = TipoAtividadeEnum.Consulta };
            var atividade2 = new Atividade { Paciente = "João", Data = DateTime.Now.AddDays(5), TipoAtividade = TipoAtividadeEnum.Cirurgia };

            atividade1.ListaMedicos.Add(medico);
            atividade2.ListaMedicos.Add(medico);

            var repositorioAtividade = new RepositorioAtividadeOrm(db);

            repositorioAtividade.InserirAsync(atividade1);
            repositorioAtividade.InserirAsync(atividade2);
            db.SaveChanges();

            // Act
            var atividadesDoMedico = repositorioAtividade.ObterAtividadesDoMedicoAsync(medico.Id).Result;

            // Assert
            Assert.AreEqual(2, atividadesDoMedico.Count);
            Assert.IsTrue(atividadesDoMedico.All(a => a.ListaMedicos.Any(m => m.Id == medico.Id)));
        }

        [TestMethod]
        public void Deve_obter_atividades_do_medico_por_lista_de_medicos()
        {
            // Arrange
            var medico1 = new Medico { Nome = "Dr. José", Especialidade = "Dermatologista", CRM = "11111-SC" };
            var medico2 = new Medico { Nome = "Dra. Maria", Especialidade = "Ginecologista", CRM = "22222-SC" };
            var atividade1 = new Atividade { Paciente = "Ana", Data = DateTime.Now, TipoAtividade = TipoAtividadeEnum.Consulta };
            var atividade2 = new Atividade { Paciente = "Carlos", Data = DateTime.Now.AddDays(5), TipoAtividade = TipoAtividadeEnum.Consulta };

            atividade1.ListaMedicos.Add(medico1);
            atividade2.ListaMedicos.Add(medico2);

            var repositorioAtividade = new RepositorioAtividadeOrm(db);

            repositorioAtividade.InserirAsync(atividade1);
            repositorioAtividade.InserirAsync(atividade2);
            db.SaveChanges();

            // Act
            var atividadesDoMedico = repositorioAtividade.ObterAtividadesDoMedicoAsync(new List<Medico> { medico1, medico2 }).Result;

            // Assert
            Assert.AreEqual(2, atividadesDoMedico.Count);
            Assert.IsTrue(atividadesDoMedico.Any(a => a.ListaMedicos.Any(m => m.Id == medico1.Id)));
            Assert.IsTrue(atividadesDoMedico.Any(a => a.ListaMedicos.Any(m => m.Id == medico2.Id)));
        }

        [TestMethod]
        public void Deve_obter_atividades_cirurgicas_com_medico()
        {
            // Arrange
            var medico = new Medico { Nome = "Dr. João", Especialidade = "Cirurgião", CRM = "33333-SC" };
            var atividade1 = new Atividade { Paciente = "Roberto", Data = DateTime.Now, TipoAtividade = TipoAtividadeEnum.Consulta };
            var atividade2 = new Atividade { Paciente = "Julia", Data = DateTime.Now.AddDays(5), TipoAtividade = TipoAtividadeEnum.Cirurgia };

            atividade1.ListaMedicos.Add(medico);
            atividade2.ListaMedicos.Add(medico);

            var repositorioAtividade = new RepositorioAtividadeOrm(db);

            repositorioAtividade.InserirAsync(atividade1);
            repositorioAtividade.InserirAsync(atividade2);
            db.SaveChanges();

            // Act
            var atividadesCirurgicas = repositorioAtividade.ObterAtividadesCirurgicasComMedicoAsync(medico.Id).Result;

            // Assert
            Assert.AreEqual(1, atividadesCirurgicas.Count);
            Assert.AreEqual(TipoAtividadeEnum.Cirurgia, atividadesCirurgicas[0].TipoAtividade);
        }
    }
}
