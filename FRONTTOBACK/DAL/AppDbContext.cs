using FRONTTOBACK.Model;
using Microsoft.EntityFrameworkCore;

namespace FRONTTOBACK.DAL
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
      public DbSet<Slider> Slider { get; set; }
      public DbSet<SliderContent> SliderContent { get; set; }
      public DbSet<Category> Categories{ get; set; }
      public DbSet<Product> Products { get; set; }
      public DbSet<Bio> Bios { get; set; }
    }
    
}
