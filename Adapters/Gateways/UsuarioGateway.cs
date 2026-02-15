using System.Collections.Generic;
using System.Threading.Tasks;
using Adapters.Gateways.Interfaces;
using Domain;

namespace Adapters.Gateways
{
    public class UsuarioGateway : IUsuarioGateway
    {
        private readonly IDataSource _usuarioDataSource;

        public UsuarioGateway(IDataSource usuarioDataSource)
        {
            _usuarioDataSource = usuarioDataSource;
        }

        public async Task AtualizarUsuario(Usuario usuario)
        {
            await _usuarioDataSource.AtualizarUsuario(usuario);
        }

        public async Task<Usuario> BuscarUsuarioPorEmail(string email)
        {
            return await _usuarioDataSource.BuscarUsuarioPorEmail(email);
        }

        public async Task ExcluirUsuario(Usuario usuario)
        {
            await _usuarioDataSource.ExcluirUsuario(usuario);
        }

        public async Task IncluirUsuario(Usuario usuario)
        {
            await _usuarioDataSource.IncluirUsuario(usuario);
        }

        public async Task<IEnumerable<Usuario>> ListarTodos()
        {
            return await _usuarioDataSource.ListarTodos();
        }
    }
}