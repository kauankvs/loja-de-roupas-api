using LojaDeRoupasAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace LojaDeRoupasAPI.Context
{
    public class LojaContext: DbContext
    {
        public LojaContext(DbContextOptions<LojaContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usuario>().ToTable("Usuario");
            builder.Entity<Estoque>().ToTable("Estoque");
            builder.Entity<Produto>().ToTable("Produto");
        }
    }
}
