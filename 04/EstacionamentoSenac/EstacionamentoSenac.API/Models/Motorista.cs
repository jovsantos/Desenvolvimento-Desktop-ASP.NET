using System.Text.Json.Serialization;

namespace EstacionamentoSenac.API.Models
{
    public class Motorista
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? VeiculoId { get; set; } // Chave estrangeira

        //[JsonIgnore]
        public Veiculo? Veiculo { get; set; } // Propriedade de navegação
    }
}
