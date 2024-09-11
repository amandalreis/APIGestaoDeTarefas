using Domain.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.Entities.ViewModels;

namespace API.Controllers;

[ApiController]
[Route("api/tarefas")]
[Authorize]
public class TarefaController : ControllerBase
{
    private readonly ITarefaService _service;
    public TarefaController(ITarefaService service)
    {
        _service = service;
    }

    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Tarefa>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<Tarefa>>> ObterTarefas()
    {
        if(!_service.ValidarContextoExistente()) return NotFound();
        var usuarioId = GetUserId();
        return Ok(await _service.ObterTarefas(usuarioId));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Tarefa), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Tarefa>> ObterTarefa(string id)
    {
        if(!_service.ValidarContextoExistente()) return NotFound();

        var usuarioId = GetUserId();
        Tarefa tarefa;

        try 
        {
            tarefa = await _service.ObterTarefa(id, usuarioId);
        }
        catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }

        return Ok(tarefa);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Tarefa), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Tarefa>> InserirTarefa(TarefaViewModel tarefaViewModel)
    {   
        if(!_service.ValidarContextoExistente()) return Problem("Erro ao criar tarefa. Contate o suporte!");

        var usuarioId = GetUserId(); 

        var tarefa = new Tarefa (
            id: Guid.NewGuid().ToString(),
            usuarioId: usuarioId,
            titulo: tarefaViewModel.Titulo!,
            descricao: tarefaViewModel.Descricao!,
            prioridade: tarefaViewModel.Prioridade,
            status: tarefaViewModel.Status,
            previstaPara: tarefaViewModel.PrevistaPara,
            concluidaEm: tarefaViewModel.ConcluidaEm
        );

        Tarefa novaTarefa;

        try
        {
            novaTarefa = await _service.InserirTarefa(tarefa);
        }
        catch(BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        

        return CreatedAtAction(nameof(ObterTarefa), new { id = novaTarefa.Id }, novaTarefa);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> AtualizarTarefa(string id, Tarefa tarefa)
    {
        if(!_service.ValidarContextoExistente()) return NotFound();

        if (id != tarefa.Id) return BadRequest("O id informado na URI é diferente do id informado no body da requisição.");
        
        var usuarioId = GetUserId();

        try 
        {
            await _service.AtualizarTarefa(tarefa, usuarioId);
        }
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch(BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> ExcluirTarefa(string id)
    {
        if(!_service.ValidarContextoExistente()) return NotFound();

        var usuarioId = GetUserId();

        try 
        {
            await _service.ExcluirTarefa(id, usuarioId);
        }
        catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }

        return NoContent();
    }
}