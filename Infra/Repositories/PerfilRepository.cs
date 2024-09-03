using Domain.Entities;
using Domain.Entities.ViewModels;
using Domain.Interfaces.Repositories;
using Infra.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class PerfilRepository : IPerfilRepository
{
    private readonly ApiDbContext _context;
    public PerfilRepository(ApiDbContext context)
    {
        _context = context;
    }
    public async Task<Perfil?> ObterPerfil(string usuarioId)
    {
        var perfil = await _context.Perfis.Where(p => p.UsuarioId == usuarioId).FirstOrDefaultAsync();

        return perfil;
    }

    public async Task<Perfil> InserirPerfil(Perfil perfil)
    {
        _context.Perfis.Add(perfil);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao inserir perfil: {ex.Message}");
        }

        return perfil;
    }

    public async Task AtualizarPerfil(Perfil perfil, Perfil perfilExistente)
    {
        _context.Entry(perfilExistente).State = EntityState.Detached;

        perfilExistente.Nome = perfil.Nome;
        perfilExistente.DataNascimento = perfil.DataNascimento;
        
        _context.Perfis.Update(perfilExistente);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao atualizar perfil: {ex.Message}");
        }

        return;
    }

    public async Task<IdentityUser?> ObterUsuarioExistente(string usuarioId)
    {
        return await _context.Users.FindAsync(usuarioId);
    }

    public bool ValidarContextoExistente()
    {
        return _context.Perfis != null;
    }
}