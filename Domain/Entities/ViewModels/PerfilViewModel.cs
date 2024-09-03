using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.ViewModels;

public class PerfilViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(250)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public DateOnly DataNascimento { get; set; }
}