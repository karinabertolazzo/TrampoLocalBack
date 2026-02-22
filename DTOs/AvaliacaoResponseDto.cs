public class AvaliacaoResponseDto
{
    public int Id { get; set; }
    public int Nota { get; set; }
    public string? Comentario { get; set; }
    public int ProfissionalId { get; set; }
    public string ProfissionalNome { get; set; } // só o nome, evita ciclo
}