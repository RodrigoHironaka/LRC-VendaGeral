﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRC.Business.Entidades;

namespace LRC.Data.Mappings
{
    public class ContaReceberMAP : IEntityTypeConfiguration<ContaReceber>
    {
        public void Configure(EntityTypeBuilder<ContaReceber> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DataCadastro).IsRequired();
            builder.Property(x => x.DataAlteracao);
            builder.Property(x => x.UsuarioCadastroId);
            builder.Property(x => x.UsuarioAlteracaoId);
            builder.Property(x => x.Descricao).IsRequired().HasColumnType("varchar(200)");
            builder.Property(x => x.DataEmissao).IsRequired();
            builder.Property(x => x.DataFechamento);
            builder.Property(x => x.DataVencimento);
            builder.Property(x => x.Valor).HasPrecision(10, 5).IsRequired();
            builder.Property(x => x.NumeroDocumento).HasColumnType("varchar(20)");
            builder.Property(x => x.Observacao).HasColumnType("varchar(8000)");
            builder.Property(x => x.Situacao).HasConversion<Int32>();
            builder.HasOne(x => x.Cliente).WithMany(x => x.ContasReceber).HasForeignKey(x => x.ClienteId);
            builder.HasMany(x => x.Parcelas).WithOne(x => x.ContaReceber).HasForeignKey(x => x.ContaReceberId);

            builder.ToTable("ContasReceber");
        }
    }
}
