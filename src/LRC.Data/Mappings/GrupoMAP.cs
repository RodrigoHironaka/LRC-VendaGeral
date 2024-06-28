using LRC.Business.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LRC.Data.Mappings
{
    public class GrupoMAP : IEntityTypeConfiguration<Grupo>
    {
        public void Configure(EntityTypeBuilder<Grupo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DataCadastro).IsRequired();
            builder.Property(x => x.DataAlteracao);
            builder.Property(x => x.UsuarioCadastroId);
            builder.Property(x => x.UsuarioAlteracaoId);
            builder.Property(x => x.Nome).IsRequired().HasColumnType("varchar(200)");
            builder.Property(x => x.Situacao).HasConversion<Int32>();
            builder.HasMany(x => x.Subgrupos).WithOne(x => x.Grupo).HasForeignKey(x => x.GrupoId);
            builder.ToTable("Grupos");
        }
    }
}
