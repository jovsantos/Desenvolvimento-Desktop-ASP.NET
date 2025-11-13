namespace Estacionamento.MAUI.Models
{
    public class Vaga
    {
        public int Id { get; set; }
        public string Localizacao { get; set; } = string.Empty;
        public string? TipoVaga { get; set; }
    }
}
