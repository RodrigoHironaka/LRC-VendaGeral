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
    public class EntregadorMAP : IEntityTypeConfiguration<Entregador>
    {
        public void Configure(EntityTypeBuilder<Entregador> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DataCadastro).IsRequired();
            builder.Property(x => x.DataAlteracao);
            builder.Property(x => x.UsuarioCadastroId);
            builder.Property(x => x.UsuarioAlteracaoId);
            builder.Property(x => x.RazaoSocial).IsRequired().HasColumnType("varchar(200)");
            builder.Property(x => x.NomeFantasia).HasColumnType("varchar(200)");
            builder.Property(x => x.Nascimento);
            builder.Property(x => x.Documento);
            builder.Property(x => x.Documento2).HasColumnType("varchar(20)");
            builder.Property(x => x.Telefone).HasColumnType("varchar(20)");
            builder.Property(x => x.Celular).HasColumnType("varchar(20)");
            builder.Property(x => x.Celular2).HasColumnType("varchar(20)");
            builder.Property(x => x.Email).HasColumnType("varchar(100)");
            builder.Property(x => x.TipoPessoa).HasConversion<Int32>();
            builder.Property(x => x.Placa).HasColumnType("varchar(100)");
            builder.Property(x => x.TipoVeiculo).HasConversion<Int32>();
            builder.Property(x => x.Situacao).HasConversion<Int32>();

            builder.OwnsOne(x => x.Endereco, endereco =>
            {
                endereco.Property(e => e.Logradouro).HasMaxLength(100).HasColumnName("Logradouro");
                endereco.Property(e => e.Numero).HasMaxLength(30).HasColumnName("Numero");
                endereco.Property(e => e.Bairro).HasMaxLength(70).HasColumnName("Bairro");
                endereco.Property(e => e.Complemento).HasMaxLength(150).HasColumnName("Complemento");
                endereco.Property(e => e.Referencia).HasMaxLength(150).HasColumnName("Referencia");

            });

            builder.ToTable("Entregadores");
        }
    }
}
