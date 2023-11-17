using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using e_AgendaMedica.Dominio.ModuloAtividade;

namespace e_AgendaMedica.Infra.Orm.ModuloAtividade
{
    public class MapeadorAtividadeOrm : IEntityTypeConfiguration<Atividade>
    {
        public void Configure(EntityTypeBuilder<Atividade> builder)
        {
            builder.ToTable("TBAtividade");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Paciente).HasColumnType("varchar(150)").IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.HorarioTermino).IsRequired();
            builder.Property(x => x.HorarioInicio).IsRequired();
            builder.Property(x => x.TipoAtividade).HasConversion<int>().IsRequired();

            builder.HasMany(a => a.ListaMedicos)
                .WithMany(m => m.ListaAtividades)
                .UsingEntity(j => j.ToTable("TBAtividades_TBMedicos"));

        }
    }
}
