using System.ComponentModel.DataAnnotations;

public class AvaliacaoCreateDto
{
    [Required]
    [Range(1, 5)]
    public int Nota { get; set; }

    [MaxLength(300)]
    public string? Comentario { get; set; }

    [Required]
    public int ProfissionalId { get; set; }
}