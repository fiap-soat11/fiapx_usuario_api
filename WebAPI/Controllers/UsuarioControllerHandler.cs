using Application.Configurations;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Adapters.Presenters.Usuario;
using WebAPI.Mappers;
using Adapters.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/Usuario")]
public class UsuarioControllerHandler : ControllerBase
{
    private readonly ILogger<UsuarioControllerHandler> _logger;
    private readonly IUsuarioController _usuarioController;

    public UsuarioControllerHandler(ILogger<UsuarioControllerHandler> logger,
                                    IUsuarioController usuarioController)
    {
        _logger = logger;
        _usuarioController = usuarioController;

    }

    [HttpGet(Name = "ListarUsuarios")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> ListarUsuarios()
    {
        var usuarios = await _usuarioController.ListarUsuarios();
        if (usuarios == null || !usuarios.Any())
            return NotFound();
        return Ok(usuarios);
    }

    [HttpGet("Email", Name = "BuscarUsuario")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> BuscarUsuario(string email)
    {
        try
        {
            var usuario = await _usuarioController.BuscarUsuario(email);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar usuario");
            return BadRequest("Erro ao buscar usuario");
        }
        
    }

    [AllowAnonymous]
    [HttpPost(Name = "IncluirUsuario")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Post([FromBody] UsuarioRequest usuario)
    {
        try
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            await _usuarioController.IncluirUsuario(usuario);
            return Ok("Usuario incluído com sucesso");
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex, "Erro ao incluir usuario");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao incluir usuario");
            return BadRequest("Erro ao incluir usuario");
        }
    }

    [HttpPut("{email}", Name = "AtualizarUsuario")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [Consumes("application/json")]    
    public async Task<IActionResult> Put(string email, [FromBody] UsuarioRequest usuario)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _usuarioController.AtualizarUsuario(usuario);
            return Ok("Usuario atualizado com sucesso");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar usuario");
            return BadRequest("Erro ao atualizar usuario");
        }
        
    }

    [HttpDelete("{email}", Name = "ExcluirUsuario")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(string email)
    {
        try
        {
            await _usuarioController.ExcluirUsuario(email);
            return Ok("Usuario exclu�do com sucesso");
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex, "Erro ao excluir usuario");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir usuario");
            return BadRequest("Erro ao excluir usuario");
        }
        
    }



}
