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
    public class EnderecoMAP : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Logradouro)
                .IsRequired()
                .HasColumnType("varchar(200)");
            builder.Property(c => c.Numero)
                .IsRequired()
                .HasColumnType("varchar(50)");
            builder.Property(c => c.Referencia)
                .IsRequired()
                .HasColumnType("varchar(8)");
            builder.Property(c => c.Complemento)
                .IsRequired()
                .HasColumnType("varchar(250)");
            builder.Property(c => c.Bairro)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("Enderecos");
        }
    }
}
