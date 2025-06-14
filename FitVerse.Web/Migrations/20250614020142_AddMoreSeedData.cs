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
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's T-Shirts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/dark-emerald-design-3868vig-zipper-squares-polo-509857.jpg?v=1747904939", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's Jackets", "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's Sweatshirts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/beige-hoodie-641391.jpg?v=1746659214", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's Shirts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/violet-oxford-shirt-121545.jpg?v=1747153870", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's Pullovers", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/camel-design-p2202-pf-round-pullover-369590.jpg?v=1746658721", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's Pants", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/cloud-soft-pant-307024.jpg?v=1749593291", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men's Shorts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/smoke-green-zipper-melton-short-530709.jpg?v=1748024610", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's T-Shirts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/silver-curved-long-sleeve-629162.jpg?v=1747153907", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Jackets", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/camel-velvet-vest-design-4-192959.jpg?v=1746659011", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Sweatshirts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/web_65.jpg?v=1746658520", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Shirts & Blouses", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/olive-linen-pocket-over-size-shirt-211139.jpg?v=1749593439", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Pullovers", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/wood-hoodie-pullover-163921.jpg?v=1746658972", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Cardigans", "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Sets & Dresses", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/dark-olive-basic-dress-328062.jpg?v=1749147053", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Pants", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/BeigeCrochetPant_1.jpg?v=1746657631", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Skirts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/mist-rd-skirt-663918.jpg?v=1746660639", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Women's Home-wear", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/checkered-29-home-pants-w-292383.jpg?v=1746662229", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Boys' Clothing", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/lentil-kids-linen-shirt-904488.jpg?v=1749242051", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Girls' Clothing", "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Fashion Bags", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/royal-blue-waist-bag-985428.jpg?v=1746659384", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Fashion Belts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/lilac-design-2-belt-165538.jpg?v=1746663213", new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

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
