using Domain.Entities.ViewModels;
using Domain.Intefaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repository;
    public AuthService(IAuthRepository repository)
    {
        _repository = repository;
    }

    public async Task<object> RegistrarUsuario(RegisterUserViewModel registerUser)
    {
        return await _repository.RegistrarUsuario(registerUser);
    }

    public async Task<object> LoginUsuario(LoginUserViewModel loginUser)
    {
        return await _repository.LoginUsuario(loginUser);
    }

    public bool ValidarContextoExistente()
    {
        return _repository.ValidarContextoExistente();
    }
}