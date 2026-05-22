using BombonesApp2026.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BombonesApp2026.Datos
{
    public class BombonesDbContext : DbContext
    {
        public DbSet<TipoBombon> TipoBombones { get; set; }
        public DbSet<FormaDePago> FormasDePago { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=BombonesIo2026;Integrated Security=True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BombonesDbContext).Assembly);
        }
    }
}
