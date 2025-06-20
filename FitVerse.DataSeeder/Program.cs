using FitVerse.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace FitVerse.DataSeeder
{
    // --- External Product Source JSON Deserialization DTOs ---
    public class ProductSourceRoot
    {
        [JsonPropertyName("products")]
        public List<ProductSourceDto> Products { get; set; } = new List<ProductSourceDto>();
    }

    public class ProductSourceDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("body_html")]
        public string BodyHtml { get; set; } = string.Empty;

        [JsonPropertyName("published_at")]
        public DateTime? PublishedAt { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("variants")]
        public List<ProductSourceVariantDto> Variants { get; set; } = new List<ProductSourceVariantDto>();

        [JsonPropertyName("images")]
        public List<ProductSourceImageDto> Images { get; set; } = new List<ProductSourceImageDto>();
    }

    public class ProductSourceVariantDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; } = "0.00";

        [JsonPropertyName("compare_at_price")]
        public string? CompareAtPrice { get; set; }

        [JsonPropertyName("available")]
        public bool Available { get; set; }
    }

    public class ProductSourceImageDto
    {
        [JsonPropertyName("src")]
        public string Src { get; set; } = string.Empty;
    }

    // --- Main Program Logic ---
    public class Program
    {
        private static readonly Random random = new Random();

        public static async Task Main(string[] args)
        {
            // --- Configuration ---
            const string connectionString = "Server=MUHMD-SABAGH-PC\\SQLEXPRESS;Database=FitVerse;Trusted_Connection=True;TrustServerCertificate=True;";
            
            // Define external product collection URLs and their corresponding FitVerse Category unique keys
            var productCollectionMappings = new List<(string url, string categoryUniqueKey)>
            {
                ("https://khotwh.com/collections/t-shirt/products.json", "Men_T-Shirts"),
                ("https://khotwh.com/collections/sweatshirt-men/products.json", "Men_Sweatshirts"),
                ("https://khotwh.com/collections/men-pants/products.json", "Men_Pants"),
                ("https://khotwh.com/collections/shorts/products.json", "Men_Shorts"),
                ("https://khotwh.com/collections/shirt-men/products.json", "Men_Shirts"),
                ("https://khotwh.com/collections/pulloverm/products.json", "Men_Pullovers"),
                ("https://khotwh.com/collections/t-shirt-women/products.json", "Women_T-Shirts"),
                ("https://khotwh.com/collections/women-jacket/products.json", "Women_Jackets"),
                ("https://khotwh.com/collections/women-sweatshirt/products.json", "Women_Sweatshirts"),
                ("https://khotwh.com/collections/shirt-blouse/products.json", "Women_Shirts & Blouses"),
                ("https://khotwh.com/collections/pullover/products.json", "Women_Pullovers"),
                ("https://khotwh.com/collections/dress/products.json", "Women_Sets & Dresses"),
                ("https://khotwh.com/collections/pants/products.json", "Women_Pants"),
                ("https://khotwh.com/collections/skirt-dress/products.json", "Women_Skirts"),
                ("https://khotwh.com/collections/home-pant/products.json", "Women_Home-wear"),
                ("https://khotwh.com/collections/kids/products.json", "Kids_Boys"), 
                ("https://khotwh.com/collections/bags/products.json", "Accessories_Bags"),
                ("https://khotwh.com/collections/belt/products.json", "Accessories_Belts")
            };


            // --- Setup DbContextOptions ---
            var optionsBuilder = new DbContextOptionsBuilder<FitVerseContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // --- HTTP Client Setup with Browser-like Headers ---
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Starting data seeding process...");

            try
            {
                using (var context = new FitVerseContext(optionsBuilder.Options))
                {
                    // --- STATIC DATA SEEDING (ORDER IS CRUCIAL) ---
                    DateTime fixedUtcDateBase = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Utc);

                    // 1. Categories (Products depend on Categories)
                    var categoriesToSeed = new List<Category>
                    {
                        new Category { Name = "Men", Description = "Men's Clothing", ImageUrl = "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                        new Category { Name = "Women", Description = "Women's Clothing", ImageUrl = "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                        new Category { Name = "Kids", Description = "Kids' Clothing", ImageUrl = "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                        new Category { Name = "Accessories", Description = "Fashion Accessories", ImageUrl = "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image", IsActive = true, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase }
                    };

                    var subCategoriesData = new List<(string name, string parentName, string description, string imageUrl)>
                    {
                        ("T-Shirts", "Men", "Men's T-Shirts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/dark-emerald-design-3868vig-zipper-squares-polo-509857.jpg?v=1747904939"),
                        ("Jackets", "Men", "Men's Jackets", "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image"),
                        ("Sweatshirts", "Men", "Men's Sweatshirts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/beige-hoodie-641391.jpg?v=1746659214"),
                        ("Shirts", "Men", "Men's Shirts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/violet-oxford-shirt-121545.jpg?v=1747153870"),
                        ("Pullovers", "Men", "Men's Pullovers", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/camel-design-p2202-pf-round-pullover-369590.jpg?v=1746658721"),
                        ("Pants", "Men", "Men's Pants", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/cloud-soft-pant-307024.jpg?v=1749593291"),
                        ("Shorts", "Men", "Men's Shorts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/smoke-green-zipper-melton-short-530709.jpg?v=1748024610"),

                        ("T-Shirts", "Women", "Women's T-Shirts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/silver-curved-long-sleeve-629162.jpg?v=1747153907"),
                        ("Jackets", "Women", "Women's Jackets", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/camel-velvet-vest-design-4-192959.jpg?v=1746659011"),
                        ("Sweatshirts", "Women", "Women's Sweatshirts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/web_65.jpg?v=1746658520"),
                        ("Shirts & Blouses", "Women", "Women's Shirts & Blouses", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/olive-linen-pocket-over-size-shirt-211139.jpg?v=1749593439"),
                        ("Pullovers", "Women", "Women's Pullovers", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/wood-hoodie-pullover-163921.jpg?v=1746658972"),
                        ("Cardigans", "Women", "Women's Cardigans", "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image"),
                        ("Sets & Dresses", "Women", "Women's Sets & Dresses", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/dark-olive-basic-dress-328062.jpg?v=1749147053"),
                        ("Pants", "Women", "Women's Pants", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/BeigeCrochetPant_1.jpg?v=1746657631"),
                        ("Skirts", "Women", "Women's Skirts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/mist-rd-skirt-663918.jpg?v=1746660639"),
                        ("Home-wear", "Women", "Women's Home-wear", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/black-knitted-2-pieces-loungewear-set-152062.jpg?v=1746658252"),

                        ("Boys", "Kids", "Boys' Clothing", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/blue-jean-jacket-282664.jpg?v=1746657662"),
                        ("Girls", "Kids", "Girls' Clothing", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/gray-knitted-cropped-cardigan-201726.jpg?v=1747805177"),

                        ("Bags", "Accessories", "Fashion Bags", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/brown-crochet-bag-373030.jpg?v=1746657644"),
                        ("Belts", "Accessories", "Fashion Belts", "https://cdn.shopify.com/s/files/1/0614/4222/8407/files/black-leather-belt-153401.jpg?v=1747154030")
                    };

                    if (!await context.Categories.AnyAsync())
                    {
                        Console.WriteLine("Seeding Categories...");
                        await context.Categories.AddRangeAsync(categoriesToSeed);
                        await context.SaveChangesAsync(); // Save main categories to get their auto-generated IDs

                        // Retrieve the IDs of the newly inserted main categories for mapping
                        var savedMainCategories = await context.Categories
                                                        .Where(c => c.ParentCategoryId == null)
                                                        .ToDictionaryAsync(c => c.Name);

                        // Now add subcategories, setting their ParentCategoryId using the retrieved IDs
                        foreach (var subCatData in subCategoriesData)
                        {
                            if (savedMainCategories.TryGetValue(subCatData.parentName, out var parentCategory))
                            {
                                context.Categories.Add(new Category
                                {
                                    Name = subCatData.name,
                                    Description = subCatData.description,
                                    ImageUrl = subCatData.imageUrl,
                                    IsActive = true,
                                    ParentCategoryId = parentCategory.Id,
                                    CreatedAt = fixedUtcDateBase,
                                    UpdatedAt = fixedUtcDateBase
                                });
                            }
                        }
                        await context.SaveChangesAsync(); // Save subcategories
                        Console.WriteLine("Categories seeded.");
                    }

                    // --- Create a comprehensive category lookup AFTER all categories are seeded ---
                    var allCategoriesLookup = new Dictionary<string, Category>();
                    var categoriesFromDb = await context.Categories.Include(c => c.ParentCategory).ToListAsync();
                    foreach (var cat in categoriesFromDb)
                    {
                        string uniqueKey = cat.ParentCategory != null ? $"{cat.ParentCategory.Name}_{cat.Name}" : cat.Name;
                        allCategoriesLookup[uniqueKey] = cat;
                    }


                    // 2. Users (CartItems and Orders depend on Users)
                    var usersToSeed = new List<User>
                    {
                        new User
                        {
                            FullName = "Admin1", Email = "admin1@fitverse.com", PasswordHash = HashPassword("Admin1Password"),
                            Role = "Admin", CreatedAt = fixedUtcDateBase.AddDays(-30), UpdatedAt = fixedUtcDateBase.AddDays(-10)
                        },
                        new User
                        {
                            FullName = "Admin2", Email = "admin2@fitverse.com", PasswordHash = HashPassword("Admin2Password"),
                            Role = "Admin", CreatedAt = fixedUtcDateBase.AddDays(-25), UpdatedAt = fixedUtcDateBase.AddDays(-5)
                        },
                        new User
                        {
                            FullName = "Test User One", Email = "test1@fitverse.com", PasswordHash = HashPassword("password123"),
                            Role = "Customer", CreatedAt = fixedUtcDateBase.AddDays(-30), UpdatedAt = fixedUtcDateBase.AddDays(-10)
                        },
                        new User
                        {
                            FullName = "Test User Two", Email = "test2@fitverse.com", PasswordHash = HashPassword("securepass"),
                            Role = "Customer", CreatedAt = fixedUtcDateBase.AddDays(-25), UpdatedAt = fixedUtcDateBase.AddDays(-5)
                        }
                    };
                    if (!await context.Users.AnyAsync())
                    {
                        Console.WriteLine("Seeding Users...");
                        await context.Users.AddRangeAsync(usersToSeed);
                        await context.SaveChangesAsync();
                        Console.WriteLine("Users seeded.");
                    }
                    var savedUsers = await context.Users.ToDictionaryAsync(u => u.Email);


                    // 3. Banners
                    var bannersToSeed = new List<Banner>
                    {
                        new Banner { Title = "Summer Sale!", Description = "Up to 50% off on all summer collections.", ImageUrl = "https://placehold.co/1920x600/e9ecef/6c757d?text=Summer+Sale+Banner", LinkUrl = "/Products?IsOnSale=true", DisplayOrder = 1, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase },
                        new Banner { Title = "New Arrivals", Description = "Check out our latest fashion items.", ImageUrl = "https://placehold.co/1920x600/e9ecef/6c757d?text=New+Arrivals+Banner", LinkUrl = "/Products?IsNewArrival=true", DisplayOrder = 2, CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase }
                    };
                    if (!await context.Banners.AnyAsync())
                    {
                        Console.WriteLine("Seeding Banners...");
                        await context.Banners.AddRangeAsync(bannersToSeed);
                        await context.SaveChangesAsync();
                        Console.WriteLine("Banners seeded.");
                    }


                    // --- DYNAMIC PRODUCT DATA SEEDING & DISCOUNT APPLICATION ---
                    List<Product> allImportedProducts = new List<Product>();

                    if (!await context.Products.AnyAsync())
                    {
                        Console.WriteLine("Importing products from external JSON sources...");
                        var newlyAddedProductsToContext = new List<Product>();
                        int totalSkippedProducts = 0;

                        var existingProductTitlesInDb = await context.Products.Select(p => p.Name).ToHashSetAsync();

                        foreach (var mapping in productCollectionMappings)
                        {
                            var (url, categoryUniqueKey) = mapping;

                            if (!allCategoriesLookup.TryGetValue(categoryUniqueKey, out var category))
                            {
                                Console.WriteLine($"  Warning: Category with unique key '{categoryUniqueKey}' not found in FitVerse database. Skipping products from URL '{url}'.");
                                continue;
                            }
                            int fitVerseCategoryId = category.Id;

                            try
                            {
                                Console.WriteLine($"  Fetching products from: {url}");
                                var productRoot = await httpClient.GetFromJsonAsync<ProductSourceRoot>(url);

                                if (productRoot?.Products != null && productRoot.Products.Any())
                                {
                                    foreach (var sourceProduct in productRoot.Products)
                                    {
                                        if (!existingProductTitlesInDb.Contains(sourceProduct.Title))
                                        {
                                            var newProduct = MapToFitVerseProduct(sourceProduct, fitVerseCategoryId);
                                            context.Products.Add(newProduct);
                                            newlyAddedProductsToContext.Add(newProduct);
                                            existingProductTitlesInDb.Add(sourceProduct.Title);
                                        }
                                        else
                                        {
                                            Console.WriteLine($"    Skipped existing product: '{sourceProduct.Title}' from URL '{url}' (already exists or added from another source).");
                                            totalSkippedProducts++;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"  No products found or failed to deserialize from {url}.");
                                }
                            }
                            catch (HttpRequestException httpEx)
                            {
                                Console.Error.WriteLine($"  HTTP Request Error for {url}: {httpEx.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.Error.WriteLine($"  Error processing URL {url}: {ex.Message}");
                            }
                        }

                        if (newlyAddedProductsToContext.Any())
                        {
                            Console.WriteLine($"Applying random discounts to ~40 products from {newlyAddedProductsToContext.Count} newly imported products...");
                            var productsToDiscount = newlyAddedProductsToContext
                                .OrderBy(p => Guid.NewGuid())
                                .Take(Math.Min(40, newlyAddedProductsToContext.Count))
                                .ToList();

                            foreach (var product in productsToDiscount)
                            {
                                product.DiscountPercentage = random.Next(5, 16);
                                if (!product.IsActive && product.DiscountPercentage > 0)
                                {
                                    product.IsActive = true;
                                }
                                Console.WriteLine($"  Product '{product.Name}' discounted by {product.DiscountPercentage}%");
                            }
                            Console.WriteLine($"Applied discounts to {productsToDiscount.Count} products.");
                        }

                        if (newlyAddedProductsToContext.Any())
                        {
                            await context.SaveChangesAsync();
                            allImportedProducts.AddRange(newlyAddedProductsToContext);
                            Console.WriteLine($"Successfully saved {newlyAddedProductsToContext.Count} new products to the database.");
                        }
                        Console.WriteLine($"Dynamic product import finished. Total Imported: {newlyAddedProductsToContext.Count}, Total Skipped: {totalSkippedProducts}");
                    }
                    else
                    {
                        Console.WriteLine("Products already exist in the database. Skipping dynamic import and discount application.");
                        allImportedProducts.AddRange(await context.Products.ToListAsync());
                    }


                    // --- DEPENDENT DATA SEEDING ---
                    // 4. CartItems (Depends on Users and Products)
                    if (!await context.CartItems.AnyAsync() && savedUsers.Any() && allImportedProducts.Count >= 3) // Ensure at least 3 products exist
                    {
                        Console.WriteLine("Seeding Cart Items...");
                        var user1 = savedUsers.GetValueOrDefault("test1@example.com");
                        var user2 = savedUsers.GetValueOrDefault("test2@example.com");

                        // Use the first few available products from the allImportedProducts list
                        var prodForCart1 = allImportedProducts[0];
                        var prodForCart2 = allImportedProducts[1];
                        var prodForCart3 = allImportedProducts[2];

                        if (user1 != null && user2 != null)
                        {
                            await context.CartItems.AddRangeAsync(
                                new CartItem { UserId = user1.Id, ProductId = prodForCart1.Id, Quantity = 1, CreatedAt = fixedUtcDateBase.AddHours(-2), UpdatedAt = fixedUtcDateBase.AddHours(-2) },
                                new CartItem { UserId = user1.Id, ProductId = prodForCart2.Id, Quantity = 2, CreatedAt = fixedUtcDateBase.AddHours(-1), UpdatedAt = fixedUtcDateBase.AddHours(-1) },
                                new CartItem { UserId = user2.Id, ProductId = prodForCart3.Id, Quantity = 1, CreatedAt = fixedUtcDateBase.AddHours(-3), UpdatedAt = fixedUtcDateBase.AddHours(-3) }
                            );
                            await context.SaveChangesAsync();
                            Console.WriteLine("Cart Items seeded.");
                        }
                        else
                        {
                            Console.WriteLine("Not enough users (need 2 with emails 'test1@example.com', 'test2@example.com') to seed CartItems. Skipping.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Skipping Cart Items seeding: Not enough products (need at least 3) or users available in DB.");
                        Console.WriteLine($"  Debug: Users found: {savedUsers.Any()}, Products count: {allImportedProducts.Count}");
                    }

                    // 5. Orders (Depends on Users and Products for total calculation)
                    if (!await context.Orders.AnyAsync() && savedUsers.Any() && allImportedProducts.Count >= 3) // Ensure at least 3 products exist for order total example
                    {
                        Console.WriteLine("Seeding Orders...");
                        var user1 = savedUsers.GetValueOrDefault("test1@example.com");
                        var user2 = savedUsers.GetValueOrDefault("test2@example.com");

                        if (user1 != null && user2 != null)
                        {
                            // Retrieve products needed for order item calculations by their dynamic IDs
                            var p1Order = allImportedProducts[0];
                            var p2Order = allImportedProducts[1];
                            var p3Order = allImportedProducts[2];

                            decimal order1Total = (1 * p1Order.EffectivePrice) + (2 * p2Order.EffectivePrice);
                            decimal order2Total = (1 * p3Order.EffectivePrice);

                            await context.Orders.AddRangeAsync(
                                new Order
                                {
                                    UserId = user1.Id,
                                    OrderDate = fixedUtcDateBase.AddDays(-10),
                                    TotalAmount = order1Total,
                                    Status = "Delivered",
                                    ShippingAddress = "123 Main St, Anytown, Anystate 12345",
                                    CustomerName = "Test User One",
                                    CustomerEmail = user1.Email,
                                    CustomerPhone = "555-111-2222",
                                    CreatedAt = fixedUtcDateBase.AddDays(-10),
                                    UpdatedAt = fixedUtcDateBase.AddDays(-5)
                                },
                                new Order
                                {
                                    UserId = user2.Id,
                                    OrderDate = fixedUtcDateBase.AddDays(-7),
                                    TotalAmount = order2Total,
                                    Status = "Pending",
                                    ShippingAddress = "456 Oak Ave, Villageton, Stateland 67890",
                                    CustomerName = "Test User Two",
                                    CustomerEmail = user2.Email,
                                    CustomerPhone = "555-333-4444",
                                    CreatedAt = fixedUtcDateBase.AddDays(-7),
                                    UpdatedAt = fixedUtcDateBase.AddDays(-7)
                                }
                            );
                            await context.SaveChangesAsync();
                            Console.WriteLine("Orders seeded.");
                        }
                        else
                        {
                            Console.WriteLine("Not enough users (need 2 with emails 'test1@example.com', 'test2@example.com') to seed Orders. Skipping.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Skipping Orders seeding: Not enough products (need at least 3) or users available in DB.");
                        Console.WriteLine($"  Debug: Users found: {savedUsers.Any()}, Products count: {allImportedProducts.Count}");
                    }
                    // Reload orders after saving to get their auto-generated IDs
                    var savedOrdersList = await context.Orders.ToListAsync();


                    // 6. OrderItems (Depends on Orders and Products)
                    if (!await context.OrderItems.AnyAsync() && savedOrdersList.Count >= 2 && allImportedProducts.Count >= 3) // Ensure orders and products exist
                    {
                        Console.WriteLine("Seeding Order Items...");
                        var order1 = savedOrdersList.FirstOrDefault(o => o.UserId == savedUsers["test1@example.com"].Id);
                        var order2 = savedOrdersList.FirstOrDefault(o => o.UserId == savedUsers["test2@example.com"].Id);

                        var p1Order = allImportedProducts[0];
                        var p2Order = allImportedProducts[1];
                        var p3Order = allImportedProducts[2];

                        if (order1 != null && order2 != null)
                        {
                            await context.OrderItems.AddRangeAsync(
                                new OrderItem { OrderId = order1.Id, ProductId = p1Order.Id, Quantity = 1, UnitPrice = p1Order.EffectivePrice, CreatedAt = fixedUtcDateBase.AddDays(-10) },
                                new OrderItem { OrderId = order1.Id, ProductId = p2Order.Id, Quantity = 2, UnitPrice = p2Order.EffectivePrice, CreatedAt = fixedUtcDateBase.AddDays(-10) },
                                new OrderItem { OrderId = order2.Id, ProductId = p3Order.Id, Quantity = 1, UnitPrice = p3Order.EffectivePrice, CreatedAt = fixedUtcDateBase.AddDays(-7) }
                            );
                            await context.SaveChangesAsync();
                            Console.WriteLine("Order Items seeded.");
                        }
                        else
                        {
                            Console.WriteLine("Not enough specific orders found in DB to seed OrderItems. Skipping.");
                            Console.WriteLine($"  Debug: Order1 found: {order1 != null}, Order2 found: {order2 != null}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Skipping Order Items seeding: Not enough orders (need at least 2) or products (need at least 3) available in DB.");
                        Console.WriteLine($"  Debug: Orders count: {savedOrdersList.Count}, Products count: {allImportedProducts.Count}");
                    }

                    Console.WriteLine("\nAll data seeding process completed successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"\nAn error occurred during data seeding: {ex.Message}");
                Console.Error.WriteLine(ex.ToString()); // Log full exception details
            }

            Console.WriteLine("Press any key to exit the seeder.");
            Console.ReadKey();
        }

        // --- Helper Methods ---
        private static Product MapToFitVerseProduct(ProductSourceDto sourceProduct, int fitVerseCategoryId)
        {
            string? material = ExtractMaterialFromBodyHtml(sourceProduct.BodyHtml);
            if (string.IsNullOrEmpty(material))
            {
                material = "Mixed Fabric";
            }

            decimal price = 0m;
            var firstVariant = sourceProduct.Variants.FirstOrDefault();
            if (firstVariant != null)
            {
                if (decimal.TryParse(firstVariant.Price, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal parsedPrice))
                {
                    price = parsedPrice;
                }
            }

            decimal? discountPercentage = null;

            int stockQuantity = random.Next(0, 31);

            bool isNewArrival = false;
            if (sourceProduct.PublishedAt.HasValue)
            {
                if ((DateTime.UtcNow - sourceProduct.PublishedAt.Value.ToUniversalTime()).TotalDays <= 60)
                {
                    isNewArrival = true;
                }
            }

            string imageUrl = sourceProduct.Images.FirstOrDefault()?.Src ?? "https://placehold.co/400x250/e9ecef/6c757d?text=No+Image";

            return new Product
            {
                Name = sourceProduct.Title,
                Material = material,
                Description = StripHtmlTags(sourceProduct.BodyHtml),
                Price = price,
                DiscountPercentage = discountPercentage,
                IsNewArrival = isNewArrival,
                ImageUrl = imageUrl,
                StockQuantity = stockQuantity,
                CategoryId = fitVerseCategoryId,
                IsActive = stockQuantity > 0,
                CreatedAt = sourceProduct.CreatedAt?.ToUniversalTime() ?? DateTime.UtcNow,
                UpdatedAt = sourceProduct.UpdatedAt?.ToUniversalTime() ?? DateTime.UtcNow
            };
        }

        private static string? ExtractMaterialFromBodyHtml(string bodyHtml)
        {
            Match match = Regex.Match(bodyHtml, @"Made from ([\w\s\d%]+?)(?: cotton| polyester| spandex| silk| denim| viscose| fleece| nylon| fabric)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }

            match = Regex.Match(bodyHtml, @"Material:\s*([\w\s]+)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }
            return null;
        }

        private static string StripHtmlTags(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;
            return Regex.Replace(html, "<.*?>", string.Empty).Trim();
        }

        private static string HashPassword(string password)
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
    }
}
