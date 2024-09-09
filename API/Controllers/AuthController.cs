using Domain.Entities;
using Domain.Entities.ViewModels;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("registrar")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> RegistrarUsuario(RegisterUserViewModel registerUserViewModel)
    {
        if(!_service.ValidarContextoExistente()) return Problem("Erro ao registrar usuário. Contate o suporte!");

        object token;

        try
        {
            token = await _service.RegistrarUsuario(registerUserViewModel);
        } 
        catch(BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(token);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("atribuir-admin/{email}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> AtribuirAdminAoUsuario(string email)
    {
        if(!_service.ValidarContextoExistente()) return Problem("Erro ao atualizar usuário. Contate o suporte!");

        try
        {
            await _service.AtribuirAdminAoUsuario(email);
        }
        catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }

        return NoContent();
    }

    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("login")]
    public async Task<ActionResult<object>> LoginUsuario(LoginUserViewModel loginUser)
    {
        var token = await _service.LoginUsuario(loginUser);

        return Ok(token);
    }
}