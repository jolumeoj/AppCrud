using Microsoft.EntityFrameworkCore;
using AppCrud.Models;
namespace AppCrud.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(tb =>
            {
                tb.HasKey(col => col.IdUser);
                
                tb.Property(col => col.IdUser)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                tb.Property(col => col.Name).HasMaxLength(50);
                tb.Property(col => col.Email).HasMaxLength(50);
                
            });
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Product>(tb =>
            {
                tb.HasKey(col => col.IdProduct);

                tb.Property(col => col.IdProduct)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                tb.Property(col => col.ProductName).HasMaxLength(200);
                tb.Property(col => col.Price).HasPrecision(18,2);

            });

            modelBuilder.Entity<Product>().ToTable("Product");

            modelBuilder.Entity<Feature>(tb =>
            {
                tb.HasKey(col => col.IdFeature);

                tb.Property(col => col.IdFeature)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                tb.Property(col => col.FeatureName).HasMaxLength(200);
               
            });

            modelBuilder.Entity<Feature>().ToTable("Feature");
            
            modelBuilder.Entity<WishProduct>(tb =>
            {
                tb.HasKey(col => col.Id);

                tb.Property(col => col.Id)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();
               
            });

            modelBuilder.Entity<Feature>().ToTable("Feature");
        }
        
       
            
            
            
        
    }
}
