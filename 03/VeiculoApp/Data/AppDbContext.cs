using Microsoft.EntityFrameworkCore;
using VeiculoApp.Models;

namespace VeiculoApp.Data
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
