using e_AgendaMedica.Dominio.ModuloAutenticacao;
using Taikandi;

namespace e_AgendaMedica.Dominio.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public Guid Id { get; set; }

        public Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }

        public EntidadeBase()
        {
            Id = SequentialGuid.NewGuid();
        }
    }
}
