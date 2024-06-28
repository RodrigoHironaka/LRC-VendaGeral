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
    public class ProdutoMAP : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DataCadastro).IsRequired();
            builder.Property(x => x.DataAlteracao);
            builder.Property(x => x.UsuarioCadastroId).IsRequired();
            builder.Property(x => x.UsuarioAlteracaoId);
            builder.Property(x => x.Situacao).HasConversion<Int32>();
            builder.Property(x => x.UnidadeMedida).HasConversion<Int32>();
            builder.Property(x => x.Nome).IsRequired().HasColumnType("varchar(200)");
            builder.Property(x => x.Descricao).IsRequired().HasColumnType("varchar(8000)");
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Quantidade).HasPrecision(10,3).IsRequired();
            builder.HasOne(x => x.Subgrupo).WithMany(x => x.Produtos).HasForeignKey(x => x.SubgrupoId);
            builder.ToTable("Produtos");
        }
    }
}
