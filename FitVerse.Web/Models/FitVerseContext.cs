using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FitVerse.Web.Models
{
    public class FitVerseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Banner> Banners { get; set; }

        public FitVerseContext(DbContextOptions<FitVerseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call base.OnModelCreating for IdentityDbContext to configure Identity tables
            base.OnModelCreating(modelBuilder);

            // Configure relationships for your custom models
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

            // Configure CartItem and Order to link to ApplicationUser (IdentityUser)
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

            // Configure indexes for performance
            // IdentityDbContext already handles unique index for Email (UserName is also email by default)
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.IsNewArrival);
        }

        // Overriding SaveChanges to automatically update CreatedAt and UpdatedAt
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
            // Only update timestamps for entities that have these properties and are not Identity entities
            // Identity entities have their own timestamping mechanisms or are handled internally.
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Category || e.Entity is Product ||
                            e.Entity is CartItem || e.Entity is Order || e.Entity is OrderItem || e.Entity is Banner ||
                            e.Entity is ApplicationUser)
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                // Set CreatedAt only for new entities
                if (entry.State == EntityState.Added)
                {
                    var createdAtProperty = entry.Entity.GetType().GetProperty("CreatedAt");
                    if (createdAtProperty != null && createdAtProperty.PropertyType == typeof(DateTime) && createdAtProperty.CanWrite)
                    {
                        createdAtProperty.SetValue(entry.Entity, DateTime.UtcNow);
                    }
                }

                // Set UpdatedAt for both added and modified entities
                var updatedAtProperty = entry.Entity.GetType().GetProperty("UpdatedAt");
                if (updatedAtProperty != null && updatedAtProperty.PropertyType == typeof(DateTime) && updatedAtProperty.CanWrite)
                {
                    updatedAtProperty.SetValue(entry.Entity, DateTime.UtcNow);
                }
            }
        }
    }
}
