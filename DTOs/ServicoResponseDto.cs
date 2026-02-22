public class ServicoResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal? Preco { get; set; }
    public int ProfissionalId { get; set; }
    public string ProfissionalNome { get; set; }
}