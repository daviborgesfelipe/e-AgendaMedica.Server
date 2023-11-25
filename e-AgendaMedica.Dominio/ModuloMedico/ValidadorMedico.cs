using FluentValidation.Validators;
using FluentValidation;
using e_AgendaMedica.Dominio.Compartilhado;
using e_AgendaMedica.Dominio.ModuloMedico.Interfaces;

namespace e_AgendaMedica.Dominio.ModuloMedico
{
    public class ValidadorMedico : AbstractValidator<Medico>, IValidadorMedico
    {
        public ValidadorMedico()
        {
            RuleFor(x => x.Nome)
                .MinimumLength(2).WithMessage("O campo nome deve conter no minimo 2 caracteres")
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Especialidade)
                .MinimumLength(3).WithMessage("O campo especialidade deve conter no minimo 3 caracteres")
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.CRM)
                .CRM()
                .NotNull()
                .NotEmpty(); 
        }
    }
}
