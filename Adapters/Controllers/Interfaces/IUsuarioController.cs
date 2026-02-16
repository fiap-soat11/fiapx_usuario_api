using Adapters.Presenters.Usuario;

namespace Adapters.Controllers.Interfaces
{
    public interface IUsuarioController
    {
        Task<List<UsuarioResponse>?> ListarUsuarios();

        Task<UsuarioResponse?> BuscarUsuario(string email);

        Task IncluirUsuario(UsuarioRequest usuario);

        Task AtualizarUsuario(UsuarioRequest usuario);

        Task ExcluirUsuario(string email);
    }
}
