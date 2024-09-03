using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.ViewModels;

public class RegisterUserViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
    public string? Email { get; set; }

    
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
    public string? Senha { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
    public string? ConfirmarSenha { get; set; }
}