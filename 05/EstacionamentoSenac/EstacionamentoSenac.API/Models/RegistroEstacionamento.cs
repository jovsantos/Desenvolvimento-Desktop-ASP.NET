using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstacionamentoSenac.API.Models
{
    [Table("RegistrosEstacionamento")]
    public class RegistroEstacionamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }

        [Required]
        public int VagaId { get; set; }
        public Vaga Vaga { get; set; }

        [Required]
        public DateTime DataHoraEntrada { get; set; }

        public DateTime? DataHoraSaida { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorFinal { get; set; }
    }
}
