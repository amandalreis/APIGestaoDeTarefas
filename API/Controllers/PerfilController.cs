using Domain.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.Entities.ViewModels;

namespace API.Controllers;

[ApiController]
[Route("api/perfil")]
[Authorize]
public class PerfilController : ControllerBase
{
    private readonly IPerfilService _service;
    public PerfilController(IPerfilService service)
    {
        _service = service;
    }

    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Tarefa>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Perfil>> ObterPerfil()
    {
        if(!_service.ValidarContextoExistente()) return NotFound();
        var usuarioId = GetUserId();

        Perfil perfil;

        try 
        {
            perfil = await _service.ObterPerfil(usuarioId);
        }
        catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }

        return Ok(perfil);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Perfil), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Perfil>> InserirPerfil(PerfilViewModel perfilViewModel)
    {
        if(!_service.ValidarContextoExistente()) return Problem("Erro ao inserir perfil. Contate o suporte!");

        var usuarioId = GetUserId();

        Perfil perfil;

        try
        {
            perfil = await _service.InserirPerfil(perfilViewModel, usuarioId);
        } 
        catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch(ConflictException ex)
        {
            return Conflict(ex.Message);
        }

        return CreatedAtAction(nameof(ObterPerfil), perfil);
    }

    [HttpPut]
    public async Task<ActionResult> AtualizarPerfil(Perfil perfil)
    {
        if(!_service.ValidarContextoExistente()) return NotFound();

        var usuarioId = GetUserId();
        try 
        {
            await _service.AtualizarPerfil(perfil, usuarioId);
        }
        catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }

        return NoContent();
    }
}