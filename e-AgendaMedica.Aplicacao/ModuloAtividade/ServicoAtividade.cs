using e_Agenda.Dominio.Compartilhado;
using e_AgendaMedica.Aplicacao.Compartilhado;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;
using FluentResults;
using Serilog;

namespace e_AgendaMedica.Aplicacao.ModuloAtividade
{
    public class ServicoAtividade : ServicoBase<Atividade, ValidadorAtividade>
    {
        private IRepositorioAtividade repositorioAtividade;
        private IContextoPersistencia contextoPersistencia;
        public ServicoAtividade(IRepositorioAtividade repositorioAtividade,
                                IContextoPersistencia contextoPersistencia)
        {
            this.repositorioAtividade = repositorioAtividade;
            this.contextoPersistencia = contextoPersistencia;
        }

        public async Task<Result<Atividade>> InserirAsync(Atividade atividade)
        {
            Result resultado = Validar(atividade);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            await repositorioAtividade.InserirAsync(atividade);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(atividade);
        }
        public async Task<Result<Atividade>> SelecionarPorIdAsync(Guid id)
        {
            var atividade = await repositorioAtividade.SelecionarPorIdAsync(id);

            if (atividade == null)
            {
                Log.Logger.Warning("Atividade {AtividadeId} não encontrada", id);

                return Result.Fail("Atividade não encontrada");
            }

            return Result.Ok(atividade);
        }

        public async Task<Result<List<Atividade>>> SelecionarTodosAsync()
        {
            var atividade = await repositorioAtividade.SelecionarTodosAsync();

            return Result.Ok(atividade);
        }
    }
}

