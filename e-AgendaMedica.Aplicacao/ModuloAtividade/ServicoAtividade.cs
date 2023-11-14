using e_Agenda.Dominio.Compartilhado;
using e_AgendaMedica.Aplicacao.Compartilhado;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloMedico;
using FluentResults;

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
    }
}

