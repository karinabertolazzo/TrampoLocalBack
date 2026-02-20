using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrampoLocal.API.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int Nota { get; set; }

        [MaxLength(300)]
        public string? Comentario { get; set; }

        [ForeignKey("Profissional")]
        public int ProfissionalId { get; set; }

        public Profissional Profissional { get; set; }
    }
}