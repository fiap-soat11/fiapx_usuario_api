using Domain;

namespace Adapters.Gateways.Interfaces
{
    public interface IDataSource
    {
        #region Usuario DataSource
        Task AtualizarUsuario(Usuario usuario);
        Task<Usuario> BuscarUsuarioPorEmail(string email);
        Task<IEnumerable<Usuario>> ListarTodos();
        Task ExcluirUsuario(Usuario usuario);
        Task<Usuario> IncluirUsuario(Usuario usuario);

        #endregion

    }

} 
