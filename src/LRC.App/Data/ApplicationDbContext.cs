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
        public DbSet<LRC.App.ViewModels.SubGrupoVM> SubGrupoVM { get; set; } = default!;
    }
}
