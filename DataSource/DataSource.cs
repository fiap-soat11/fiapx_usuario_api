using Application.Configurations;
using DataSource.Repositories.Interfaces;
using Domain;
using System.Linq;

namespace DataSource
{
    public class DataSource : Adapters.Gateways.Interfaces.IDataSource
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public DataSource(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        #region Usuario Datasource
        public async Task AtualizarUsuario(Usuario usuario)
        {
            _usuarioRepository.Atualizar(usuario);
        }
        
        public async Task<Usuario> BuscarUsuarioPorEmail(string email)
        {
            return _usuarioRepository.Buscar(x => x.Email.Equals(email)).FirstOrDefault();
        }

        public async Task<IEnumerable<Usuario>> ListarTodos()
        {
            return _usuarioRepository.ListarTodos();
        }

        public async Task ExcluirUsuario(Usuario usuario)
        {
            if (!_usuarioRepository.Existe(x => x.Email == usuario.Email))
                throw new BusinessException("Email não cadastrado");

            _usuarioRepository.Atualizar(usuario);
        }

        public async Task<Usuario> IncluirUsuario(Usuario usuario)
        {
            if (_usuarioRepository.Existe(x => x.Email == usuario.Email))
                throw new BusinessException("Usuario já existe");

            _usuarioRepository.Inserir(usuario);
            return usuario;
        }
        #endregion

    }
}