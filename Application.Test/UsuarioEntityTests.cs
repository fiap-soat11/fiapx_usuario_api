using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System;

namespace Application.Test.Domain
{
    [TestClass]
    public class UsuarioEntityTests
    {
        [TestMethod]
        public void Usuario_CanBeCreated()
        {
            var usuario = new Usuario();

            Assert.IsNotNull(usuario);
        }

        [TestMethod]
        public void Usuario_PropertiesCanBeSet()
        {
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "João Silva",
                Email = "joao@email.com",
                Password = "hashedPassword",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            Assert.AreEqual(1, usuario.Id);
            Assert.AreEqual("João Silva", usuario.Nome);
            Assert.AreEqual("joao@email.com", usuario.Email);
            Assert.AreEqual("hashedPassword", usuario.Password);
            Assert.IsNotNull(usuario.CreatedAt);
            Assert.IsNotNull(usuario.UpdatedAt);
        }

        [TestMethod]
        public void Usuario_PropertiesCanBeModified()
        {
            var usuario = new Usuario
            {
                Nome = "Nome Original",
                Email = "original@email.com"
            };

            usuario.Nome = "Nome Modificado";
            usuario.Email = "modificado@email.com";

            Assert.AreEqual("Nome Modificado", usuario.Nome);
            Assert.AreEqual("modificado@email.com", usuario.Email);
        }

        [TestMethod]
        public void Usuario_EmailCanBeNull()
        {
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "João",
                Email = null,
                Password = "hash"
            };

            Assert.IsNull(usuario.Email);
        }

        [TestMethod]
        public void Usuario_TimestampsCanBeSet()
        {
            var createdAt = DateTime.Now.AddDays(-1);
            var updatedAt = DateTime.Now;

            var usuario = new Usuario
            {
                CreatedAt = createdAt,
                UpdatedAt = updatedAt
            };

            Assert.AreEqual(createdAt, usuario.CreatedAt);
            Assert.AreEqual(updatedAt, usuario.UpdatedAt);
        }
    }
}
