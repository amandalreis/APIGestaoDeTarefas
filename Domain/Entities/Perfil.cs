using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Perfil
{
    [Key]
    public string? Id { get; set; }

    [ForeignKey("Usuario")]
    public string UsuarioId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(250)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public DateOnly DataNascimento { get; set; }

    public Perfil(string usuarioId, string nome, DateOnly dataNascimento)
    {
        Id = Guid.NewGuid().ToString();
        UsuarioId = usuarioId;
        Nome = nome;
        DataNascimento = dataNascimento;
    }
}
