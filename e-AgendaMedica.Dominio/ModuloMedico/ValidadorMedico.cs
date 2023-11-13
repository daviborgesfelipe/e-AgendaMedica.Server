using FluentValidation.Validators;
using FluentValidation;
using e_AgendaMedica.Dominio.Compartilhado;

namespace e_AgendaMedica.Dominio.ModuloMedico
{
    public class ValidadorMedico : AbstractValidator<Medico>, IValidadorMedico
    {
        public ValidadorMedico()
        {
            RuleFor(x => x.Nome)
                .NotNull().NotEmpty();

            RuleFor(x => x.Especialidade)
                .NotNull().NotEmpty();

            RuleFor(x => x.CRM)
                .CRM()
                .NotNull().NotEmpty(); 
        }

    }
}
