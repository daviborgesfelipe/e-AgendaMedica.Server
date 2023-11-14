using e_Agenda.Dominio.Compartilhado;
using e_AgendaMedica.Aplicacao.Compartilhado;
using e_AgendaMedica.Dominio.ModuloMedico;
using FluentResults;
using Serilog;

namespace e_AgendaMedica.Aplicacao.ModuloMedico
{
    public class ServicoMedico : ServicoBase<Medico, ValidadorMedico>
    {
        private IRepositorioMedico repositorioMedico;
        private IContextoPersistencia contextoPersistencia;

        public ServicoMedico(IRepositorioMedico repositorioMedico, 
                             IContextoPersistencia contextoPersistencia)
        {
            this.repositorioMedico = repositorioMedico;
            this.contextoPersistencia = contextoPersistencia;
        }

        public async Task<Result<Medico>> InserirAsync(Medico medico)
        {
            Result resultado = Validar(medico);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            await repositorioMedico.InserirAsync(medico);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(medico);
        }

        public async Task<Result<Medico>> Editar(Medico medico)
        {
            var resultadoGet = await repositorioMedico.SelecionarPorIdAsync(medico.Id);

            if (resultadoGet == null)
                return Result.Fail($"Medico com ID {medico.Id} não encontrado.");

            var resultadoValidacao = Validar(medico);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            await EditarAsync(resultadoGet);

            return Result.Ok(medico);
        }

        private async Task<Result<Medico>> EditarAsync(Medico medico)
        {
            repositorioMedico.EditarAsync(medico);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok();
        }
         
        public async Task<Result> Excluir(Guid id)
        {
            var medicoResult = await SelecionarPorIdAsync(id);

            if (medicoResult.IsSuccess)
                return await Excluir(medicoResult.Value);

            return Result.Fail(medicoResult.Errors);
        }

        public async Task<Result> Excluir(Medico medico)
        {
            await repositorioMedico.ExcluirAsync(medico);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok();
        }

        public async Task<Result<Medico>> SelecionarPorIdAsync(Guid id)
        {
            var contato = await repositorioMedico.SelecionarPorIdAsync(id);

            if (contato == null)
            {
                Log.Logger.Warning("Medico {MedicoId} não encontrado", id);

                return Result.Fail("Medico não encontrado");
            }

            return Result.Ok(contato);
        }

        public async Task<Result<List<Medico>>> SelecionarTodosAsync()
        {
            var contatos = await repositorioMedico.SelecionarTodosAsync();

            return Result.Ok(contatos);
        }
    }
}
