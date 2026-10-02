using BombonesApp2026.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BombonesApp2026.Datos.EntityTypeConfiguration
{
    public class BombonEntityTypeConfiguration : IEntityTypeConfiguration<Bombon>
    {
        public void Configure(EntityTypeBuilder<Bombon> builder)
        {
            builder.ToTable("Bombones");
            builder.Property(b => b.TipoBombonId)
                .IsRequired();
            builder.Property(b => b.PesoEnGramos).IsRequired().HasColumnName("PesoGramos");
            builder.Property(b => b.TieneAzucar).IsRequired();
        }
    }
}
