using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Adapters.Controllers;
using Adapters.Gateways.Interfaces;
using Adapters.Presenters.Usuario;
using Application.Configurations;
using Application.UseCases;
using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Test.Controllers
{
    [TestClass]
    public class UsuarioControllerTests
    {
        private Mock<ILogger<UsuarioController>> _mockLogger;
        private Mock<IUsuarioUseCase> _mockUsuarioUseCase;
        private Mock<IUsuarioGateway> _mockUsuarioGateway;
        private UsuarioController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<UsuarioController>>();
            _mockUsuarioUseCase = new Mock<IUsuarioUseCase>();
            _mockUsuarioGateway = new Mock<IUsuarioGateway>();
            _controller = new UsuarioController(
                _mockLogger.Object,
                _mockUsuarioUseCase.Object,
                _mockUsuarioGateway.Object
            );
        }

        [TestMethod]
        public async Task ListarUsuarios_WithUsuarios_ReturnsUsuariosList()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nome = "João", Email = "joao@email.com", Password = "hash1" },
                new Usuario { Id = 2, Nome = "Maria", Email = "maria@email.com", Password = "hash2" }
            };

            _mockUsuarioGateway.Setup(g => g.ListarTodos()).ReturnsAsync(usuarios);

            var result = await _controller.ListarUsuarios();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("João", result[0].Nome);
            Assert.AreEqual("Maria", result[1].Nome);
        }

        [TestMethod]
        public async Task ListarUsuarios_WithNoUsuarios_ReturnsEmptyList()
        {
            _mockUsuarioGateway.Setup(g => g.ListarTodos()).ReturnsAsync(new List<Usuario>());

            var result = await _controller.ListarUsuarios();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task ListarUsuarios_WithNull_ReturnsEmptyList()
        {
            _mockUsuarioGateway.Setup(g => g.ListarTodos()).ReturnsAsync((IEnumerable<Usuario>)null);

            var result = await _controller.ListarUsuarios();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task BuscarUsuario_ExistingEmail_ReturnsUsuario()
        {
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "João",
                Email = "joao@email.com",
                Password = "hash"
            };

            _mockUsuarioGateway.Setup(g => g.BuscarUsuarioPorEmail("joao@email.com"))
                .ReturnsAsync(usuario);

            var result = await _controller.BuscarUsuario("joao@email.com");

            Assert.IsNotNull(result);
            Assert.AreEqual("João", result.Nome);
            Assert.AreEqual("joao@email.com", result.Email);
        }

        [TestMethod]
        public async Task BuscarUsuario_NonExistingEmail_ReturnsNull()
        {
            _mockUsuarioGateway.Setup(g => g.BuscarUsuarioPorEmail(It.IsAny<string>()))
                .ReturnsAsync((Usuario)null);

            var result = await _controller.BuscarUsuario("naoexiste@email.com");

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task BuscarUsuario_GatewayThrowsException_ThrowsException()
        {
            _mockUsuarioGateway.Setup(g => g.BuscarUsuarioPorEmail(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Database error"));

            await _controller.BuscarUsuario("joao@email.com");
        }

        [TestMethod]
        public async Task IncluirUsuario_NewUsuario_InsertsUsuario()
        {
            var request = new UsuarioRequest
            {
                Nome = "João",
                Email = "joao@email.com",
                Password = "senha123"
            };

            var usuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Password = request.Password
            };

            _mockUsuarioGateway.Setup(g => g.BuscarUsuarioPorEmail(request.Email))
                .ReturnsAsync((Usuario)null);

            _mockUsuarioUseCase.Setup(u => u.IncluirUsuario(It.IsAny<Usuario>()))
                .ReturnsAsync(usuario);

            await _controller.IncluirUsuario(request);

            _mockUsuarioGateway.Verify(g => g.IncluirUsuario(It.IsAny<Usuario>()), Times.Once);
            _mockUsuarioGateway.Verify(g => g.AtualizarUsuario(It.IsAny<Usuario>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task IncluirUsuario_ExistingEmail_ThrowsBusinessException()
        {
            var request = new UsuarioRequest
            {
                Nome = "João",
                Email = "joao@email.com",
                Password = "senha123"
            };

            var usuarioExistente = new Usuario
            {
                Id = 1,
                Nome = "João Existente",
                Email = "joao@email.com",
                Password = "hash"
            };

            _mockUsuarioGateway.Setup(g => g.BuscarUsuarioPorEmail(request.Email))
                .ReturnsAsync(usuarioExistente);

            await _controller.IncluirUsuario(request);
        }

        [TestMethod]
        public async Task AtualizarUsuario_ExistingUsuario_UpdatesUsuario()
        {
            var request = new UsuarioRequest
            {
                Nome = "João Atualizado",
                Email = "joao@email.com",
                Password = "novaSenha123"
            };

            var usuarioExistente = new Usuario
            {
                Id = 1,
                Nome = "João",
                Email = "joao@email.com",
                Password = "hashAntigo"
            };

            var usuarioAtualizado = new Usuario
            {
                Id = 1,
                Nome = "João Atualizado",
                Email = "joao@email.com",
                Password = "hashNovo"
            };

            _mockUsuarioGateway.Setup(g => g.BuscarUsuarioPorEmail(request.Email))
                .ReturnsAsync(usuarioExistente);

            _mockUsuarioUseCase.Setup(u => u.AtualizarUsuario(
                It.IsAny<Usuario>(), It.IsAny<Usuario>()))
                .ReturnsAsync(usuarioAtualizado);

            await _controller.AtualizarUsuario(request);

            _mockUsuarioGateway.Verify(g => g.AtualizarUsuario(It.IsAny<Usuario>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task AtualizarUsuario_NonExistingUsuario_ThrowsBusinessException()
        {
            var request = new UsuarioRequest
            {
                Nome = "João",
                Email = "naoexiste@email.com",
                Password = "senha123"
            };

            _mockUsuarioGateway.Setup(g => g.BuscarUsuarioPorEmail(request.Email))
                .ReturnsAsync((Usuario)null);

            await _controller.AtualizarUsuario(request);
        }
    }
}
