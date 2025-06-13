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
                new Category { Id = 1, Name = "Men", Description = "Men's Clothing", ImageUrl = "/images/categories/men.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 2, Name = "Women", Description = "Women's Clothing", ImageUrl = "/images/categories/women.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 3, Name = "Kids", Description = "Kids' Clothing", ImageUrl = "/images/categories/kids.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 4, Name = "Accessories", Description = "Fashion Accessories", ImageUrl = "/images/categories/accessories.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },

                // Men's subcategories
                new Category { Id = 5, Name = "T-Shirts", ParentCategoryId = 1, Description = "Men's T-Shirts", ImageUrl = "/images/categories/men/t-shirts.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 6, Name = "Jackets", ParentCategoryId = 1, Description = "Men's Jackets", ImageUrl = "/images/categories/men/jackets.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 7, Name = "Sweatshirts", ParentCategoryId = 1, Description = "Men's Sweatshirts", ImageUrl = "/images/categories/men/sweatshirts.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 8, Name = "Shirts", ParentCategoryId = 1, Description = "Men's Shirts", ImageUrl = "/images/categories/men/shirts.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 9, Name = "Pullovers", ParentCategoryId = 1, Description = "Men's Pullovers", ImageUrl = "/images/categories/men/pullovers.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 10, Name = "Pants", ParentCategoryId = 1, Description = "Men's Pants", ImageUrl = "/images/categories/men/pants.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 11, Name = "Shorts", ParentCategoryId = 1, Description = "Men's Shorts", ImageUrl = "/images/categories/men/shorts.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },

                // Women's subcategories
                new Category { Id = 12, Name = "T-Shirts", ParentCategoryId = 2, Description = "Women's T-Shirts", ImageUrl = "/images/categories/women/t-shirts.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 13, Name = "Jackets", ParentCategoryId = 2, Description = "Women's Jackets", ImageUrl = "/images/categories/women/jackets.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 14, Name = "Sweatshirts", ParentCategoryId = 2, Description = "Women's Sweatshirts", ImageUrl = "/images/categories/women/sweatshirts.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 15, Name = "Shirts & Blouses", ParentCategoryId = 2, Description = "Women's Shirts & Blouses", ImageUrl = "/images/categories/women/shirts-blouses.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 16, Name = "Pullovers", ParentCategoryId = 2, Description = "Women's Pullovers", ImageUrl = "/images/categories/women/pullovers.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 17, Name = "Cardigans", ParentCategoryId = 2, Description = "Women's Cardigans", ImageUrl = "/images/categories/women/cardigans.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 18, Name = "Sets & Dresses", ParentCategoryId = 2, Description = "Women's Sets & Dresses", ImageUrl = "/images/categories/women/sets-dresses.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 19, Name = "Pants", ParentCategoryId = 2, Description = "Women's Pants", ImageUrl = "/images/categories/women/pants.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 20, Name = "Skirts", ParentCategoryId = 2, Description = "Women's Skirts", ImageUrl = "/images/categories/women/skirts.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 21, Name = "Home-wear", ParentCategoryId = 2, Description = "Women's Home-wear", ImageUrl = "/images/categories/women/home-wear.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },

                // Kids' subcategories
                new Category { Id = 22, Name = "Boys", ParentCategoryId = 3, Description = "Boys' Clothing", ImageUrl = "/images/categories/kids/boys.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 23, Name = "Girls", ParentCategoryId = 3, Description = "Girls' Clothing", ImageUrl = "/images/categories/kids/girls.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },

                // Accessories subcategories
                new Category { Id = 24, Name = "Bags", ParentCategoryId = 4, Description = "Fashion Bags", ImageUrl = "/images/categories/accessories/bags.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                new Category { Id = 25, Name = "Belts", ParentCategoryId = 4, Description = "Fashion Belts", ImageUrl = "/images/categories/accessories/belts.jpg", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase }
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

            // Seed Products
            // Constructing image paths using the categoryPathMap
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Men's Casual T-Shirt",
                    Material = "Cotton",
                    Description = "Comfortable and stylish casual t-shirt for men. Perfect for everyday wear.",
                    Price = 25.00m,
                    DiscountPercentage = 20m,
                    IsNewArrival = true,
                    ImageUrl = $"/images/products/{categoryPathMap[5].mainFolder}/{categoryPathMap[5].subFolder}/men-tshirt1.jpg",
                    StockQuantity = 50,
                    CategoryId = 5,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-20),
                    UpdatedAt = fixedUtcDateBase.AddDays(-20)
                },
                new Product
                {
                    Id = 2,
                    Name = "Stylish Leather Jacket",
                    Material = "Leather",
                    Description = "Premium leather jacket for a bold look. Durable and fashionable.",
                    Price = 150.00m,
                    DiscountPercentage = 15m,
                    IsNewArrival = false,
                    ImageUrl = $"/images/products/{categoryPathMap[6].mainFolder}/{categoryPathMap[6].subFolder}/men-jacket1.jpg",
                    StockQuantity = 20,
                    CategoryId = 6,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-25),
                    UpdatedAt = fixedUtcDateBase.AddDays(-25)
                },
                new Product
                {
                    Id = 3,
                    Name = "Elegant Summer Dress",
                    Material = "Polyester",
                    Description = "Light and airy summer dress, ideal for warm weather and special occasions.",
                    Price = 60.00m,
                    DiscountPercentage = null,
                    IsNewArrival = true,
                    ImageUrl = $"/images/products/{categoryPathMap[18].mainFolder}/{categoryPathMap[18].subFolder}/women-dress1.jpg",
                    StockQuantity = 30,
                    CategoryId = 18,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-18),
                    UpdatedAt = fixedUtcDateBase.AddDays(-18)
                },
                new Product
                {
                    Id = 4,
                    Name = "Kids' Graphic Tee",
                    Material = "Cotton Blend",
                    Description = "Fun graphic t-shirt for boys, soft and breathable.",
                    Price = 18.00m,
                    DiscountPercentage = 10m,
                    IsNewArrival = true,
                    ImageUrl = $"/images/products/{categoryPathMap[22].mainFolder}/{categoryPathMap[22].subFolder}/kids-boys-tee1.jpg",
                    StockQuantity = 70,
                    CategoryId = 22,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-16),
                    UpdatedAt = fixedUtcDateBase.AddDays(-16)
                },
                new Product
                {
                    Id = 5,
                    Name = "Designer Handbag",
                    Material = "Synthetic Leather",
                    Description = "Chic handbag with multiple compartments, perfect for daily use.",
                    Price = 85.00m,
                    DiscountPercentage = null,
                    IsNewArrival = false,
                    ImageUrl = $"/images/products/{categoryPathMap[24].mainFolder}/{categoryPathMap[24].subFolder}/accessories-bag1.jpg",
                    StockQuantity = 40,
                    CategoryId = 24,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-22),
                    UpdatedAt = fixedUtcDateBase.AddDays(-22)
                },
                new Product
                {
                    Id = 6,
                    Name = "Men's Denim Shirt",
                    Material = "Denim",
                    Description = "Classic denim shirt for a rugged yet stylish look. Versatile for any season.",
                    Price = 45.00m,
                    DiscountPercentage = 25m,
                    IsNewArrival = false,
                    ImageUrl = $"/images/products/{categoryPathMap[8].mainFolder}/{categoryPathMap[8].subFolder}/men-shirt1.jpg",
                    StockQuantity = 60,
                    CategoryId = 8,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-19),
                    UpdatedAt = fixedUtcDateBase.AddDays(-19)
                },
                new Product
                {
                    Id = 7,
                    Name = "Women's Floral Skirt",
                    Material = "Viscose",
                    Description = "Flowy midi skirt with a vibrant floral print. Perfect for spring and summer.",
                    Price = 35.00m,
                    DiscountPercentage = null,
                    IsNewArrival = true,
                    ImageUrl = $"/images/products/{categoryPathMap[20].mainFolder}/{categoryPathMap[20].subFolder}/women-skirt1.jpg",
                    StockQuantity = 55,
                    CategoryId = 20,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-17),
                    UpdatedAt = fixedUtcDateBase.AddDays(-17)
                },
                new Product
                {
                    Id = 8,
                    Name = "Unisex Fleece Hoodie",
                    Material = "Fleece",
                    Description = "Soft and warm hoodie, suitable for both men and women. Great for layering.",
                    Price = 55.00m,
                    DiscountPercentage = 10m,
                    IsNewArrival = true,
                    ImageUrl = $"/images/products/{categoryPathMap[7].mainFolder}/{categoryPathMap[7].subFolder}/unisex-hoodie1.jpg",
                    StockQuantity = 45,
                    CategoryId = 7,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-15),
                    UpdatedAt = fixedUtcDateBase.AddDays(-15)
                },
                new Product
                {
                    Id = 9,
                    Name = "Kids' Winter Jacket",
                    Material = "Waterproof Nylon",
                    Description = "Durable and warm jacket for kids, ideal for cold weather adventures.",
                    Price = 70.00m,
                    DiscountPercentage = null,
                    IsNewArrival = false,
                    ImageUrl = $"/images/products/{categoryPathMap[22].mainFolder}/{categoryPathMap[22].subFolder}/kids-jacket1.jpg",
                    StockQuantity = 25,
                    CategoryId = 22,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-23),
                    UpdatedAt = fixedUtcDateBase.AddDays(-23)
                },
                new Product
                {
                    Id = 10,
                    Name = "Elegant Women's Blouse",
                    Material = "Silk",
                    Description = "Luxurious silk blouse, perfect for professional or formal occasions.",
                    Price = 75.00m,
                    DiscountPercentage = 20m,
                    IsNewArrival = false,
                    ImageUrl = $"/images/products/{categoryPathMap[15].mainFolder}/{categoryPathMap[15].subFolder}/women-blouse1.jpg",
                    StockQuantity = 35,
                    CategoryId = 15,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-21),
                    UpdatedAt = fixedUtcDateBase.AddDays(-21)
                },
                new Product
                {
                    Id = 11,
                    Name = "Men's Slim Fit Chinos",
                    Material = "Cotton Twill",
                    Description = "Modern slim fit chino pants, comfortable and stylish for various occasions.",
                    Price = 50.00m,
                    DiscountPercentage = null,
                    IsNewArrival = true,
                    ImageUrl = $"/images/products/{categoryPathMap[10].mainFolder}/{categoryPathMap[10].subFolder}/men-pants1.jpg",
                    StockQuantity = 65,
                    CategoryId = 10,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-14),
                    UpdatedAt = fixedUtcDateBase.AddDays(-14)
                },
                new Product
                {
                    Id = 12,
                    Name = "Classic Leather Belt",
                    Material = "Genuine Leather",
                    Description = "A timeless accessory, made from genuine leather for durability and style.",
                    Price = 30.00m,
                    DiscountPercentage = null,
                    IsNewArrival = true,
                    ImageUrl = $"/images/products/{categoryPathMap[25].mainFolder}/{categoryPathMap[25].subFolder}/accessories-belt1.jpg",
                    StockQuantity = 80,
                    CategoryId = 25,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-12),
                    UpdatedAt = fixedUtcDateBase.AddDays(-12)
                },
                new Product
                {
                    Id = 13,
                    Name = "Women's Yoga Pants",
                    Material = "Spandex Blend",
                    Description = "High-waist, comfortable yoga pants for all workouts.",
                    Price = 40.00m,
                    DiscountPercentage = 5m,
                    IsNewArrival = true,
                    ImageUrl = $"/images/products/{categoryPathMap[19].mainFolder}/{categoryPathMap[19].subFolder}/women-yogapants.jpg",
                    StockQuantity = 90,
                    CategoryId = 19,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-11),
                    UpdatedAt = fixedUtcDateBase.AddDays(-11)
                },
                new Product
                {
                    Id = 14,
                    Name = "Kids' Character Hoodie",
                    Material = "Cotton Fleece",
                    Description = "Soft and fun hoodie with popular character print for kids.",
                    Price = 30.00m,
                    DiscountPercentage = null,
                    IsNewArrival = false,
                    ImageUrl = $"/images/products/{categoryPathMap[23].mainFolder}/{categoryPathMap[23].subFolder}/kids-hoodie.jpg",
                    StockQuantity = 110,
                    CategoryId = 23,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-9),
                    UpdatedAt = fixedUtcDateBase.AddDays(-9)
                },
                new Product
                {
                    Id = 15,
                    Name = "Men's Athletic Shorts",
                    Material = "Polyester",
                    Description = "Breathable and quick-dry shorts for sports and casual wear.",
                    Price = 22.00m,
                    DiscountPercentage = 10m,
                    IsNewArrival = true,
                    ImageUrl = $"/images/products/{categoryPathMap[11].mainFolder}/{categoryPathMap[11].subFolder}/men-shorts1.jpg",
                    StockQuantity = 150,
                    CategoryId = 11,
                    IsActive = true,
                    CreatedAt = fixedUtcDateBase.AddDays(-8),
                    UpdatedAt = fixedUtcDateBase.AddDays(-8)
                }
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
    }
}
