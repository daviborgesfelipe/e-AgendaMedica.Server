using e_AgendaMedica.Aplicacao.Compartilhado;
using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.Dominio.ModuloAutenticacao;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace e_AgendaMedica.Aplicacao.ModuloAutenticacao
{
    public class ServicoAutenticacao : ServicoBase<Usuario, ValidadorUsuario>
    {
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signManager;

        public ServicoAutenticacao(UserManager<Usuario> userManager, SignInManager<Usuario> signManager)
        {
            this.userManager = userManager;
            this.signManager = signManager;
        }
        public async Task<Result<Usuario>> RegistrarAsync(Usuario usuario, string senha) 
        {
            Result resultado = Validar(usuario);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            var usuarioEncontrado = await userManager.FindByEmailAsync(usuario.Email);

            if (usuarioEncontrado != null)
                return Result.Fail("Email ja cadastrado");

            IdentityResult identityResult = await userManager.CreateAsync(usuario, senha);

            if(identityResult.Succeeded == false)
            {
                Log.Logger.Warning("Usuario de Id:{UsuarioId}, ocorreu um erro ao registrar usuario.", usuario.Id);

                return Result.Fail(identityResult.Errors.Select(erro => new Error( erro.Description )));
            }

            return Result.Ok(usuario);
        }
        public async Task<Result<Usuario>> Autenticar(string login, string senha)
        {
            var resultado = await signManager.PasswordSignInAsync(login, senha, false, true);

            var erros = new List<Error>();

            if (resultado.IsLockedOut)
                erros.Add(new Error("O acesso para o usuario foi boqueado"));

            if (resultado.IsNotAllowed)
                erros.Add(new Error("O login ou senha estão incorretos"));

            if (erros.Count > 0)
                return Result.Fail(erros);

            var usuario = await userManager.FindByNameAsync(login);

            return Result.Ok(usuario);
        }

        public async Task<Result<Usuario>> Sair(string email)
        {
            await signManager.SignOutAsync();

            Log.Logger.Debug("Sessão do usuário {@email} removida...", email);

            return Result.Ok();
        }
    }
}
