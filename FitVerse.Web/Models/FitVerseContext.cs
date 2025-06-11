using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Models
{
    public class FitVerseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public FitVerseContext(DbContextOptions<FitVerseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(ci => ci.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.IsNewArrival);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.IsOnSale);

            modelBuilder.Entity<Product>().Ignore(p => p.IsOnSale);
            modelBuilder.Entity<Product>().Ignore(p => p.EffectivePrice);
            modelBuilder.Entity<CartItem>().Ignore(ci => ci.TotalPrice);
            modelBuilder.Entity<OrderItem>().Ignore(oi => oi.TotalPrice);

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Men", Description = "Men's Clothing" },
                new Category { Id = 2, Name = "Women", Description = "Women's Clothing" },
                new Category { Id = 3, Name = "Kids", Description = "Kids' Clothing" },
                new Category { Id = 4, Name = "Accessories", Description = "Fashion Accessories" },

                // Men's subcategories
                new Category { Id = 5, Name = "T-Shirts", ParentCategoryId = 1 },
                new Category { Id = 6, Name = "Jackets", ParentCategoryId = 1 },
                new Category { Id = 7, Name = "Sweatshirts", ParentCategoryId = 1 },
                new Category { Id = 8, Name = "Shirts", ParentCategoryId = 1 },
                new Category { Id = 9, Name = "Pullovers", ParentCategoryId = 1 },
                new Category { Id = 10, Name = "Pants", ParentCategoryId = 1 },
                new Category { Id = 11, Name = "Shorts", ParentCategoryId = 1 },

                // Women's subcategories
                new Category { Id = 12, Name = "T-Shirts", ParentCategoryId = 2 },
                new Category { Id = 13, Name = "Jackets", ParentCategoryId = 2 },
                new Category { Id = 14, Name = "Sweatshirts", ParentCategoryId = 2 },
                new Category { Id = 15, Name = "Shirts & Blouses", ParentCategoryId = 2 },
                new Category { Id = 16, Name = "Pullovers", ParentCategoryId = 2 },
                new Category { Id = 17, Name = "Cardigans", ParentCategoryId = 2 },
                new Category { Id = 18, Name = "Sets & Dresses", ParentCategoryId = 2 },
                new Category { Id = 19, Name = "Pants", ParentCategoryId = 2 },
                new Category { Id = 20, Name = "Skirts", ParentCategoryId = 2 },
                new Category { Id = 21, Name = "Home-wear", ParentCategoryId = 2 },

                // Kids' subcategories
                new Category { Id = 22, Name = "Boys", ParentCategoryId = 3 },
                new Category { Id = 23, Name = "Girls", ParentCategoryId = 3 },

                // Accessories subcategories
                new Category { Id = 24, Name = "Bags", ParentCategoryId = 4 },
                new Category { Id = 25, Name = "Belts", ParentCategoryId = 4 }
            );

            // Seed Banners
            modelBuilder.Entity<Banner>().HasData(
                new Banner { Id = 1, Title = "Summer Sale", Description = "Up to 50% off on summer collection", ImageUrl = "/images/banners/summer-sale.jpg", DisplayOrder = 1 },
                new Banner { Id = 2, Title = "New Arrivals", Description = "Check out our latest fashion trends", ImageUrl = "/images/banners/new-arrivals.jpg", DisplayOrder = 2 },
                new Banner { Id = 3, Title = "Winter Collection", Description = "Stay warm with our winter essentials", ImageUrl = "/images/banners/winter-collection.jpg", DisplayOrder = 3 }
            );
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is User || e.Entity is Category || e.Entity is Product ||
                           e.Entity is CartItem || e.Entity is Order || e.Entity is Banner)
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}
