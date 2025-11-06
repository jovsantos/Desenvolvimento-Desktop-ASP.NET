using CadastroDeUsuario.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeUsuario.Data
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
        {
            
        }
        public DbSet<Usuario> Usuarios { get; set; }
    }

}
