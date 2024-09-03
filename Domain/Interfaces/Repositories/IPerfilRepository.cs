using Domain.Entities;
using Domain.Entities.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces.Repositories;

public interface IPerfilRepository
{
    public Task<Perfil?> ObterPerfil(string usuarioId);
    public Task<Perfil> InserirPerfil(Perfil perfil);
    public Task AtualizarPerfil(Perfil perfil, Perfil perfilExistente);
    public bool ValidarContextoExistente();
    public Task<IdentityUser?> ObterUsuarioExistente(string usuarioId);
}