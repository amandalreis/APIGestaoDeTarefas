using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _repository;
    public TarefaService(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Tarefa>> ObterTarefas(string usuarioId)
    {
        if (await ObterUsuarioExistente(usuarioId) is null) throw new  NotFoundException("O usuário não existe.");

        return await _repository.ObterTarefas(usuarioId);
    }

    public async Task<Tarefa> ObterTarefa(string id, string usuarioId)
    {
        if (await ObterUsuarioExistente(usuarioId) is null) throw new NotFoundException("O usuário não existe.");

        var tarefa = await ObterTarefaNullable(id, usuarioId) ?? throw new NotFoundException("Não existe nenhuma tarefa com o id informado pertencente ao usuário logado.");

        return tarefa;
    }

    public async Task<Tarefa> InserirTarefa(Tarefa tarefa)
    {
        ValidarTarefa(tarefa);

        if (await ObterUsuarioExistente(tarefa.UsuarioId!) is null) throw new NotFoundException("O usuário não existe.");

        return await _repository.InserirTarefa(tarefa);
    }

    public async Task AtualizarTarefa(Tarefa tarefa, string usuarioId)
    {
        ValidarTarefa(tarefa);

        if (await ObterUsuarioExistente(usuarioId) is null) throw new NotFoundException("O usuário não existe.");

        if (tarefa.UsuarioId != usuarioId) throw new UnauthorizedAccessException("Usuário não autorizado: não é possível atualizar uma tarefa de outro usuário.");

        var tarefaExistente = await ObterTarefaNullable(tarefa.Id, usuarioId) ?? throw new NotFoundException("Não existe nenhuma tarefa com o id informado pertencente ao usuário logado.");

        await _repository.AtualizarTarefa(tarefa, tarefaExistente);
        return;
    }

    public async Task ExcluirTarefa(string id, string usuarioId)
    {
        if (await ObterUsuarioExistente(usuarioId) is null) throw new  NotFoundException("O usuário não existe.");

        var tarefa = await ObterTarefaNullable(id, usuarioId) ?? throw new NotFoundException("Não existe nenhuma tarefa com o id informado pertencente ao usuário logado.");

        await _repository.ExcluirTarefa(tarefa);
        return;
    }

    public bool ValidarContextoExistente()
    {
        return _repository.ValidarContextoExistente();
    }

    private async Task<Tarefa?> ObterTarefaNullable(string id, string usuarioId)
    {
        return await _repository.ObterTarefa(id, usuarioId);
    }

    private async Task<IdentityUser?> ObterUsuarioExistente(string usuarioId)
    {
        return await _repository.ObterUsuarioExistente(usuarioId);
    }

    private void ValidarTarefa(Tarefa tarefa)
    {
        if ((tarefa.Status == StatusTarefa.Concluida && tarefa.ConcluidaEm is null) ||
            (tarefa.ConcluidaEm != null && tarefa.Status != StatusTarefa.Concluida))
        {
            throw new BadRequestException("Tarefas concluídas devem possuir a propriedade Status com o valor 'Concluída' e a propriedade ConcluidaEm não nula.");
        }
    }
}
