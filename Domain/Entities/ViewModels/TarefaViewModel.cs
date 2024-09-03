using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities.ViewModels;

public class TarefaViewModel
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(250)]
    public string? Titulo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(500)]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [AllowedValues(PrioridadeTarefa.Baixa, PrioridadeTarefa.Media, PrioridadeTarefa.Alta, PrioridadeTarefa.Urgente, ErrorMessage = "A prioridade fornecida é inválida.")]
    public PrioridadeTarefa Prioridade { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [AllowedValues(StatusTarefa.NaoIniciada, StatusTarefa.EmAndamento, StatusTarefa.Pausada, StatusTarefa.Concluida, ErrorMessage = "O status fornecido é inválido.")]
    public StatusTarefa Status { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public DateTime PrevistaPara { get; set; }
    
    public DateTime? ConcluidaEm { get; set; }
}