using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces.Repositories;

public interface ITarefaRepository
{
    public Task<Tarefa?> ObterTarefa(string id, string usuarioId);
    public Task<IEnumerable<Tarefa>> ObterTarefas(string usuarioId);
    public Task AtualizarTarefa(Tarefa tarefa, Tarefa tarefaExistente);
    public Task ExcluirTarefa(Tarefa tarefa);
    public Task<Tarefa> InserirTarefa(Tarefa tarefa);
    public bool ValidarContextoExistente();
    public Task<IdentityUser?> ObterUsuarioExistente(string usuarioId);
}