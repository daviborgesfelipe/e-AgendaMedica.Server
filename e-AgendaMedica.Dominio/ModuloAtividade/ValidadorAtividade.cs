using e_AgendaMedica.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.ModuloAtividade.Interfaces;
using FluentValidation;

namespace e_AgendaMedica.Dominio.ModuloAtividade
{
    public class ValidadorAtividade : AbstractValidator<Atividade>, IValidadorAtividade
    {
        public ValidadorAtividade()
        {
            RuleFor(x => x.Paciente)
                .MinimumLength(3)
                .NotNull().WithMessage("O campo paciente é obrigatório")
                .NotEmpty().WithMessage("O campo paciente é obrigatório");

            RuleFor(x => x.Data)
                .NotEqual(DateTime.MinValue).WithMessage("O campo data é obrigatório");

            RuleFor(x => x.HorarioTermino)
                .GreaterThanOrEqualTo((x) => x.HorarioInicio).WithMessage("O campo horario de termino não pode ser maior que o horario de inicio é obrigatório")
                .NotEqual(TimeSpan.MinValue).WithMessage("O campo horario termino é obrigatório")
                .NotEmpty().WithMessage("O campo horario termino é obrigatório");

            RuleFor(x => x.HorarioInicio)
                .NotEqual(TimeSpan.MinValue).WithMessage("O campo horario inicio é obrigatório");

            //RuleFor(x => x.TipoAtividade)
            //    .Equal(TipoAtividadeEnum.Cirurgia)
            //    .When(x => x.ListaMedicos != null && x.ListaMedicos.Count != 1)
            //    .WithMessage("Para atividades do tipo cirurgia, a lista de médicos deve conter exatamente um médico.");

            //RuleFor(x => x.TipoAtividade)
            //    .Equal(TipoAtividadeEnum.Consulta)
            //    .When(x => x.ListaMedicos == null || !x.ListaMedicos.Any())
            //    .WithMessage("Para atividades do tipo consulta, a lista de médicos não pode estar vazia.");

            //VALIDAR
            RuleFor(x => x.TipoAtividade);

            RuleFor(x => x.ListaMedicos)
                .NotNull().WithMessage("O campo lista de pacientes é obrigatório")
                .NotEmpty().WithMessage("O campo lista de pacientes é obrigatório");
        }
    }
}
