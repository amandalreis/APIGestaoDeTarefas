using System.Runtime.CompilerServices;
using Domain.Intefaces.Repositories;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class Dependencias 
{
    public static void ConfigurarServices(IServiceCollection services)
    {
        //Services
        services.AddScoped<ITarefaService, TarefaService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPerfilService, PerfilService>();

        //Repositories
        services.AddScoped<ITarefaRepository, TarefaRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IPerfilRepository, PerfilRepository>();
    }
}