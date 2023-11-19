using FluentValidation;

namespace e_AgendaMedica.Dominio.ModuloAutenticacao
{
    public class ValidadorUsuario : AbstractValidator<Usuario>
    {
        public ValidadorUsuario()
        {
            RuleFor(x => x.Nome)
                .NotNull().NotEmpty();
        }
    }
}
