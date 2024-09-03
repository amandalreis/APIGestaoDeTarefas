using Domain.Entities;
using Domain.Entities.ViewModels;

namespace Domain.Interfaces.Services;

public interface IPerfilService
{
    public Task<Perfil> ObterPerfil(string usuarioId);
    public Task<Perfil> InserirPerfil(PerfilViewModel perfilViewModel, string usuarioId);
    public Task AtualizarPerfil(Perfil perfil, string usuarioId);
    public bool ValidarContextoExistente();
}