using Domain;
using Adapters.Presenters.Usuario;

namespace WebAPI.Mappers
{
    public class UsuarioMapper
    {
        public static Usuario ToEntity(UsuarioRequest dto)
        {
            return new Usuario
            {
                Password = dto.Password,
                Nome = dto.Nome,
                Email = dto.Email,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public static UsuarioResponse ToDTO(Usuario usuario)
        {
            return new UsuarioResponse
            {
                Nome = usuario.Nome,
                Email = usuario.Email

            };
        }
    }
}
