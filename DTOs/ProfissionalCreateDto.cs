namespace TrampoLocal.API.DTOs
{
    public class ProfissionalCreateDto
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Cidade { get; set; }
        public int CategoriaId { get; set; }
        public string Descricao { get; set; }
    }
}