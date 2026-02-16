using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Gateways.Interfaces
{
    public interface IUsuarioGateway
    {
        Task IncluirUsuario(Usuario cliente);
        Task AtualizarUsuario(Usuario cliente);
        Task ExcluirUsuario(Usuario cliente);
        Task<IEnumerable<Usuario>> ListarTodos();
        Task<Usuario> BuscarUsuarioPorEmail(string email);
    }
}
