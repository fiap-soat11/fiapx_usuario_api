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
            var usuario = new Usuario
            {
                Password = "123Teste",
                Nome = "João",
                Email = "joao@email.com",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var result = await _usuarioUseCase.IncluirUsuario(usuario);

            Assert.IsNotNull(result);
            Assert.AreEqual(usuario.Nome, result.Nome);
            Assert.AreEqual(usuario.Email, result.Email);
            Assert.AreNotEqual("123Teste", result.Password);
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
        public async Task IncluirUsuario_NullPassword_ThrowsException()
        {
            var usuario = new Usuario { Password = null, Nome = "Teste", Email = "teste@email.com" };
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
        public async Task IncluirUsuario_NullNome_ThrowsException()
        {
            var usuario = new Usuario { Password = "123Teste", Nome = null, Email = "teste@email.com" };
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
        [ExpectedException(typeof(ArgumentException))]
        public async Task IncluirUsuario_NullEmail_ThrowsException()
        {
            var usuario = new Usuario { Password = "123Teste", Nome = "Teste", Email = null };
            await _usuarioUseCase.IncluirUsuario(usuario);
        }

        [TestMethod]
        public async Task AtualizarUsuario_ValidData_ReturnsUpdatedUsuario()
        {
            var usuarioOriginal = new Usuario
            {
                Password = BCrypt.Net.BCrypt.HashPassword("senhaAntiga", workFactor: 12),
                Nome = "João",
                Email = "joao@email.com",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var usuarioAtualizado = new Usuario
            {
                Password = "novaSenha123",
                Nome = "João da Silva",
                Email = "joao@email.com",
                CreatedAt = usuarioOriginal.CreatedAt,
                UpdatedAt = usuarioOriginal.UpdatedAt
            };

            var result = await _usuarioUseCase.AtualizarUsuario(usuarioOriginal, usuarioAtualizado);

            Assert.AreEqual(usuarioAtualizado.Nome, result.Nome);
            Assert.AreEqual(usuarioOriginal.Email, result.Email);
            Assert.IsNotNull(result.UpdatedAt);
        }

        [TestMethod]
        public async Task AtualizarUsuario_SamePassword_DoesNotChangePassword()
        {
            var originalPasswordHash = BCrypt.Net.BCrypt.HashPassword("senha123", workFactor: 12);
            var usuarioOriginal = new Usuario
            {
                Password = originalPasswordHash,
                Nome = "João",
                Email = "joao@email.com",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var usuarioAtualizado = new Usuario
            {
                Password = originalPasswordHash,
                Nome = "João Silva",
                Email = "joao@email.com"
            };

            var result = await _usuarioUseCase.AtualizarUsuario(usuarioOriginal, usuarioAtualizado);

            Assert.AreEqual(originalPasswordHash, result.Password);
            Assert.AreEqual("João Silva", result.Nome);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AtualizarUsuario_NullUsuarioOriginal_ThrowsException()
        {
            var novoUsuario = new Usuario { Password = "123", Nome = "Teste", Email = "teste@email.com" };
            await _usuarioUseCase.AtualizarUsuario(null, novoUsuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AtualizarUsuario_NullNovoUsuario_ThrowsException()
        {
            var usuarioOriginal = new Usuario { Password = "123", Nome = "Teste", Email = "teste@email.com" };
            await _usuarioUseCase.AtualizarUsuario(usuarioOriginal, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task AtualizarUsuario_DifferentEmail_ThrowsException()
        {
            var usuarioOriginal = new Usuario
            {
                Password = "123Teste",
                Nome = "João",
                Email = "joao@email.com"
            };

            var usuarioAtualizado = new Usuario
            {
                Password = "123Teste",
                Nome = "João",
                Email = "outro@email.com"
            };

            await _usuarioUseCase.AtualizarUsuario(usuarioOriginal, usuarioAtualizado);
        }

        [TestMethod]
        public async Task ExcluirUsuario_ValidUsuario_ReturnsUsuario()
        {
            var usuario = new Usuario
            {
                Password = "123Teste",
                Nome = "João",
                Email = "joao@email.com"
            };

            var result = await _usuarioUseCase.ExcluirUsuario(usuario);

            Assert.IsNotNull(result);
            Assert.AreEqual(usuario.Email, result.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ExcluirUsuario_NullUsuario_ThrowsException()
        {
            await _usuarioUseCase.ExcluirUsuario(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ExcluirUsuario_EmptyEmail_ThrowsException()
        {
            var usuario = new Usuario { Password = "123Teste", Nome = "Teste", Email = "" };
            await _usuarioUseCase.ExcluirUsuario(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ExcluirUsuario_NullEmail_ThrowsException()
        {
            var usuario = new Usuario { Password = "123Teste", Nome = "Teste", Email = null };
            await _usuarioUseCase.ExcluirUsuario(usuario);
        }

        [TestMethod]
        public void VerificarSenha_ValidPassword_ReturnsTrue()
        {
            var senha = "senha123";
            var hash = BCrypt.Net.BCrypt.HashPassword(senha, workFactor: 12);

            var result = _usuarioUseCase.VerificarSenha(senha, hash);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VerificarSenha_InvalidPassword_ReturnsFalse()
        {
            var senha = "senha123";
            var senhaErrada = "senhaErrada";
            var hash = BCrypt.Net.BCrypt.HashPassword(senha, workFactor: 12);

            var result = _usuarioUseCase.VerificarSenha(senhaErrada, hash);

            Assert.IsFalse(result);
        }
    }
}
