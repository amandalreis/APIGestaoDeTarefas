using Domain.Entities;
using Domain.Entities.ViewModels;
using Domain.Interfaces.Services;
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

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> RegistrarUsuario(RegisterUserViewModel registerUser)
    {
        if(!_service.ValidarContextoExistente()) return Problem("Erro ao registrar usuário. Contate o suporte!");

        object token;

        try
        {
            token = await _service.RegistrarUsuario(registerUser);
        } 
        catch(BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<ActionResult<object>> LoginUsuario(LoginUserViewModel loginUser)
    {
        var token = await _service.LoginUsuario(loginUser);

        return Ok(token);
    }
}