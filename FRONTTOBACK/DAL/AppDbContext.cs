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
      public DbSet<Sale> Sales { get; set; }
      public DbSet<SalesProduct> SalesProducts { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Slider>().HasData(
                new Slider
                {
                    Id = 1,
                    ImageUrl = "h3-slider-background.jpg",
                   
                },

                 new Slider
                 {
                     Id = 2,
                     ImageUrl = "h3-slider-background-2.jpg",

                 },

                  new Slider
                  {
                      Id = 3,
                      ImageUrl = "h3-slider-background-3.jpg",

                  }

          );

        }


    }
    
}
