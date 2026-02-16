using Adapters.Controllers.Interfaces;
using Adapters.Gateways.Interfaces;
using Adapters.Presenters.Usuario;
using Application.Configurations;
using Application.UseCases;
using Microsoft.Extensions.Logging;
using WebAPI.Mappers;

namespace Adapters.Controllers
{
    public class UsuarioController : IUsuarioController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioUseCase _usuarioUseCase;
        private readonly IUsuarioGateway _usuarioGateway;

        public UsuarioController(ILogger<UsuarioController> logger,
                                 IUsuarioUseCase usuarioUseCase,
                                 IUsuarioGateway usuarioGateway)
        {
            _logger = logger;
            _usuarioUseCase = usuarioUseCase;
            _usuarioGateway = usuarioGateway;
        }

        public async Task<List<UsuarioResponse>?> ListarUsuarios()
        {
            var listaUsuarios = new List<UsuarioResponse>();
            var usuarios = await _usuarioGateway.ListarTodos();
            if (usuarios == null || !usuarios.Any())
                return listaUsuarios;                       
            listaUsuarios = usuarios.Select(usuario => UsuarioMapper.ToDTO(usuario))
                                    .ToList();
            return listaUsuarios;
        }

        public async Task<UsuarioResponse?> BuscarUsuario(string email)
        {
            try
            {
                var usuario = await _usuarioGateway.BuscarUsuarioPorEmail(email);
                if (usuario == null)
                    return null;                
                var usuarioResponse = UsuarioMapper.ToDTO(usuario);
                return usuarioResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuario");
                throw;
            }

        }

        public async Task IncluirUsuario(UsuarioRequest usuarioRequest)
        {
            try
            {
                var usuarioExistente = await _usuarioGateway.BuscarUsuarioPorEmail(usuarioRequest.Email);
                if (usuarioExistente != null)
                    throw new BusinessException("Usuario já cadastrado com o Email informado");



                var usuario = UsuarioMapper.ToEntity(usuarioRequest);
                if(usuarioExistente is null)
                {
                    var novoUsuario = await _usuarioUseCase.IncluirUsuario(usuario);
                    await _usuarioGateway.IncluirUsuario(novoUsuario);
                }
                else
                {
                    var usuarioAtualizado = await _usuarioUseCase.AtualizarUsuario(usuarioExistente, usuario);
                    await _usuarioGateway.AtualizarUsuario(usuarioAtualizado);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao incluir usuario");
                throw;
            }
        }

        public async Task AtualizarUsuario(UsuarioRequest usuarioRequest)
        {
            try
            {
                var usuario = await _usuarioGateway.BuscarUsuarioPorEmail(usuarioRequest.Email);
                if (usuario == null)
                    throw new BusinessException("Usuario não encontrado");
                var novoUsuario = UsuarioMapper.ToEntity(usuarioRequest);
                novoUsuario = await _usuarioUseCase.AtualizarUsuario(usuario, novoUsuario);
                await _usuarioGateway.AtualizarUsuario(novoUsuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar usuario");
                throw;
                
            }

        }

        public async Task ExcluirUsuario(string email)
        {
            try
            {
                var usuario = await _usuarioGateway.BuscarUsuarioPorEmail(email);
                if (usuario == null)
                    throw new BusinessException("Usuario não encontrado");
                var usuarioExcluido = await _usuarioUseCase.ExcluirUsuario(usuario);
                await _usuarioGateway.ExcluirUsuario(usuarioExcluido);
            }            
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir usuario");
                throw;
            }

        }

    }
}