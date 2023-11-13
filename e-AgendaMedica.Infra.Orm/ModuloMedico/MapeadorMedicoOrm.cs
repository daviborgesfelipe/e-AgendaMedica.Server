using e_AgendaMedica.Dominio.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_AgendaMedica.Infra.Orm.ModuloMedico
{
    internal class MapeadorMedicoOrm : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("TBMedico");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Nome).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.Especialidade).HasColumnType("varchar(200)").IsRequired(required: false);
            builder.Property(x => x.CRM).HasColumnType("varchar(20)").IsRequired(required: false);
        }
    }
}
