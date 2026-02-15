using Application.Configurations;
using Domain;
using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class UsuarioUseCase : IUsuarioUseCase
    {
        public async Task<Usuario> AtualizarUsuario(Usuario entity, Usuario novoUsuario)
        {
            if(entity == null || novoUsuario == null)
            {
                throw new ArgumentNullException("Usuario ou novo usuario não podem ser nulos.");
            }
            if (entity.Email != novoUsuario.Email)
            {
                throw new ArgumentException("Email do usuario não pode ser alterado.");
            }

            if (!string.IsNullOrEmpty(novoUsuario.Password) && entity.Password != novoUsuario.Password)
            {
                entity.Password = CriptografarSenha(novoUsuario.Password);
            }

            entity.Nome = novoUsuario.Nome;
            entity.Email = novoUsuario.Email;
            entity.UpdatedAt = DateTime.UtcNow;
            return entity;
        }

        public async Task<Usuario> ExcluirUsuario(Usuario entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("Usuario não pode ser nulo.");
            }
            if (string.IsNullOrEmpty(entity.Email))
            {
                throw new ArgumentException("Email do usuario não pode ser nulo ou vazio.");
            }

            return entity;
        }

        public async Task<Usuario> IncluirUsuario(Usuario novoUsuario)
        {
            if (novoUsuario == null)
            {
                throw new ArgumentNullException("Novo usuario não pode ser nulo.");
            }
            if (string.IsNullOrEmpty(novoUsuario.Password))
            {
                throw new ArgumentException("Senha do usuario não pode ser nulo ou vazio.");
            }
            if (string.IsNullOrEmpty(novoUsuario.Nome))
            {
                throw new ArgumentException("Nome do usuario não pode ser nulo ou vazio.");
            }
            if (string.IsNullOrEmpty(novoUsuario.Email))
            {
                throw new ArgumentException("Email do usuario não pode ser nulo ou vazio.");
            }

            novoUsuario.Password = CriptografarSenha(novoUsuario.Password);

            return novoUsuario;
        }


        private string CriptografarSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha, workFactor: 12);
        }

        public bool VerificarSenha(string senha, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }

    }
}
