using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LRC.App.ViewModels;

namespace LRC.App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LRC.App.ViewModels.ClienteVM> ClienteVM { get; set; } = default!;
        public DbSet<LRC.App.ViewModels.EnderecoVM> EnderecoVM { get; set; } = default!;
        public DbSet<LRC.App.ViewModels.ColaboradorVM> ColaboradorVM { get; set; } = default!;
        public DbSet<LRC.App.ViewModels.EntregadorVM> EntregadorVM { get; set; } = default!;
        public DbSet<LRC.App.ViewModels.FornecedorVM> FornecedorVM { get; set; } = default!;
        public DbSet<LRC.App.ViewModels.FormaPagamentoVM> FormaPagamentoVM { get; set; } = default!;
        public DbSet<LRC.App.ViewModels.ContaPagarVM> ContaPagarVM { get; set; } = default!;
        public DbSet<LRC.App.ViewModels.ContaReceberVM> ContaReceberVM { get; set; } = default!;
        public DbSet<LRC.App.ViewModels.CaixaVM> CaixaVM { get; set; } = default!;
        public DbSet<LRC.App.ViewModels.FluxoCaixaVM> FluxoCaixaVM { get; set; } = default!;
    }
}
