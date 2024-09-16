using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Services;
using Moq;

namespace Testes.Services;

public class TarefaServiceTestes
{
    private readonly Mock<ITarefaRepository> _repositoryMock;
    private readonly TarefaService _service;
    public TarefaServiceTestes()
    {
        _repositoryMock = new Mock<ITarefaRepository>();
        _service = new TarefaService(_repositoryMock.Object);
    }

    [Fact]
    public async Task TarefaService_ObterTarefas_DeveRetornarTarefas()
    {
        //Arrange
        var listaTarefasTeste = new List<Tarefa>() 
        {
            new Tarefa (
                id: Guid.NewGuid().ToString(),
                usuarioId: "1",
                titulo: "Tarefa Teste",
                descricao: "Realizando teste de tarefa",
                prioridade: Domain.Enums.PrioridadeTarefa.Baixa,
                status: Domain.Enums.StatusTarefa.NaoIniciada,
                previstaPara: DateTime.Now,
                concluidaEm: null
            )
        };

        _repositoryMock.Setup(r => r.ObterUsuarioExistente(It.IsAny<string>()))
            .ReturnsAsync(new Microsoft.AspNetCore.Identity.IdentityUser());
        
        _repositoryMock.Setup(r => r.ObterTarefas(It.IsAny<string>()))
            .ReturnsAsync(listaTarefasTeste);

        //Act
        var tarefas = await _service.ObterTarefas("usuarioIdTeste");

        //Assert
        Assert.IsAssignableFrom<IEnumerable<Tarefa>>(tarefas);
        Assert.NotNull(tarefas);
        Assert.NotEmpty(tarefas);
        Assert.Equal(listaTarefasTeste[0], tarefas.FirstOrDefault());
    }


}