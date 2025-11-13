using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstacionamentoSenac.API.Models
{
    [Table("Veiculos")]
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A placa é obrigatória.")]
        [StringLength(10, ErrorMessage = "A placa deve ter no máximo 10 caracteres.")]
        public string Placa { get; set; }

        [StringLength(50, ErrorMessage = "O modelo deve ter no máximo 50 caracteres.")]
        public string? Modelo { get; set; }

        [StringLength(30, ErrorMessage = "A cor deve ter no máximo 30 caracteres.")]
        public string? Cor { get; set; }

        [Required]
        public int MotoristaId { get; set; }
        public Motorista? Motorista { get; set; }
    }
}
