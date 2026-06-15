using Microsoft.EntityFrameworkCore;
using Produtos.Models;

namespace Produtos.Data
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options) { }
            
        public DbSet<Usuario>Usuarios { get; set; }
        public DbSet<Produto>Produtos {  get; set; }
    }
}
