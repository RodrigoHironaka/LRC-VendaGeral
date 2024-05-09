using LRC.Business.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            builder.Property(x => x.Nome).IsRequired().HasColumnType("varchar(200)");
            builder.HasOne(x => x.Grupo).WithOne(x => x.Subgrupo);
            builder.ToTable("SubGrupos");
        }
    }
}
