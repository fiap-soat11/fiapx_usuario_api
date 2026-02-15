using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Adapters.Gateways;
using Adapters.Gateways.Interfaces;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Test.Gateways
{
    [TestClass]
    public class UsuarioGatewayTests
    {
        private Mock<IDataSource> _mockDataSource;
        private UsuarioGateway _gateway;

        [TestInitialize]
        public void Setup()
        {
            _mockDataSource = new Mock<IDataSource>();
            _gateway = new UsuarioGateway(_mockDataSource.Object);
        }

        [TestMethod]
        public async Task ListarTodos_ReturnsUsuarios()
        {
            var expectedUsuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nome = "João", Email = "joao@email.com" },
                new Usuario { Id = 2, Nome = "Maria", Email = "maria@email.com" }
            };

            _mockDataSource.Setup(d => d.ListarTodos()).ReturnsAsync(expectedUsuarios);

            var result = await _gateway.ListarTodos();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            _mockDataSource.Verify(d => d.ListarTodos(), Times.Once);
        }

        [TestMethod]
        public async Task BuscarUsuarioPorEmail_ExistingEmail_ReturnsUsuario()
        {
            var expectedUsuario = new Usuario
            {
                Id = 1,
                Nome = "João",
                Email = "joao@email.com"
            };

            _mockDataSource.Setup(d => d.BuscarUsuarioPorEmail("joao@email.com"))
                .ReturnsAsync(expectedUsuario);

            var result = await _gateway.BuscarUsuarioPorEmail("joao@email.com");

            Assert.IsNotNull(result);
            Assert.AreEqual("João", result.Nome);
            Assert.AreEqual("joao@email.com", result.Email);
            _mockDataSource.Verify(d => d.BuscarUsuarioPorEmail("joao@email.com"), Times.Once);
        }

        [TestMethod]
        public async Task BuscarUsuarioPorEmail_NonExistingEmail_ReturnsNull()
        {
            _mockDataSource.Setup(d => d.BuscarUsuarioPorEmail(It.IsAny<string>()))
                .ReturnsAsync((Usuario)null);

            var result = await _gateway.BuscarUsuarioPorEmail("naoexiste@email.com");

            Assert.IsNull(result);
            _mockDataSource.Verify(d => d.BuscarUsuarioPorEmail("naoexiste@email.com"), Times.Once);
        }

        [TestMethod]
        public async Task IncluirUsuario_ValidUsuario_CallsDataSource()
        {
            var usuario = new Usuario
            {
                Nome = "João",
                Email = "joao@email.com",
                Password = "hash"
            };

            _mockDataSource.Setup(d => d.IncluirUsuario(usuario))
                .ReturnsAsync(usuario);

            await _gateway.IncluirUsuario(usuario);

            _mockDataSource.Verify(d => d.IncluirUsuario(usuario), Times.Once);
        }

        [TestMethod]
        public async Task AtualizarUsuario_ValidUsuario_CallsDataSource()
        {
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "João Atualizado",
                Email = "joao@email.com",
                Password = "hash"
            };

            _mockDataSource.Setup(d => d.AtualizarUsuario(usuario))
                .Returns(Task.CompletedTask);

            await _gateway.AtualizarUsuario(usuario);

            _mockDataSource.Verify(d => d.AtualizarUsuario(usuario), Times.Once);
        }

        [TestMethod]
        public async Task ExcluirUsuario_ValidUsuario_CallsDataSource()
        {
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "João",
                Email = "joao@email.com",
                Password = "hash"
            };

            _mockDataSource.Setup(d => d.ExcluirUsuario(usuario))
                .Returns(Task.CompletedTask);

            await _gateway.ExcluirUsuario(usuario);

            _mockDataSource.Verify(d => d.ExcluirUsuario(usuario), Times.Once);
        }
    }
}
