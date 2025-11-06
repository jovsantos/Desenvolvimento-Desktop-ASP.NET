using Microsoft.EntityFrameworkCore;
using NovoEstacionamernto.Models;

namespace NovoEstacionamernto.Data
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Servico> Servicos { get; set; }

    }
}
