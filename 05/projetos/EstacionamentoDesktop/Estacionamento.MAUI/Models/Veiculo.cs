namespace Estacionamento.MAUI.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string? Modelo { get; set; }
        public string? Cor { get; set; }

        public int MotoristaId { get; set; }
        public Motorista? Motorista { get; set; }
    }
}
