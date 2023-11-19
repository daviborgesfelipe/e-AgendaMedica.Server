using e_AgendaMedica.Dominio.Compartilhado;
using Microsoft.AspNetCore.Identity;
using Taikandi;

namespace e_AgendaMedica.Dominio.ModuloAutenticacao
{
    public class Usuario : IdentityUser<Guid>
    {
        public string Nome { get; set; }
    }
}
