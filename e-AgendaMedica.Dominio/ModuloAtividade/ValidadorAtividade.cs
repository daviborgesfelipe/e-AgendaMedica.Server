using e_AgendaMedica.Dominio.Compartilhado;
using FluentValidation;

namespace e_AgendaMedica.Dominio.ModuloAtividade
{
    public class ValidadorAtividade : AbstractValidator<Atividade>, IValidadorAtividade
    {
        public ValidadorAtividade()
        {
            RuleFor(x => x.Paciente)
                .MinimumLength(3)
                .NotNull().NotEmpty();

            RuleFor(x => x.Data)
                .NotNull().NotEmpty();

            RuleFor(x => x.HorarioTermino)
                .NotNull().NotEmpty().GreaterThanOrEqualTo((x) => x.HorarioInicio)
                .NotNull().NotEmpty();

            RuleFor(x => x.HorarioInicio)
                .NotNull().NotEmpty();

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
                .NotNull().NotEmpty();
        }
    }
}
