namespace CadastroDeUsuario.Models
{
    internal class Usuario
    {
        public int Id { get; set; }
        public string? PrimeiroNome { get; set; }
        public string? Sobrenome { get; set; }
        public DateOnly DataNascimento { get; set; }
    }
}
