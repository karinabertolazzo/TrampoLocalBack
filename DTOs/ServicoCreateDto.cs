using System.ComponentModel.DataAnnotations;

public class ServicoCreateDto
{
    [Required, MaxLength(150)]
    public string Nome { get; set; }

    [MaxLength(500)]
    public string? Descricao { get; set; }

    public decimal? Preco { get; set; }

    [Required]
    public int ProfissionalId { get; set; }
}