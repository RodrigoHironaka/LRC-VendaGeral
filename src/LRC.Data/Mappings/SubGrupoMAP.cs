using LRC.Business.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LRC.Data.Mappings
{
    public class SubGrupoMAP : IEntityTypeConfiguration<Subgrupo>
    {
        public void Configure(EntityTypeBuilder<Subgrupo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DataCadastro).IsRequired();
            builder.Property(x => x.DataAlteracao);
            builder.Property(x => x.UsuarioCadastroId);
            builder.Property(x => x.UsuarioAlteracaoId);
            builder.Property(x => x.Situacao).HasConversion<Int32>();
            builder.Property(x => x.Nome).IsRequired().HasColumnType("varchar(200)");
            builder.HasOne(x => x.Grupo).WithMany(x => x.Subgrupos).HasForeignKey(x => x.GrupoId);
            builder.ToTable("SubGrupos");
        }
    }
}
