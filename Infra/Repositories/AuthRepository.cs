using Domain.Entities;
using Infra.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Domain.Intefaces.Repositories;
using Domain.Entities.ViewModels;
using System.Security.Claims;

namespace Infra.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly ApiDbContext _context;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public AuthRepository(ApiDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<object> RegistrarUsuario(RegisterUserViewModel registerUser)
    {
        var usuario = new IdentityUser()
        {
            Email = registerUser.Email,
            UserName = registerUser.Email,
            EmailConfirmed = true
        };

        var resultadoCriacaoUsuario = await _userManager.CreateAsync(usuario, registerUser.Senha!);

        if (resultadoCriacaoUsuario.Succeeded)
        {
            await _signInManager.SignInAsync(usuario, false);
            return await GerarJwt(usuario.Id);
        }

        var erro = resultadoCriacaoUsuario.Errors.FirstOrDefault()!;
        if(erro.Code.Contains("Password")) throw new BadRequestException(erro.Description);
        throw new Exception($"Ocorreu um erro ao registrar usuário: {erro.Description}.");
    }

    public async Task<object> LoginUsuario(LoginUserViewModel loginUser)
    {
        var resultado = await _signInManager.PasswordSignInAsync(loginUser.Email!, loginUser.Senha!, false, true);

        if (resultado.Succeeded)
        {
            var usuario = await _userManager.FindByEmailAsync(loginUser.Email!) ?? throw new NotFoundException("O usuário não existe.");;

            var token = await GerarJwt(usuario.Id);
            return token;
        }

        throw new UnauthorizedAccessException("Credenciais inválidas.");
    }

    public async Task AtribuirAdminAoUsuario(IdentityUser usuario)
    {
        var resultadoAtribuirRoleAdmin = await _userManager.AddToRoleAsync(usuario, "Admin");
        if(!resultadoAtribuirRoleAdmin.Succeeded) throw new Exception($"Ocorreu um erro ao atribuir role admin ao usuário: {resultadoAtribuirRoleAdmin.Errors.FirstOrDefault()}");
    }

    public async Task<IdentityUser?> ObterUsuarioExistente(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> ValidarUsuarioAdmin(IdentityUser usuario)
    {
        var roles = await _userManager.GetRolesAsync(usuario);
        if(roles.Contains("Admin")) return true;
        return false;
    }

    public bool ValidarContextoExistente()
    {
        return _context.Users != null;
    }

    private async Task<object> GerarJwt(string usuarioId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo!);
        var usuario = await _userManager.FindByIdAsync(usuarioId);
        var roles = await _userManager.GetRolesAsync(usuario!);
        
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, usuarioId),
        };

        foreach(var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor 
        {
            Issuer = _jwtSettings.Emissor,
            Audience = _jwtSettings.Audiencia,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims)
        });

        var encodedToken = tokenHandler.WriteToken(token);

        return new {token = encodedToken};
    }
}