using EstacionamentoSenac.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EstacionamentoSenac.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
    }
}
