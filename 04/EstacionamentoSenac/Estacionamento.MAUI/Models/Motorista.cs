namespace Estacionamento.MAUI.Models
{
    public class Motorista
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? VeiculoId { get; set; }

        public Veiculo? Veiculo { get; set; }
    }
}
