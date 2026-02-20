using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrampoLocal.API.Models
{
    public class Servico
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }

        [MaxLength(500)]
        public string? Descricao { get; set; }

        public decimal? Preco { get; set; }

        // Chave estrangeira
        [ForeignKey("Profissional")]
        public int ProfissionalId { get; set; }

        public Profissional Profissional { get; set; }
    }
}