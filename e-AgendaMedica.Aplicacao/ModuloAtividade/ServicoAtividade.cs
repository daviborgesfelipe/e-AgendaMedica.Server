using e_Agenda.Dominio.Compartilhado;
using e_AgendaMedica.Aplicacao.Compartilhado;
using e_AgendaMedica.Aplicacao.ModuloMedico;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;
using FluentResults;
using Serilog;

namespace e_AgendaMedica.Aplicacao.ModuloAtividade
{
    public class ServicoAtividade : ServicoBase<Atividade, ValidadorAtividade>, IServicoAtividade
    {
        private IRepositorioAtividade repositorioAtividade;
        private IContextoPersistencia contextoPersistencia;
        private IServicoMedico servicoMedico;

        public ServicoAtividade(IRepositorioAtividade repositorioAtividade,
                                IContextoPersistencia contextoPersistencia,
                                IServicoMedico servicoMedico)
        {
            this.repositorioAtividade = repositorioAtividade;
            this.contextoPersistencia = contextoPersistencia;
            this.servicoMedico = servicoMedico;
        }

        public async Task<Result<Atividade>> InserirAsync(Atividade atividade)
        {
            Result resultado = Validar(atividade);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            if (atividade.TipoAtividade == TipoAtividadeEnum.Cirurgia)
            {
                if (atividade.ListaMedicos.Count > 1)
                    return Result.Fail<Atividade>("Para atividades do tipo cirurgia, a lista de médicos deve conter no máximo um médico.");
            }

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

        public async Task<Result<Atividade>> EditarAsync(Atividade atividade)
        {
            Result resultado = Validar(atividade);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            var atividadeExistente = await SelecionarPorIdAsync(atividade.Id);

            if (atividade.TipoAtividade == TipoAtividadeEnum.Cirurgia)
            {
                if (atividade.ListaMedicos.Any(m => atividadeExistente.Value.ListaMedicos.Any(am => am.Id != m.Id)))
                {
                    if (atividade.ListaMedicos.Count >= 1)
                        return Result.Fail<Atividade>("Para atividades do tipo cirurgia, a lista de médicos deve conter no máximo um médico.");
                }
            }

            await repositorioAtividade.EditarAsync(atividade);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok();
        }

        public async Task<Result<Atividade>> ExcluirAsync(Atividade atividade)
        {
            await repositorioAtividade.ExcluirAsync(atividade);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok();
        }
    }
}

