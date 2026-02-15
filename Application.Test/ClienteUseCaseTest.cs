using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.UseCases;
using Domain;
using System;
using System.Threading.Tasks;

namespace Application.Tests.UseCases
{
    [TestClass]
    public class UsuarioUseCaseTests
    {
        private UsuarioUseCase _usuarioUseCase;

        [TestInitialize]
        public void Setup()
        {
            _usuarioUseCase = new UsuarioUseCase();
        }

        [TestMethod]
        public async Task IncluirUsuario_ValidData_ReturnsUsuario()
        {
            // Arrange
            var usuario = new Usuario
            {
                Password = "123Teste",
                Nome = "João",
                Email = "joao@email.com",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // Act
            var result = await _usuarioUseCase.IncluirUsuario(usuario);

            // Assert
            Assert.AreEqual(usuario, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task IncluirUsuario_NullUsuario_ThrowsException()
        {
            await _usuarioUseCase.IncluirUsuario(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task IncluirUsuario_EmptyPassword_ThrowsException()
        {
            var usuario = new Usuario { Password = "", Nome = "Teste", Email = "teste@email.com" };
            await _usuarioUseCase.IncluirUsuario(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task IncluirUsuario_EmptyNome_ThrowsException()
        {
            var usuario = new Usuario { Password = "123Teste", Nome = "", Email = "teste@email.com" };
            await _usuarioUseCase.IncluirUsuario(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task IncluirUsuario_EmptyEmail_ThrowsException()
        {
            var usuario = new Usuario { Password = "123Teste", Nome = "Teste", Email = "" };
            await _usuarioUseCase.IncluirUsuario(usuario);
        }

        [TestMethod]
        public async Task AtualizarUsuario_ValidData_ReturnsUpdatedUsuario()
        {
            var usuarioOriginal = new Usuario
            {
                Password = "123Teste",
                Nome = "João",
                Email = "joao@email.com",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var usuarioAtualizado = new Usuario
            {
                Password = "123teste",
                Nome = "João da Silva",
                Email = "joao.silva@email.com",
                CreatedAt = usuarioOriginal.CreatedAt,
                UpdatedAt = usuarioOriginal.UpdatedAt.AddMinutes(10),
            };

            var result = await _usuarioUseCase.AtualizarUsuario(usuarioOriginal, usuarioAtualizado);

            Assert.AreEqual(usuarioAtualizado.Nome, result.Nome);
            Assert.AreEqual(usuarioAtualizado.Email, result.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AtualizarUsuario_NullUsuario_ThrowsException()
        {
            var novoUsuario = new Usuario { Password = "123teste", Nome = "Novo", Email = "novo@email.com" };
            await _usuarioUseCase.AtualizarUsuario(null, novoUsuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task AtualizarUsuario_PasswordDifferent_ThrowsException()
        {
            var usuarioOriginal = new Usuario { Password = "123teste", };
            var usuarioNovo = new Usuario { Password = "321Teste", };

            await _usuarioUseCase.AtualizarUsuario(usuarioOriginal, usuarioNovo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ExcluirUsuario_NullUsuario_ThrowsException()
        {
            await _usuarioUseCase.ExcluirUsuario(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ExcluirUsuario_EmptyPassword_ThrowsException()
        {
            var usuario = new Usuario { Password = "123teste", Nome = "Usuario" };

            await _usuarioUseCase.ExcluirUsuario(usuario);
        }
    }
}
