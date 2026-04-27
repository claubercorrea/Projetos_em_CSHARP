using ListaFilme.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaFilme.DATA
{
    public class FilmeContext: DbContext
    {
        public FilmeContext(DbContextOptions<FilmeContext> options) : base(options) { }
        public DbSet<Filme>Filmes { get; set; }
    }
}
