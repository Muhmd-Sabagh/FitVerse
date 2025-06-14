using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic; // Required for Dictionary

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
        public DbSet<Banner> Banners { get; set; }

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


            // Configure indexes for performance
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.IsNewArrival);


            // Seed data directly within OnModelCreating with FIXED DateTime values and updated ImageUrls
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Using a fixed point in time for seeding to avoid PendingModelChangesWarning
            DateTime fixedUtcDateBase = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Utc);

            // Helper for Product Image Paths: CategoryId -> (Main Folder, Subfolder)
            var categoryPathMap = new Dictionary<int, (string mainFolder, string subFolder)>
            {
                { 5, ("men", "t-shirts") },       // Men's T-Shirts
                { 6, ("men", "jackets") },        // Men's Jackets
                { 7, ("men", "sweatshirts") },    // Men's Sweatshirts
                { 8, ("men", "shirts") },         // Men's Shirts
                { 9, ("men", "pullovers") },      // Men's Pullovers
                { 10, ("men", "pants") },         // Men's Pants
                { 11, ("men", "shorts") },        // Men's Shorts
                { 12, ("women", "t-shirts") },    // Women's T-Shirts
                { 13, ("women", "jackets") },     // Women's Jackets
                { 14, ("women", "sweatshirts") }, // Women's Sweatshirts
                { 15, ("women", "shirts-blouses") },// Women's Shirts & Blouses
                { 16, ("women", "pullovers") },   // Women's Pullovers
                { 17, ("women", "cardigans") },   // Women's Cardigans
                { 18, ("women", "sets-dresses") },// Women's Sets & Dresses
                { 19, ("women", "pants") },       // Women's Pants
                { 20, ("women", "skirts") },      // Women's Skirts
                { 21, ("women", "home-wear") },   // Women's Home-wear
                { 22, ("kids", "boys") },         // Kids' Boys
                { 23, ("kids", "girls") },        // Kids' Girls
                { 24, ("accessories", "bags") },  // Accessories - Bags
                { 25, ("accessories", "belts") }  // Accessories - Belts
            };


            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                // Main categories
                new Category { Id = 1, Name = "Men", Description = "Men's Clothing", ImageUrl = "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 2, Name = "Women", Description = "Women's Clothing", ImageUrl = "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 3, Name = "Kids", Description = "Kids' Clothing", ImageUrl = "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 4, Name = "Accessories", Description = "Fashion Accessories", ImageUrl = "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },

                // Men's subcategories
                new Category { Id = 5, Name = "T-Shirts", ParentCategoryId = 1, Description = "Men's T-Shirts", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/dark-emerald-design-3868vig-zipper-squares-polo-509857.jpg?v=1747904939", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 6, Name = "Jackets", ParentCategoryId = 1, Description = "Men's Jackets", ImageUrl = "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 7, Name = "Sweatshirts", ParentCategoryId = 1, Description = "Men's Sweatshirts", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/beige-hoodie-641391.jpg?v=1746659214", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 8, Name = "Shirts", ParentCategoryId = 1, Description = "Men's Shirts", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/violet-oxford-shirt-121545.jpg?v=1747153870", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 9, Name = "Pullovers", ParentCategoryId = 1, Description = "Men's Pullovers", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/camel-design-p2202-pf-round-pullover-369590.jpg?v=1746658721", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 10, Name = "Pants", ParentCategoryId = 1, Description = "Men's Pants", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/cloud-soft-pant-307024.jpg?v=1749593291", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 11, Name = "Shorts", ParentCategoryId = 1, Description = "Men's Shorts", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/smoke-green-zipper-melton-short-530709.jpg?v=1748024610", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },

                // Women's subcategories
                new Category { Id = 12, Name = "T-Shirts", ParentCategoryId = 2, Description = "Women's T-Shirts", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/silver-curved-long-sleeve-629162.jpg?v=1747153907", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 13, Name = "Jackets", ParentCategoryId = 2, Description = "Women's Jackets", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/camel-velvet-vest-design-4-192959.jpg?v=1746659011", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 14, Name = "Sweatshirts", ParentCategoryId = 2, Description = "Women's Sweatshirts", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/web_65.jpg?v=1746658520", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 15, Name = "Shirts & Blouses", ParentCategoryId = 2, Description = "Women's Shirts & Blouses", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/olive-linen-pocket-over-size-shirt-211139.jpg?v=1749593439", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 16, Name = "Pullovers", ParentCategoryId = 2, Description = "Women's Pullovers", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/wood-hoodie-pullover-163921.jpg?v=1746658972", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 17, Name = "Cardigans", ParentCategoryId = 2, Description = "Women's Cardigans", ImageUrl = "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 18, Name = "Sets & Dresses", ParentCategoryId = 2, Description = "Women's Sets & Dresses", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/dark-olive-basic-dress-328062.jpg?v=1749147053", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 19, Name = "Pants", ParentCategoryId = 2, Description = "Women's Pants", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/BeigeCrochetPant_1.jpg?v=1746657631", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 20, Name = "Skirts", ParentCategoryId = 2, Description = "Women's Skirts", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/mist-rd-skirt-663918.jpg?v=1746660639", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 21, Name = "Home-wear", ParentCategoryId = 2, Description = "Women's Home-wear", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/checkered-29-home-pants-w-292383.jpg?v=1746662229", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },

                // Kids' subcategories
                new Category { Id = 22, Name = "Boys", ParentCategoryId = 3, Description = "Boys' Clothing", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/lentil-kids-linen-shirt-904488.jpg?v=1749242051", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 23, Name = "Girls", ParentCategoryId = 3, Description = "Girls' Clothing", ImageUrl = "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },

                // Accessories subcategories
                new Category { Id = 24, Name = "Bags", ParentCategoryId = 4, Description = "Fashion Bags", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/royal-blue-waist-bag-985428.jpg?v=1746659384", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 25, Name = "Belts", ParentCategoryId = 4, Description = "Fashion Belts", ImageUrl = "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/lilac-design-2-belt-165538.jpg?v=1746663213", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase }
            );

            // Hash passwords for test users
            string passwordHash1 = HashPassword("password123");
            string passwordHash2 = HashPassword("securepass");

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Test User One",
                    Email = "test1@example.com",
                    PasswordHash = passwordHash1,
                    Role = "Customer",
                    CreatedAt = fixedUtcDateBase.AddDays(-30),
                    UpdatedAt = fixedUtcDateBase.AddDays(-10)
                },
                new User
                {
                    Id = 2,
                    FullName = "Test User Two",
                    Email = "test2@example.com",
                    PasswordHash = passwordHash2,
                    Role = "Customer",
                    CreatedAt = fixedUtcDateBase.AddDays(-25),
                    UpdatedAt = fixedUtcDateBase.AddDays(-5)
                }
            );

            // Seed Banners
            modelBuilder.Entity<Banner>().HasData(
                new Banner { Id = 1, Title = "Summer Sale", Description = "Up to 50% off on summer collection", ImageUrl = "/images/banners/slider1.jpg", LinkUrl = "/Product/CategoryProducts?categoryId=12", DisplayOrder = 1, IsActive = true, CreatedAt = fixedUtcDateBase.AddDays(-15), UpdatedAt = fixedUtcDateBase.AddDays(-15) },
                new Banner { Id = 2, Title = "New Arrivals", Description = "Check out our latest fashion trends", ImageUrl = "/images/banners/slider2.jpg", LinkUrl = "/Home/Index", DisplayOrder = 2, IsActive = true, CreatedAt = fixedUtcDateBase.AddDays(-14), UpdatedAt = fixedUtcDateBase.AddDays(-14) },
                new Banner { Id = 3, Title = "Winter Collection", Description = "Stay warm with our winter essentials", ImageUrl = "/images/banners/slider3.jpg", LinkUrl = "/Product/CategoryProducts?categoryId=6", DisplayOrder = 3, IsActive = true, CreatedAt = fixedUtcDateBase.AddDays(-13), UpdatedAt = fixedUtcDateBase.AddDays(-13) }
            );

            // Seed CartItems for User 1 (Current shopping cart)
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { Id = 1, UserId = 1, ProductId = 1, CreatedAt = fixedUtcDateBase.AddHours(-2), UpdatedAt = fixedUtcDateBase.AddHours(-2), Quantity = 2 },
                new CartItem { Id = 2, UserId = 1, ProductId = 3, CreatedAt = fixedUtcDateBase.AddHours(-1), UpdatedAt = fixedUtcDateBase.AddHours(-1), Quantity = 1 }
            );

            // Seed CartItems for User 2 (Current shopping cart)
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { Id = 3, UserId = 2, ProductId = 6, CreatedAt = fixedUtcDateBase.AddHours(-3), UpdatedAt = fixedUtcDateBase.AddHours(-3), Quantity = 1 },
                new CartItem { Id = 4, UserId = 2, ProductId = 8, CreatedAt = fixedUtcDateBase.AddHours(-4), UpdatedAt = fixedUtcDateBase.AddHours(-4), Quantity = 2 }
            );

            // Define effective prices for order items at the time of order (based on product seed data above)
            decimal order1_product1_effectivePrice = 25.00m * (1 - (20m / 100m)); // $20.00
            decimal order1_product3_effectivePrice = 60.00m; // $60.00

            decimal order2_product10_effectivePrice = 75.00m * (1 - (20m / 100m)); // $60.00
            decimal order2_product2_effectivePrice = 150.00m * (1 - (15m / 100m)); // $127.50

            decimal order3_product6_effectivePrice = 45.00m * (1 - (25m / 100m)); // $33.75
            decimal order3_product8_effectivePrice = 55.00m * (1 - (10m / 100m)); // $49.50


            // Seed Orders for User 1
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    UserId = 1,
                    OrderDate = fixedUtcDateBase.AddDays(-10),
                    TotalAmount = (2 * order1_product1_effectivePrice) + (1 * order1_product3_effectivePrice),
                    Status = "Delivered",
                    ShippingAddress = "123 Main St, Anytown, Anystate 12345",
                    CustomerName = "Test User One",
                    CustomerEmail = "test1@example.com",
                    CustomerPhone = "555-111-2222",
                    CreatedAt = fixedUtcDateBase.AddDays(-10),
                    UpdatedAt = fixedUtcDateBase.AddDays(-5)
                },
                new Order
                {
                    Id = 2,
                    UserId = 1,
                    OrderDate = fixedUtcDateBase.AddDays(-3),
                    TotalAmount = (1 * order2_product10_effectivePrice) + (1 * order2_product2_effectivePrice),
                    Status = "Shipped",
                    ShippingAddress = "123 Main St, Anytown, Anystate 12345",
                    CustomerName = "Test User One",
                    CustomerEmail = "test1@example.com",
                    CustomerPhone = "555-111-2222",
                    CreatedAt = fixedUtcDateBase.AddDays(-3),
                    UpdatedAt = fixedUtcDateBase.AddDays(-2)
                }
            );

            // Seed Orders for User 2
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 3,
                    UserId = 2,
                    OrderDate = fixedUtcDateBase.AddDays(-7),
                    TotalAmount = (1 * order3_product6_effectivePrice) + (2 * order3_product8_effectivePrice),
                    Status = "Pending",
                    ShippingAddress = "456 Oak Ave, Villageton, Stateland 67890",
                    CustomerName = "Test User Two",
                    CustomerEmail = "test2@example.com",
                    CustomerPhone = "555-333-4444",
                    CreatedAt = fixedUtcDateBase.AddDays(-7),
                    UpdatedAt = fixedUtcDateBase.AddDays(-7)
                }
            );

            // Seed OrderItems for Order 1 (User 1)
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = order1_product1_effectivePrice, CreatedAt = fixedUtcDateBase.AddDays(-10) },
                new OrderItem { Id = 2, OrderId = 1, ProductId = 3, Quantity = 1, UnitPrice = order1_product3_effectivePrice, CreatedAt = fixedUtcDateBase.AddDays(-10) }
            );

            // Seed OrderItems for Order 2 (User 1)
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 3, OrderId = 2, ProductId = 10, Quantity = 1, UnitPrice = order2_product10_effectivePrice, CreatedAt = fixedUtcDateBase.AddDays(-3) },
                new OrderItem { Id = 4, OrderId = 2, ProductId = 2, Quantity = 1, UnitPrice = order2_product2_effectivePrice, CreatedAt = fixedUtcDateBase.AddDays(-3) }
            );

            // Seed OrderItems for Order 3 (User 2)
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 5, OrderId = 3, ProductId = 6, Quantity = 1, UnitPrice = order3_product6_effectivePrice, CreatedAt = fixedUtcDateBase.AddDays(-7) },
                new OrderItem { Id = 6, OrderId = 3, ProductId = 8, Quantity = 2, UnitPrice = order3_product8_effectivePrice, CreatedAt = fixedUtcDateBase.AddDays(-7) }
            );
        }

        // Helper method to hash passwords (SHA256)
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
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
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is User || e.Entity is Category || e.Entity is Product ||
                            e.Entity is CartItem || e.Entity is Order || e.Entity is OrderItem || e.Entity is Banner)
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                // Set CreatedAt only for new entities
                if (entry.State == EntityState.Added)
                {
                    var createdAtProperty = entry.Entity.GetType().GetProperty("CreatedAt");
                    if (createdAtProperty != null && createdAtProperty.PropertyType == typeof(DateTime))
                    {
                        createdAtProperty.SetValue(entry.Entity, DateTime.UtcNow);
                    }
                }

                // Set UpdatedAt for both added and modified entities
                var updatedAtProperty = entry.Entity.GetType().GetProperty("UpdatedAt");
                if (updatedAtProperty != null && updatedAtProperty.PropertyType == typeof(DateTime))
                {
                    updatedAtProperty.SetValue(entry.Entity, DateTime.UtcNow);
                }
            }
        }
        public DbSet<FitVerse.Web.Models.CartItem_ViewModel> CartItem_ViewModel { get; set; } = default!;
    }
}
