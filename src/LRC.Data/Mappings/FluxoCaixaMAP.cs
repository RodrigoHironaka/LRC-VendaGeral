using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRC.Business.Entidades;

namespace LRC.Data.Mappings
{
    public class FluxoCaixaMAP : IEntityTypeConfiguration<FluxoCaixa>
    {
        public void Configure(EntityTypeBuilder<FluxoCaixa> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Descricao).IsRequired().HasColumnType("varchar(200)");
            builder.Property(x => x.DataCadastro).IsRequired();
            builder.Property(x => x.DataAlteracao);
            builder.Property(x => x.UsuarioCadastroId);
            builder.Property(x => x.UsuarioAlteracaoId);
            builder.Property(x => x.Valor).HasPrecision(10, 5);
            builder.Property(x => x.Data);
            builder.Property(x => x.DebitoCredito).HasConversion<Int32>();
            builder.HasOne(x => x.FormaPagamento).WithMany(x => x.FluxosCaixa).HasForeignKey(x => x.FormaPagamentoId);
            builder.HasOne(x => x.Caixa).WithMany(x => x.FluxosCaixa).HasForeignKey(x => x.CaixaId);
            builder.ToTable("FluxosCaixa");
        }
    }
}