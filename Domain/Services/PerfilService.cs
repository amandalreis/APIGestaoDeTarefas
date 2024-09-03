using Domain.Entities;
using Domain.Entities.ViewModels;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services;

public class PerfilService : IPerfilService
{
    readonly IPerfilRepository _repository;
    public PerfilService(IPerfilRepository repository)
    {
        _repository = repository;
    }

    public async Task<Perfil> InserirPerfil(PerfilViewModel perfilViewModel, string usuarioId)
    {
        if(await ObterUsuarioExistente(usuarioId) is null) throw new NotFoundException("O usuário não existe.");

        var perfilExistente = await ObterPerfilNullable(usuarioId);
        if (perfilExistente is not null) throw new ConflictException("O usuário já possui um perfil. Não é possível criar mais de um perfil para o mesmo usuário.");

        var perfil = new Perfil(usuarioId, perfilViewModel.Nome!, perfilViewModel.DataNascimento);

        return await _repository.InserirPerfil(perfil);
    }

    public async Task<Perfil> ObterPerfil(string usuarioId)
    {
        if (await ObterUsuarioExistente(usuarioId) is null) throw new NotFoundException("O usuário não existe.");

        var perfil = await ObterPerfilNullable(usuarioId) ?? throw new NotFoundException("Não existe nenhum perfil pertencente ao usuário logado.");
    
        return perfil;
    }

    public async Task AtualizarPerfil(Perfil perfil, string usuarioId)
    {
        if (await ObterUsuarioExistente(usuarioId) is null) throw new NotFoundException("O usuário não existe.");

        if (perfil.UsuarioId != usuarioId) throw new UnauthorizedAccessException("Usuário não autorizado: não é possível atualizar um perfil de outro usuário.");

        var perfilExistente = await ObterPerfilNullable(usuarioId) ?? throw new NotFoundException("Não existe nenhum perfil pertencente ao usuário logado.");

        if (perfilExistente.Id != perfil.Id) throw new UnauthorizedAccessException("Não há perfil com o id informado pertencente ao usuário logado.");

        await _repository.AtualizarPerfil(perfil, perfilExistente);
        return;
    }

    public bool ValidarContextoExistente()
    {
        return _repository.ValidarContextoExistente();
    }

    private async Task<IdentityUser?> ObterUsuarioExistente(string usuarioId)
    {
        return await _repository.ObterUsuarioExistente(usuarioId);
    }

    private async Task<Perfil?> ObterPerfilNullable(string usuarioId)
    {
        return await _repository.ObterPerfil(usuarioId);
    }
}