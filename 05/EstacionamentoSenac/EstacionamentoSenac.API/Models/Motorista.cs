using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstacionamentoSenac.API.Models
{
    [Table("Motoristas")]
    public class Motorista
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CPF deve ter 14 caracteres.")]
        public string Cpf { get; set; }

        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
        public string? Telefone { get; set; }

        public ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
    }
}
