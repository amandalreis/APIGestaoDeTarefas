namespace Domain.Entities;

public class JwtSettings
{
    public string? Segredo { get; set; }
    public int ExpiracaoHoras { get; set; }
    public string? Emissor { get; set; }
    public string? Audiencia { get; set; }
}