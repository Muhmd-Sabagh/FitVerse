using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitVerse.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Banner",
                table: "Banner");

            migrationBuilder.RenameTable(
                name: "Banner",
                newName: "Banners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Banners",
                table: "Banners",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ImageUrl", "LinkUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 12, 17, 12, 0, 0, 0, DateTimeKind.Utc), "/images/banners/slider1.jpg", "/Product/CategoryProducts?categoryId=12", new DateTime(2022, 12, 17, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImageUrl", "LinkUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 12, 18, 12, 0, 0, 0, DateTimeKind.Utc), "/images/banners/slider2.jpg", "/Home/Index", new DateTime(2022, 12, 18, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ImageUrl", "LinkUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 12, 19, 12, 0, 0, 0, DateTimeKind.Utc), "/images/banners/slider3.jpg", "/Product/CategoryProducts?categoryId=6", new DateTime(2022, 12, 19, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "/images/categories/men.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "/images/categories/women.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "/images/categories/kids.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "/images/categories/accessories.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's T-Shirts", "/images/categories/men/t-shirts.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's Jackets", "/images/categories/men/jackets.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's Sweatshirts", "/images/categories/men/sweatshirts.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's Shirts", "/images/categories/men/shirts.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's Pullovers", "/images/categories/men/pullovers.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's Pants", "/images/categories/men/pants.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's Shorts", "/images/categories/men/shorts.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's T-Shirts", "/images/categories/women/t-shirts.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Jackets", "/images/categories/women/jackets.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Sweatshirts", "/images/categories/women/sweatshirts.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Shirts & Blouses", "/images/categories/women/shirts-blouses.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Pullovers", "/images/categories/women/pullovers.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Cardigans", "/images/categories/women/cardigans.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Sets & Dresses", "/images/categories/women/sets-dresses.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Pants", "/images/categories/women/pants.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Skirts", "/images/categories/women/skirts.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Home-wear", "/images/categories/women/home-wear.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Boys' Clothing", "/images/categories/kids/boys.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Girls' Clothing", "/images/categories/kids/girls.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Fashion Bags", "/images/categories/accessories/bags.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Fashion Belts", "/images/categories/accessories/belts.jpg", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "DiscountPercentage", "ImageUrl", "IsActive", "IsNewArrival", "Material", "Name", "Price", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 5, new DateTime(2022, 12, 12, 12, 0, 0, 0, DateTimeKind.Utc), "Comfortable and stylish casual t-shirt for men. Perfect for everyday wear.", 20m, "/images/products/men/t-shirts/men-tshirt1.jpg", true, true, "Cotton", "Men's Casual T-Shirt", 25.00m, 50, new DateTime(2022, 12, 12, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 6, new DateTime(2022, 12, 7, 12, 0, 0, 0, DateTimeKind.Utc), "Premium leather jacket for a bold look. Durable and fashionable.", 15m, "/images/products/men/jackets/men-jacket1.jpg", true, false, "Leather", "Stylish Leather Jacket", 150.00m, 20, new DateTime(2022, 12, 7, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 18, new DateTime(2022, 12, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Light and airy summer dress, ideal for warm weather and special occasions.", null, "/images/products/women/sets-dresses/women-dress1.jpg", true, true, "Polyester", "Elegant Summer Dress", 60.00m, 30, new DateTime(2022, 12, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 22, new DateTime(2022, 12, 16, 12, 0, 0, 0, DateTimeKind.Utc), "Fun graphic t-shirt for boys, soft and breathable.", 10m, "/images/products/kids/boys/kids-boys-tee1.jpg", true, true, "Cotton Blend", "Kids' Graphic Tee", 18.00m, 70, new DateTime(2022, 12, 16, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 24, new DateTime(2022, 12, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Chic handbag with multiple compartments, perfect for daily use.", null, "/images/products/accessories/bags/accessories-bag1.jpg", true, false, "Synthetic Leather", "Designer Handbag", 85.00m, 40, new DateTime(2022, 12, 10, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 8, new DateTime(2022, 12, 13, 12, 0, 0, 0, DateTimeKind.Utc), "Classic denim shirt for a rugged yet stylish look. Versatile for any season.", 25m, "/images/products/men/shirts/men-shirt1.jpg", true, false, "Denim", "Men's Denim Shirt", 45.00m, 60, new DateTime(2022, 12, 13, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 20, new DateTime(2022, 12, 15, 12, 0, 0, 0, DateTimeKind.Utc), "Flowy midi skirt with a vibrant floral print. Perfect for spring and summer.", null, "/images/products/women/skirts/women-skirt1.jpg", true, true, "Viscose", "Women's Floral Skirt", 35.00m, 55, new DateTime(2022, 12, 15, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 7, new DateTime(2022, 12, 17, 12, 0, 0, 0, DateTimeKind.Utc), "Soft and warm hoodie, suitable for both men and women. Great for layering.", 10m, "/images/products/men/sweatshirts/unisex-hoodie1.jpg", true, true, "Fleece", "Unisex Fleece Hoodie", 55.00m, 45, new DateTime(2022, 12, 17, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 22, new DateTime(2022, 12, 9, 12, 0, 0, 0, DateTimeKind.Utc), "Durable and warm jacket for kids, ideal for cold weather adventures.", null, "/images/products/kids/boys/kids-jacket1.jpg", true, false, "Waterproof Nylon", "Kids' Winter Jacket", 70.00m, 25, new DateTime(2022, 12, 9, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 15, new DateTime(2022, 12, 11, 12, 0, 0, 0, DateTimeKind.Utc), "Luxurious silk blouse, perfect for professional or formal occasions.", 20m, "/images/products/women/shirts-blouses/women-blouse1.jpg", true, false, "Silk", "Elegant Women's Blouse", 75.00m, 35, new DateTime(2022, 12, 11, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 10, new DateTime(2022, 12, 18, 12, 0, 0, 0, DateTimeKind.Utc), "Modern slim fit chino pants, comfortable and stylish for various occasions.", null, "/images/products/men/pants/men-pants1.jpg", true, true, "Cotton Twill", "Men's Slim Fit Chinos", 50.00m, 65, new DateTime(2022, 12, 18, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 25, new DateTime(2022, 12, 20, 12, 0, 0, 0, DateTimeKind.Utc), "A timeless accessory, made from genuine leather for durability and style.", null, "/images/products/accessories/belts/accessories-belt1.jpg", true, true, "Genuine Leather", "Classic Leather Belt", 30.00m, 80, new DateTime(2022, 12, 20, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 19, new DateTime(2022, 12, 21, 12, 0, 0, 0, DateTimeKind.Utc), "High-waist, comfortable yoga pants for all workouts.", 5m, "/images/products/women/pants/women-yogapants.jpg", true, true, "Spandex Blend", "Women's Yoga Pants", 40.00m, 90, new DateTime(2022, 12, 21, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 23, new DateTime(2022, 12, 23, 12, 0, 0, 0, DateTimeKind.Utc), "Soft and fun hoodie with popular character print for kids.", null, "/images/products/kids/girls/kids-hoodie.jpg", true, false, "Cotton Fleece", "Kids' Character Hoodie", 30.00m, 110, new DateTime(2022, 12, 23, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, 11, new DateTime(2022, 12, 24, 12, 0, 0, 0, DateTimeKind.Utc), "Breathable and quick-dry shorts for sports and casual wear.", 10m, "/images/products/men/shorts/men-shorts1.jpg", true, true, "Polyester", "Men's Athletic Shorts", 22.00m, 150, new DateTime(2022, 12, 24, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 2, 12, 0, 0, 0, DateTimeKind.Utc), "test1@example.com", "Test User One", "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f", "Customer", new DateTime(2022, 12, 22, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2022, 12, 7, 12, 0, 0, 0, DateTimeKind.Utc), "test2@example.com", "Test User Two", "fbb4a8a163ffa958b4f02bf9cabb30cfefb40de803f2c4c346a9d39b3be1b544", "Customer", new DateTime(2022, 12, 27, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CreatedAt", "ProductId", "Quantity", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 1, 10, 0, 0, 0, DateTimeKind.Utc), 1, 2, new DateTime(2023, 1, 1, 10, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, new DateTime(2023, 1, 1, 11, 0, 0, 0, DateTimeKind.Utc), 3, 1, new DateTime(2023, 1, 1, 11, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, new DateTime(2023, 1, 1, 9, 0, 0, 0, DateTimeKind.Utc), 6, 1, new DateTime(2023, 1, 1, 9, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 4, new DateTime(2023, 1, 1, 8, 0, 0, 0, DateTimeKind.Utc), 8, 2, new DateTime(2023, 1, 1, 8, 0, 0, 0, DateTimeKind.Utc), 2 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "CustomerEmail", "CustomerName", "CustomerPhone", "OrderDate", "ShippingAddress", "Status", "TotalAmount", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 22, 12, 0, 0, 0, DateTimeKind.Utc), "test1@example.com", "Test User One", "555-111-2222", new DateTime(2022, 12, 22, 12, 0, 0, 0, DateTimeKind.Utc), "123 Main St, Anytown, Anystate 12345", "Delivered", 100.000m, new DateTime(2022, 12, 27, 12, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, new DateTime(2022, 12, 29, 12, 0, 0, 0, DateTimeKind.Utc), "test1@example.com", "Test User One", "555-111-2222", new DateTime(2022, 12, 29, 12, 0, 0, 0, DateTimeKind.Utc), "123 Main St, Anytown, Anystate 12345", "Shipped", 187.5000m, new DateTime(2022, 12, 30, 12, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, new DateTime(2022, 12, 25, 12, 0, 0, 0, DateTimeKind.Utc), "test2@example.com", "Test User Two", "555-333-4444", new DateTime(2022, 12, 25, 12, 0, 0, 0, DateTimeKind.Utc), "456 Oak Ave, Villageton, Stateland 67890", "Pending", 132.7500m, new DateTime(2022, 12, 25, 12, 0, 0, 0, DateTimeKind.Utc), 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CreatedAt", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 22, 12, 0, 0, 0, DateTimeKind.Utc), 1, 1, 2, 20.000m },
                    { 2, new DateTime(2022, 12, 22, 12, 0, 0, 0, DateTimeKind.Utc), 1, 3, 1, 60.00m },
                    { 3, new DateTime(2022, 12, 29, 12, 0, 0, 0, DateTimeKind.Utc), 2, 10, 1, 60.000m },
                    { 4, new DateTime(2022, 12, 29, 12, 0, 0, 0, DateTimeKind.Utc), 2, 2, 1, 127.5000m },
                    { 5, new DateTime(2022, 12, 25, 12, 0, 0, 0, DateTimeKind.Utc), 3, 6, 1, 33.7500m },
                    { 6, new DateTime(2022, 12, 25, 12, 0, 0, 0, DateTimeKind.Utc), 3, 8, 2, 49.500m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Banners",
                table: "Banners");

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Banners",
                newName: "Banner");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Banner",
                table: "Banner",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Banner",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ImageUrl", "LinkUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/banners/summer-sale.jpg", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Banner",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImageUrl", "LinkUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/banners/new-arrivals.jpg", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Banner",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ImageUrl", "LinkUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/banners/winter-collection.jpg", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
