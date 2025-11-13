using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstacionamentoSenac.API.Models
{
    [Table("Vagas")]
    public class Vaga
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A localização é obrigatória.")]
        [StringLength(50, ErrorMessage = "A localização deve ter no máximo 50 caracteres.")]
        public string Localizacao { get; set; }

        [StringLength(20, ErrorMessage = "O tipo de vaga deve ter no máximo 20 caracteres.")]
        public string? TipoVaga { get; set; }
    }
}
