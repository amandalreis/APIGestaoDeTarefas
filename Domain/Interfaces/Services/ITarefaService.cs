using Domain.Entities;


namespace Domain.Interfaces.Services;

public interface ITarefaService
{
    public Task<Tarefa> ObterTarefa(string id, string usuarioId);
    public Task<IEnumerable<Tarefa>> ObterTarefas(string usuarioId);
    public Task AtualizarTarefa(Tarefa tarefa, string usuarioId);
    public Task ExcluirTarefa(string id, string usuarioId);
    public Task<Tarefa> InserirTarefa(Tarefa tarefa);
    public bool ValidarContextoExistente();
}