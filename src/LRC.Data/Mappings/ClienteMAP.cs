using LRC.Business.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LRC.Data.Mappings
{
    public class ClienteMAP : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //verificar relacionamentos

            builder.HasKey(x => x.Id);
            builder.Property(x => x.DataCadastro).IsRequired();
            builder.Property(x => x.DataAlteracao);
            builder.Property(x => x.UsuarioCadastroId);
            builder.Property(x => x.UsuarioAlteracaoId);
            builder.Property(x => x.RazaoSocial).IsRequired().HasColumnType("varchar(200)");
            builder.Property(x => x.NomeFantasia).IsRequired().HasColumnType("varchar(200)");
            builder.Property(x => x.Nascimento);
            builder.Property(x => x.Documento);
            builder.Property(x => x.Documento2).IsRequired().HasColumnType("varchar(20)");
            builder.Property(x => x.Telefone).IsRequired().HasColumnType("varchar(20)");
            builder.Property(x => x.Celular).IsRequired().HasColumnType("varchar(20)");
            builder.Property(x => x.Celular2).IsRequired().HasColumnType("varchar(20)");
            builder.Property(x => x.Email).IsRequired().HasColumnType("varchar(100)");
            builder.Property(x => x.TipoPessoa).HasConversion<Int32>(); 
            builder.Property(x => x.Responsaveis).IsRequired().HasColumnType("varchar(200)");
            //builder.HasMany(x => x.Enderecos).WithOne().HasForeignKey("ClienteId").HasPrincipalKey("Id").OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasMany(x => x.Enderecos).WithOne(x => x.Cliente).HasForeignKey(x => x.ClienteId);
            builder.ToTable("Clientes");
        }
    }
}
