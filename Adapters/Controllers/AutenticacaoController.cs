using Adapters.Controllers.Interfaces;
using Adapters.Gateways.Interfaces;
using Adapters.Presenters.Autenticacao;
using Application.Configurations;
using Application.Interfaces;
using Application.UseCases;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Controllers
{
    public class AutenticacaoController : IAutenticacaoController
    {
        private readonly ILogger<AutenticacaoController> _logger;
        private readonly IAutenticacaoUseCase _autenticacaoUseCase;
        private readonly IUsuarioGateway _usuarioGateway;
        private readonly IUsuarioUseCase _usuarioUseCase;

        public AutenticacaoController(
            ILogger<AutenticacaoController> logger,
            IAutenticacaoUseCase autenticacaoUseCase,
            IUsuarioGateway usuarioGateway,
            IUsuarioUseCase usuarioUseCase)
        {
            _logger = logger;
            _autenticacaoUseCase = autenticacaoUseCase;
            _usuarioGateway = usuarioGateway;
            _usuarioUseCase = usuarioUseCase;
        }

        public async Task<AutenticacaoResponse> GerarToken(AutenticacaoRequest autenticacao)
        {
            try
            {
                var usuario = await _usuarioGateway.BuscarUsuarioPorEmail(autenticacao.Email);
                if (usuario == null)
                {
                    _logger.LogWarning("Tentativa de login com email não cadastrado: {Email}", autenticacao.Email);
                    throw new BusinessException("Email ou senha inválidos");
                }

                var senhaValida = _usuarioUseCase.VerificarSenha(autenticacao.Password, usuario.Password);
                if (!senhaValida)
                {
                    _logger.LogWarning("Tentativa de login com senha incorreta para o email: {Email}", autenticacao.Email);
                    throw new BusinessException("Email ou senha inválidos");
                }

                var token = await _autenticacaoUseCase.GerarToken(usuario.Email, usuario.Password);

                var response = new AutenticacaoResponse
                {
                    Token = token,
                    TokenType = "Bearer",
                    ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                    Email = usuario.Email,
                    Nome = usuario.Nome
                };

                _logger.LogInformation("Token gerado com sucesso para o usuário: {Email}", usuario.Email);
                return response;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar token de autenticação");
                throw new BusinessException("Erro ao processar autenticação");
            }
        }
    }
}
