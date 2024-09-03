using Domain.Entities.ViewModels;

namespace Domain.Intefaces.Repositories;

public interface IAuthRepository
{
    public Task<object> RegistrarUsuario(RegisterUserViewModel registerUser);
    public Task<object> LoginUsuario(LoginUserViewModel loginUser);
    public bool ValidarContextoExistente();
}