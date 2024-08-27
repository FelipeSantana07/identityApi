using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IdentityApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Config
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<ProdutoModel> Produto { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseNpgsql(ObterStringConexao());
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=API_IDENTITY;Username=user;Password=123");
                base.OnConfiguring(optionsBuilder);
            }
        }

        //public string ObterStringConexao()
        //{
        //    return "Host=localhost;Port=5432;Database=API_IDENTITY;Username=user;Password=123";
        //}
    }
}
