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
    }
}
