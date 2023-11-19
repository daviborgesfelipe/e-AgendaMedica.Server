using AutoMapper;
using e_AgendaMedica.Aplicacao.Compartilhado;
using e_AgendaMedica.Dominio.Compartilhado.Interfaces;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloAtividade.Interfaces;
using e_AgendaMedica.Dominio.ModuloMedico.Interfaces;
using FluentResults;
using Serilog;


namespace e_AgendaMedica.Aplicacao.ModuloAtividade
{
    public class ServicoAtividade : ServicoBase<Atividade, ValidadorAtividade>, IServicoAtividade
    {
        private IRepositorioAtividade repositorioAtividade;
        private IContextoPersistencia contextoPersistencia;
        private IRepositorioMedico repositorioMedico;
        private IMapper mapeador;

        public ServicoAtividade(IRepositorioAtividade repositorioAtividade,
                                IContextoPersistencia contextoPersistencia,
                                IRepositorioMedico repositorioMedico,
                                IMapper mapeador)
        {
            this.repositorioAtividade = repositorioAtividade;
            this.contextoPersistencia = contextoPersistencia;
            this.repositorioMedico = repositorioMedico;
            this.mapeador = mapeador;
        }

        public async Task<Result<Atividade>> InserirAsync(Atividade atividade)
        {
            Result resultado = Validar(atividade);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            if (await ConflitoComOutrasAtividades(atividade))
            {
                Log.Logger.Warning("Atividade de Id:{AtividadeId}, tem conflito de horários com outras atividades.", atividade.Id);
                return Result.Fail("Conflito de horários com outras atividades.");
            }

            if (!await VerificarTempoRecuperacaoNecessario(atividade))
            {
                Log.Logger.Warning("Tempo de recuperação insuficiente para atividade de cirurgia do médico. Atividade de Id:{AtividadeId}", atividade.Id);
                return Result.Fail<Atividade>("Tempo de recuperação insuficiente para atividade de cirurgia do médico.");
            }

            if (ConferirAtividadeCirurgica(atividade) && atividade.ListaMedicos.Count > 1)
            {
                Log.Logger.Warning("Atividades do tipo cirurgia. A lista de médicos deve conter no máximo um médico. Atividade de Id:{AtividadeId} contém uma lista de médicos com o total de: {ListaMedicosCount}", atividade.Id, atividade.ListaMedicos.Count);


                return Result.Fail<Atividade>("Para atividades do tipo cirurgia, a lista de médicos deve conter no máximo um médico.");
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

            if (await ConflitoComOutrasAtividades(atividade))
            {
                Log.Logger.Warning("Atividade de Id:{AtividadeId} contém conflito de horários com outras atividades", atividade.Id);

                return Result.Fail("Conflito de horários com outras atividades.");
            }

            if (!await VerificarTempoRecuperacaoNecessario(atividade))
            {
                Log.Logger.Warning("Tempo de recuperação insuficiente para atividade de cirurgia do médico. Atividade de Id:{AtividadeId}", atividade.Id);
                return Result.Fail<Atividade>("Tempo de recuperação insuficiente para atividade de cirurgia do médico.");
            }

            if (ConferirAtividadeCirurgica(atividade))
            {
                if (TemMaisDeUmMedicoDiferente(atividade, atividadeExistente))
                {
                    Log.Logger.Warning("Atividades do tipo cirurgia. A lista de médicos deve conter no máximo um médico. Atividade de Id:{AtividadeId} contém uma lista de médicos com o total de: {ListaMedicosCount}", atividade.Id, atividade.ListaMedicos.Count);
                    
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

        public async Task<Result<List<MedicoComHorasVM>>> ObterMedicosMaisHorasTrabalhadas(DateTime dataInicio, DateTime dataFim)
        {
            var listaAtividadesNoPeriodo = await repositorioAtividade.ObterAtividadesNoPeriodoAsync(dataInicio, dataFim);

            var horasTrabalhadasPorMedico = new Dictionary<Guid, TimeSpan>();

            foreach (var atividade in listaAtividadesNoPeriodo)
            {
                var atividadeComMedico = await ObterPorIdAsync(atividade.Id);
                foreach (var medico in atividadeComMedico.Value.ListaMedicos)
                {
                    if (!horasTrabalhadasPorMedico.ContainsKey(medico.Id))
                    {
                        horasTrabalhadasPorMedico[medico.Id] = TimeSpan.Zero;
                    }

                    horasTrabalhadasPorMedico[medico.Id] += atividade.HorarioTermino - atividade.HorarioInicio;
                }
            }

            var listaIdsMedicosMaisHorasTrabalhadas = 
                    horasTrabalhadasPorMedico.OrderByDescending(medicoHoras => medicoHoras.Value)
                                  .Take(10)
                                  .Select(medicoHoras => medicoHoras.Key)
                                  .ToList();

            var listaMedicos = await repositorioMedico.ObterMuitos(listaIdsMedicosMaisHorasTrabalhadas);
            
            var listaMedicosFinal = mapeador.Map<List<MedicoComHorasVM>>(listaMedicos.ToResult().Value);
            
            foreach (var medico in listaMedicosFinal)
                medico.TotalHorasTrabalhadas += horasTrabalhadasPorMedico.GetValueOrDefault(medico.Id, TimeSpan.Zero);

            return Result.Ok(listaMedicosFinal);
        }

        #region Verifica Conflitos

        private bool TemMaisDeUmMedicoDiferente(Atividade atividade, Result<Atividade> atividadeExistente)
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

        private async Task<bool> VerificarTempoRecuperacaoNecessario(Atividade atividade)
        {
            foreach (var medico in atividade.ListaMedicos)
            {
                var tempoRecuperacaoNecessario = atividade.TipoAtividade == TipoAtividadeEnum.Cirurgia
                    ? TimeSpan.FromHours(4)
                    : TimeSpan.FromMinutes(20);

                var ultimaAtividadeConcluida = await repositorioAtividade.ObterUltimaAtividadeConcluidaDoMedicoAsync(medico.Id);

                if (ultimaAtividadeConcluida != null &&
                    atividade.Data <= (ultimaAtividadeConcluida.Data += ultimaAtividadeConcluida.HorarioTermino.Add(tempoRecuperacaoNecessario)))
                {
                    Log.Logger.Warning("Tempo de recuperação insuficiente para atividade do médico {MedicoId}. Atividade de Id:{AtividadeId}", medico.Id, atividade.Id);
                    return false;
                }
            }

            return true;
        }

        #endregion


    }
}

