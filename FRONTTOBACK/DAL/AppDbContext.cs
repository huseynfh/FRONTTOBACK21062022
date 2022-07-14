using FRONTTOBACK.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FRONTTOBACK.DAL
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
      public DbSet<Slider> Slider { get; set; }
      public DbSet<SliderContent> SliderContent { get; set; }
      public DbSet<Category> Categories{ get; set; }
      public DbSet<Product> Products { get; set; }
      public DbSet<Bio> Bios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
    
}
