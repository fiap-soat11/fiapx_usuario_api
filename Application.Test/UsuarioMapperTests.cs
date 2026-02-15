using Microsoft.VisualStudio.TestTools.UnitTesting;
using Adapters.Presenters.Usuario;
using Domain;
using WebAPI.Mappers;
using System;

namespace Application.Test.Mappers
{
    [TestClass]
    public class UsuarioMapperTests
    {
        [TestMethod]
        public void ToEntity_ValidRequest_ReturnsUsuario()
        {
            var request = new UsuarioRequest
            {
                Nome = "João",
                Email = "joao@email.com",
                Password = "senha123"
            };

            var result = UsuarioMapper.ToEntity(request);

            Assert.IsNotNull(result);
            Assert.AreEqual(request.Nome, result.Nome);
            Assert.AreEqual(request.Email, result.Email);
            Assert.AreEqual(request.Password, result.Password);
            Assert.IsTrue(result.CreatedAt != default);
            Assert.IsTrue(result.UpdatedAt != default);
        }

        [TestMethod]
        public void ToEntity_ValidRequest_SetsTimestamps()
        {
            var request = new UsuarioRequest
            {
                Nome = "João",
                Email = "joao@email.com",
                Password = "senha123"
            };

            var beforeCreation = DateTime.Now.AddSeconds(-1);
            var result = UsuarioMapper.ToEntity(request);
            var afterCreation = DateTime.Now.AddSeconds(1);

            Assert.IsTrue(result.CreatedAt >= beforeCreation && result.CreatedAt <= afterCreation);
            Assert.IsTrue(result.UpdatedAt >= beforeCreation && result.UpdatedAt <= afterCreation);
        }

        [TestMethod]
        public void ToDTO_ValidUsuario_ReturnsResponse()
        {
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "João",
                Email = "joao@email.com",
                Password = "hash",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var result = UsuarioMapper.ToDTO(usuario);

            Assert.IsNotNull(result);
            Assert.AreEqual(usuario.Nome, result.Nome);
            Assert.AreEqual(usuario.Email, result.Email);
        }

        [TestMethod]
        public void ToDTO_ValidUsuario_DoesNotIncludePassword()
        {
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "João",
                Email = "joao@email.com",
                Password = "secretHash",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var result = UsuarioMapper.ToDTO(usuario);

            var properties = result.GetType().GetProperties();
            var hasPasswordProperty = Array.Exists(properties, p => p.Name == "Password");

            Assert.IsFalse(hasPasswordProperty);
        }
    }
}
