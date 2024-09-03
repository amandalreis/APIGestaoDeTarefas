using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class Tarefa
{
    [Key]
    public string Id { get; set; }

    [ForeignKey("Usuario")]
    public string? UsuarioId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(250)]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(400)]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [AllowedValues(PrioridadeTarefa.Baixa, PrioridadeTarefa.Media, PrioridadeTarefa.Alta, PrioridadeTarefa.Urgente, ErrorMessage = "A prioridade fornecida é inválida.")]
    public PrioridadeTarefa Prioridade { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [AllowedValues(StatusTarefa.NaoIniciada, StatusTarefa.EmAndamento, StatusTarefa.Pausada, StatusTarefa.Concluida, ErrorMessage = "O status fornecido é inválido.")]
    public StatusTarefa Status { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public DateTime PrevistaPara { get; set; }

    public DateTime? ConcluidaEm { get; set; }

    public Tarefa(string id, string usuarioId, string titulo, string descricao, PrioridadeTarefa prioridade, StatusTarefa status, DateTime previstaPara, DateTime? concluidaEm)
    {
        Id = id ?? Guid.NewGuid().ToString();
        UsuarioId = usuarioId;
        Titulo = titulo;
        Descricao = descricao;
        PrevistaPara = previstaPara;
        Prioridade = prioridade;
        Status = status;
        ConcluidaEm = concluidaEm;
    }
}