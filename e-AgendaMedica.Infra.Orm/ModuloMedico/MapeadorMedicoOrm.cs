using e_AgendaMedica.Dominio.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_AgendaMedica.Infra.Orm.ModuloMedico
{
    public class MapeadorMedicoOrm : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("TBMedico");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Nome).HasColumnType("varchar(150)").IsRequired();
            builder.Property(x => x.Especialidade).HasColumnType("varchar(150)").IsRequired(required: false);
            builder.Property(x => x.CRM).HasColumnType("varchar(20)").IsRequired(required: false);

            builder.HasMany(x => x.ListaAtividades);

            builder.HasOne(a => a.Usuario)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
