using Domain.Entities.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Domain.Intefaces.Repositories;

public interface IAuthRepository
{
    public Task<object> RegistrarUsuario(RegisterUserViewModel registerUser);
    public Task AtribuirAdminAoUsuario(IdentityUser usuario);
    public Task<object> LoginUsuario(LoginUserViewModel loginUser);
    public Task<IdentityUser?> ObterUsuarioExistente(string email);
    public Task<bool> ValidarUsuarioAdmin(IdentityUser usuario);
    public bool ValidarContextoExistente();
}