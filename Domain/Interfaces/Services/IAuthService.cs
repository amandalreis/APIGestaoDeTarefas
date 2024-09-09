using Domain.Entities.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces.Services;

public interface IAuthService 
{
    public Task<object> RegistrarUsuario(RegisterUserViewModel registerUser);
    public Task AtribuirAdminAoUsuario(string email);
    public Task<object> LoginUsuario(LoginUserViewModel loginUser);
    public bool ValidarContextoExistente();
}