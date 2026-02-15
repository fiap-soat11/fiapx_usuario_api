using Application.Configurations;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Adapters.Presenters.Usuario;
using WebAPI.Mappers;
using Adapters.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Adapters.Presenters.Autenticacao;

namespace WebAPI.Controllers;

//[ApiController]
[Route("api/autenticacao")]
public class AutenticacaoControllerHandler : ControllerBase
{
    private readonly ILogger<AutenticacaoControllerHandler> _logger;
    private readonly IAutenticacaoController _autenticacaoController;

    public AutenticacaoControllerHandler(ILogger<AutenticacaoControllerHandler> logger,
                                    IAutenticacaoController autenticacaoController)
    {
        _logger = logger;
        _autenticacaoController = autenticacaoController;

    }


    [HttpPost(Name = "GerarToken")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> GerarToken([FromBody] AutenticacaoRequest autenticacao)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Chamar o controller para gerar o token
            var response = await _autenticacaoController.GerarToken(autenticacao);

            return Ok(response);
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(ex, "Falha na autenticação para o email: {Email}", autenticacao.Email);
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao gerar token");
            return StatusCode(500, new { message = "Erro interno ao processar autenticação" });
        }
    }

}
