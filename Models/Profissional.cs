using System.ComponentModel.DataAnnotations;

namespace TrampoLocal.API.Models
{
    public class Profissional
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(11)]
        public string CPF { get; set; }

        [Required]
        [MaxLength(20)]
        public string Telefone { get; set; }

        [Required]
        [MaxLength(100)]
        public string Cidade { get; set; }

        

        [MaxLength(500)]
        public string? Descricao { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        // Relacionamentos
        public List<Servico> Servicos { get; set; } = new();
        public List<Avaliacao> Avaliacoes { get; set; } = new();
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }


    }
}