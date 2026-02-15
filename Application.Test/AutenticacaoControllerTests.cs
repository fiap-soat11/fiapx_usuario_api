using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Adapters.Controllers;
using Adapters.Gateways.Interfaces;
using Adapters.Presenters.Autenticacao;
using Application.Configurations;
using Application.Interfaces;
using Application.UseCases;
using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application.Test.Controllers
{
    [TestClass]
    public class AutenticacaoControllerTests
    {
        private Mock<ILogger<AutenticacaoController>> _mockLogger;
        private Mock<IAutenticacaoUseCase> _mockAutenticacaoUseCase;
        private Mock<IUsuarioGateway> _mockUsuarioGateway;
        private Mock<IUsuarioUseCase> _mockUsuarioUseCase;
        private AutenticacaoController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<AutenticacaoController>>();
            _mockAutenticacaoUseCase = new Mock<IAutenticacaoUseCase>();
            _mockUsuarioGateway = new Mock<IUsuarioGateway>();
            _mockUsuarioUseCase = new Mock<IUsuarioUseCase>();

            _controller = new AutenticacaoController(
                _mockLogger.Object,
                _mockAutenticacaoUseCase.Object,
                _mockUsuarioGateway.Object,
                _mockUsuarioUseCase.Object
            );
        }

        [TestMethod]
        public async Task GerarToken_ValidCredentials_ReturnsToken()
        {
            var request = new AutenticacaoRequest
            {
                Email = "joao@email.com",
                Password = "senha123"
            };

            var usuario = new Usuario
            {
                Id = 1,
                Nome = "João",
                Email = "joao@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("senha123", workFactor: 12)
            };

            var expectedToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.test.token";

            _mockUsuarioGateway.Setup(g => g.BuscarUsuarioPorEmail(request.Email))
                .ReturnsAsync(usuario);

            _mockUsuarioUseCase.Setup(u => u.VerificarSenha(
                request.Password, usuario.Password))
                .Returns(true);

            _mockAutenticacaoUseCase.Setup(a => a.GerarToken(usuario.Email, usuario.Password))
                .ReturnsAsync(expectedToken);

            var result = await _controller.GerarToken(request);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedToken, result.Token);
            Assert.AreEqual("Bearer", result.TokenType);
            Assert.AreEqual(usuario.Email, result.Email);
            Assert.AreEqual(usuario.Nome, result.Nome);
            Assert.IsNotNull(result.ExpiresAt);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task GerarToken_NonExistingEmail_ThrowsBusinessException()
        {
            var request = new AutenticacaoRequest
            {
                Email = "naoexiste@email.com",
                Password = "senha123"
            };

            _mockUsuarioGateway.Setup(g => g.BuscarUsuarioPorEmail(request.Email))
                .ReturnsAsync((Usuario)null);

            await _controller.GerarToken(request);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task GerarToken_InvalidPassword_ThrowsBusinessException()
        {
            var request = new AutenticacaoRequest
            {
                Email = "joao@email.com",
                Password = "senhaErrada"
            };

            var usuario = new Usuario
            {
                Id = 1,
                Nome = "João",
                Email = "joao@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("senha123", workFactor: 12)
            };

            _mockUsuarioGateway.Setup(g => g.BuscarUsuarioPorEmail(request.Email))
                .ReturnsAsync(usuario);

            _mockUsuarioUseCase.Setup(u => u.VerificarSenha(
                request.Password, usuario.Password))
                .Returns(false);

            await _controller.GerarToken(request);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task GerarToken_GatewayThrowsException_ThrowsBusinessException()
        {
            var request = new AutenticacaoRequest
            {
                Email = "joao@email.com",
                Password = "senha123"
            };

            var usuario = new Usuario
            {
                Id = 1,
                Nome = "João",
                Email = "joao@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("senha123", workFactor: 12)
            };

            _mockUsuarioGateway.Setup(g => g.BuscarUsuarioPorEmail(request.Email))
                .ReturnsAsync(usuario);

            _mockUsuarioUseCase.Setup(u => u.VerificarSenha(
                request.Password, usuario.Password))
                .Returns(true);

            _mockAutenticacaoUseCase.Setup(a => a.GerarToken(usuario.Email, usuario.Password))
                .ThrowsAsync(new Exception("Token generation error"));

            await _controller.GerarToken(request);
        }
    }
}
