namespace NovoEstacionamernto.Models
{
    internal class Veiculo
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Cores { get; set; }


        // Propriedade de navegação para o Cliente

        public Cliente Cliente { get; set; }

    }
}
