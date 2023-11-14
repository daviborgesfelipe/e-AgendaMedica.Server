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
