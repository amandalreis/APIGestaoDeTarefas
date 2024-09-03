using Domain.Entities.ViewModels;

namespace Domain.Interfaces.Services;

public interface IAuthService 
{
    public Task<object> RegistrarUsuario(RegisterUserViewModel registerUser);
    public Task<object> LoginUsuario(LoginUserViewModel loginUser);
    public bool ValidarContextoExistente();
}