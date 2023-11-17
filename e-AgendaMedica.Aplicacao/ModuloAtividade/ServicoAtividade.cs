using e_AgendaMedica.Aplicacao.Compartilhado;
using e_AgendaMedica.Dominio.Compartilhado.Interfaces;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloAtividade.Interfaces;
using e_AgendaMedica.Dominio.ModuloMedico.Interfaces;
using e_AgendaMedica.Dominio.ModuloMedico;
using FluentResults;
using Serilog;

namespace e_AgendaMedica.Aplicacao.ModuloAtividade
{
    public class ServicoAtividade : ServicoBase<Atividade, ValidadorAtividade>, IServicoAtividade
    {
        private IRepositorioAtividade repositorioAtividade;
        private IContextoPersistencia contextoPersistencia;
        private IRepositorioMedico repositorioMedico;

        public ServicoAtividade(IRepositorioAtividade repositorioAtividade,
                                IContextoPersistencia contextoPersistencia,
                                IRepositorioMedico repositorioMedico)
        {
            this.repositorioAtividade = repositorioAtividade;
            this.contextoPersistencia = contextoPersistencia;
            this.repositorioMedico = repositorioMedico;
        }

        public async Task<Result<Atividade>> InserirAsync(Atividade atividade)
        {
            Result resultado = Validar(atividade);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            if (ConferirAtividadeCirurgica(atividade))
            {
                if (atividade.ListaMedicos.Count > 1)
                    Log.Logger.Warning("Atividades do tipo cirurgia. A lista de médicos deve conter no máximo um médico. Atividade de Id:{AtividadeId} contém uma lista de médicos com o total de: {ListaMedicosCount}", atividade.Id, atividade.ListaMedicos.Count);


                return Result.Fail<Atividade>("Para atividades do tipo cirurgia, a lista de médicos deve conter no máximo um médico.");
            }

            if (await ConflitoComOutrasAtividades(atividade))
            {
                return Result.Fail("Conflito de horários com outras atividades.");
            }

            await repositorioAtividade.InserirAsync(atividade);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(atividade);
        }

        public async Task<Result<Atividade>> ObterPorIdAsync(Guid id)
        {
            var atividade = await repositorioAtividade.ObterPorIdAsync(id);

            if (atividade == null)
            {
                Log.Logger.Warning("Atividade {AtividadeId} não encontrada",id);

                return Result.Fail("Atividade não encontrada");
            }

            return Result.Ok(atividade);
        }

        public async Task<Result<List<Atividade>>> ObterTodosAsync()
        {
            var atividade = await repositorioAtividade.ObterTodosAsync();

            return Result.Ok(atividade);
        }

        public async Task<Result<Atividade>> EditarAsync(Atividade atividade)
        {
            Result resultado = Validar(atividade);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            var atividadeExistente = await ObterPorIdAsync(atividade.Id);

            if (ConferirAtividadeCirurgica(atividade))
            {
                if (ComparaMedicoJaEstaNaLista(atividade, atividadeExistente))
                {
                    if (atividade.ListaMedicos.Count > 1)
                        Log.Logger.Warning("Atividades do tipo cirurgia. A lista de médicos deve conter no máximo um médico. Atividade de Id:{AtividadeId} contém uma lista de médicos com o total de: {ListaMedicosCount}", atividade.Id, atividade.ListaMedicos.Count);


                    return Result.Fail<Atividade>("Para atividades do tipo cirurgia, a lista de médicos deve conter no máximo um médico.");
                }
            }

            if (await ConflitoComOutrasAtividades(atividade))
            {
                Log.Logger.Warning("Atividade de Id:{AtividadeId} contém conflito de horários com outras atividades", atividade.Id );

                return Result.Fail("Conflito de horários com outras atividades.");
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

        public async Task<Result<List<Medico>>> ObterMedicosMaisHorasTrabalhadas(DateTime dataInicio, DateTime dataFim)
        {
            // Lógica para calcular horas trabalhadas por médico dentro do período
            var atividadesNoPeriodo = await repositorioAtividade.ObterAtividadesNoPeriodoAsync(dataInicio, dataFim);

            var horasPorMedico = new Dictionary<Guid, TimeSpan>();

            foreach (var atividade in atividadesNoPeriodo)
            {
                var teste = ObterPorIdAsync(atividade.Id);
                foreach (var medico in teste.Result.Value.ListaMedicos)
                {
                    if (!horasPorMedico.ContainsKey(medico.Id))
                    {
                        horasPorMedico[medico.Id] = TimeSpan.Zero;
                    }

                    horasPorMedico[medico.Id] += atividade.HorarioTermino - atividade.HorarioInicio;
                }
            }

            // Ordenar os médicos com base nas horas trabalhadas e selecionar os 10 principais
            var top10Medicos = 
                horasPorMedico.OrderByDescending(pair => pair.Value)
                    .Take(10).Select(pair => pair.Key).ToList();

            var medicos = await repositorioMedico.ObterMuitos(top10Medicos);

            return Result.Ok(medicos);
        }


        #region Conferir Conflito

        private bool ComparaMedicoJaEstaNaLista(Atividade atividade, Result<Atividade> atividadeExistente)
        {
            return atividade.ListaMedicos.Any(medico => atividadeExistente.Value.ListaMedicos.Any(medicoExistente => medicoExistente.Id != medico.Id));
        }

        private async Task<bool> ConflitoComOutrasAtividades(Atividade atividade)
        {
            var atividadesDoMedico = await repositorioAtividade.ObterAtividadesDoMedicoAsync(atividade.ListaMedicos);
            
            return atividadesDoMedico.Any(outraAtividade => atividade.ConflitoCom(outraAtividade));
        }

        private bool ConferirAtividadeCirurgica(Atividade atividade)
        {
            return atividade.TipoAtividade == TipoAtividadeEnum.Cirurgia;
        }
        #endregion

    }
}

