using Domain;

namespace Application.UseCases
{
    public interface IUsuarioUseCase
    {
        Task<Usuario> AtualizarUsuario(Usuario entity, Usuario novoUsuario);
        Task<Usuario> ExcluirUsuario(Usuario entity);
        Task<Usuario> IncluirUsuario(Usuario novoUsuario);
        bool VerificarSenha(string senha, string hash);
    }
}
