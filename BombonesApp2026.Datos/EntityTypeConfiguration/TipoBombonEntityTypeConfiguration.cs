using BombonesApp2026.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BombonesApp2026.Datos.EntityTypeConfiguration
{
    public class TipoBombonEntityTypeConfiguration : IEntityTypeConfiguration<TipoBombon>
    {
        public void Configure(EntityTypeBuilder<TipoBombon> builder)
        {
            builder.ToTable("TipoBombones");
            builder.HasKey(tb => tb.TipoBombonId);
            builder.Property(tb => tb.RowVersion).IsRowVersion();
        }
    }
}
