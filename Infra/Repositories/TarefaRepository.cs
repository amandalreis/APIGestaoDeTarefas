using System.Runtime.CompilerServices;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly ApiDbContext _context;
    public TarefaRepository(ApiDbContext context)
    {
        _context = context;
    }

    public async Task AtualizarTarefa(Tarefa tarefa, Tarefa tarefaExistente)
    {
        _context.Entry(tarefaExistente).State = EntityState.Detached;
        _context.Tarefas.Update(tarefa);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao atualizar tarefa: {ex.Message}");
        }

        return;
    }

    public async Task ExcluirTarefa(Tarefa tarefa)
    {
        _context.Tarefas.Remove(tarefa);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao excluir tarefa: {ex.Message}");
        }

        return;
    }

    public async Task<Tarefa> InserirTarefa(Tarefa tarefa)
    {
        _context.Tarefas.Add(tarefa);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao inserir tarefa: {ex.Message}");
        }

        return tarefa;
    }

    public async Task<Tarefa?> ObterTarefa(string id, string usuarioId)
    {
        var tarefa = await _context.Tarefas.Where(t => t.Id == id && t.UsuarioId == usuarioId).FirstOrDefaultAsync();

        return tarefa;
    }

    public async Task<IEnumerable<Tarefa>> ObterTarefas(string usuarioId)
    {
        return await _context.Tarefas.Where(t => t.UsuarioId == usuarioId).ToListAsync();
    }

    public async Task<IdentityUser?> ObterUsuarioExistente(string usuarioId)
    {
        return await _context.Users.FindAsync(usuarioId);
    }

    public bool ValidarContextoExistente()
    {
        return _context.Tarefas != null;
    }
}
