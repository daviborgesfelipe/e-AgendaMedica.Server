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

            //VALIDAR
            RuleFor(x => x.TipoAtividade);

            RuleFor(x => x.ListaMedicos)
                .NotNull().NotEmpty();
        }
    }
}
