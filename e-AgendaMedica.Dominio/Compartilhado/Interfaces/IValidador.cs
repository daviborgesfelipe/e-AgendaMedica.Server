using FluentValidation.Results;

namespace e_AgendaMedica.Dominio.Compartilhado.Interfaces
{
    public interface IValidador<T> where T : EntidadeBase<T>
    {
        public ValidationResult Validate(T instance);
    }
}