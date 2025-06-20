using FitVerse.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;
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
            // --- Configuration Setup ---
            // Determine the current directory where the seeder executable is running.
            string currentDirectory = Directory.GetCurrentDirectory();

            // Navigate up to the solution root.
            string solutionRootDirectory = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", ".."));
            string webAppsettingsPath = Path.Combine(solutionRootDirectory, "FitVerse.Web", "appsettings.json");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(webAppsettingsPath, optional: false, reloadOnChange: true)
                .Build();

            // Retrieve the connection string from configuration
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.Error.WriteLine("Error: Connection string 'DefaultConnection' not found or is empty in FitVerse.Web/appsettings.json.");
                Console.WriteLine("Ensure the file exists and contains the 'DefaultConnection' string.");
                Console.WriteLine("Press any key to exit the seeder.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Using connection string from FitVerse.Web/appsettings.json: {connectionString}");

            // Setup ServiceCollection for DbContext and Identity services
            var services = new ServiceCollection();
            services.AddDbContext<FitVerseContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<FitVerseContext>()
                .AddDefaultTokenProviders();

            // Build the service provider
            using var serviceProvider = services.BuildServiceProvider();
            // Resolve UserManager and RoleManager from the service provider
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<FitVerseContext>();


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
                ("https://khotwh.com/collections/cardigan/products.json", "Women_Cardigans"),
                ("https://khotwh.com/collections/dress/products.json", "Women_Sets & Dresses"),
                ("https://khotwh.com/collections/pants/products.json", "Women_Pants"),
                ("https://khotwh.com/collections/skirt-dress/products.json", "Women_Skirts"),
                ("https://khotwh.com/collections/home-pant/products.json", "Women_Home-wear"),
                ("https://khotwh.com/collections/kids/products.json", "Kids_Boys"),
                ("https://khotwh.com/collections/bags/products.json", "Accessories_Bags"),
                ("https://khotwh.com/collections/belt/products.json", "Accessories_Belts")
            };


            // --- HTTP Client Setup with Browser-like Headers ---
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Starting data seeding process...");

            try
            {
                // Ensure Identity roles exist
                await SeedRolesAsync(roleManager);

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
                    await context.SaveChangesAsync();

                    var savedMainCategories = await context.Categories
                                                    .Where(c => c.ParentCategoryId == null)
                                                    .ToDictionaryAsync(c => c.Name);

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
                    await context.SaveChangesAsync();
                    Console.WriteLine("Categories seeded.");
                }

                var allCategoriesLookup = new Dictionary<string, Category>();
                var categoriesFromDb = await context.Categories.Include(c => c.ParentCategory).ToListAsync();
                foreach (var cat in categoriesFromDb)
                {
                    string uniqueKey = cat.ParentCategory != null ? $"{cat.ParentCategory.Name}_{cat.Name}" : cat.Name;
                    allCategoriesLookup[uniqueKey] = cat;
                }

                // 2. Users (Admins and Customers) - Using Identity UserManager
                ApplicationUser adminUser1 = null;
                ApplicationUser adminUser2 = null;
                ApplicationUser customerUser1 = null;
                ApplicationUser customerUser2 = null;

                if (!await userManager.Users.AnyAsync())
                {
                    Console.WriteLine("Seeding Users and Roles...");

                    // Create Roles if they don't exist
                    await SeedRolesAsync(roleManager);

                    // Create Admin Users (with default IDs 1 and 2 conceptually)
                    adminUser1 = new ApplicationUser { UserName = "admin1@fitverse.com", Email = "admin1@fitverse.com", FullName = "Admin User One", CreatedAt = fixedUtcDateBase, UpdatedAt = fixedUtcDateBase };
                    var result1 = await userManager.CreateAsync(adminUser1, "Admin1Pass!");
                    if (result1.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser1, "Admin");
                        Console.WriteLine($"  Created Admin: {adminUser1.Email}");
                    }
                    else
                    {
                        Console.WriteLine($"  Error creating Admin {adminUser1.Email}: {string.Join(", ", result1.Errors.Select(e => e.Description))}");
                    }

                    adminUser2 = new ApplicationUser { UserName = "admin2@fitverse.com", Email = "admin2@fitverse.com", FullName = "Admin User Two", CreatedAt = fixedUtcDateBase.AddHours(1), UpdatedAt = fixedUtcDateBase.AddHours(1) };
                    var result2 = await userManager.CreateAsync(adminUser2, "Admin2Pass!");
                    if (result2.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser2, "Admin");
                        Console.WriteLine($"  Created Admin: {adminUser2.Email}");
                    }
                    else
                    {
                        Console.WriteLine($"  Error creating Admin {adminUser2.Email}: {string.Join(", ", result2.Errors.Select(e => e.Description))}");
                    }

                    // Create Customer Users (with default IDs 3 and 4 conceptually)
                    customerUser1 = new ApplicationUser { UserName = "test1@fitverse.com", Email = "test1@fitverse.com", FullName = "Test User One", CreatedAt = fixedUtcDateBase.AddDays(-30), UpdatedAt = fixedUtcDateBase.AddDays(-10) };
                    var result3 = await userManager.CreateAsync(customerUser1, "Test1Pass!");
                    if (result3.Succeeded)
                    {
                        await userManager.AddToRoleAsync(customerUser1, "Customer");
                        Console.WriteLine($"  Created Customer: {customerUser1.Email}");
                    }
                    else
                    {
                        Console.WriteLine($"  Error creating Customer {customerUser1.Email}: {string.Join(", ", result3.Errors.Select(e => e.Description))}");
                    }

                    customerUser2 = new ApplicationUser { UserName = "test2@fitverse.com", Email = "test2@fitverse.com", FullName = "Test User Two", CreatedAt = fixedUtcDateBase.AddDays(-25), UpdatedAt = fixedUtcDateBase.AddDays(-5) };
                    var result4 = await userManager.CreateAsync(customerUser2, "Test2Pass!");
                    if (result4.Succeeded)
                    {
                        await userManager.AddToRoleAsync(customerUser2, "Customer");
                        Console.WriteLine($"  Created Customer: {customerUser2.Email}");
                    }
                    else
                    {
                        Console.WriteLine($"  Error creating Customer {customerUser2.Email}: {string.Join(", ", result4.Errors.Select(e => e.Description))}");
                    }

                    Console.WriteLine("Users and Roles seeded.");
                }
                else
                {
                    Console.WriteLine("Users already exist. Skipping user/role seeding.");
                    adminUser1 = await userManager.FindByEmailAsync("admin1@fitverse.com");
                    adminUser2 = await userManager.FindByEmailAsync("admin2@fitverse.com");
                    customerUser1 = await userManager.FindByEmailAsync("test1@fitverse.com");
                    customerUser2 = await userManager.FindByEmailAsync("test2@fitverse.com");
                }


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
                // Ensure customer users exist before seeding their cart/order data
                if (customerUser1 == null || customerUser2 == null)
                {
                    Console.WriteLine("Customer users not found/created. Skipping CartItems, Orders, OrderItems seeding.");
                }
                else
                {
                    // 4. CartItems (Depends on Users and Products)
                    if (!await context.CartItems.AnyAsync() && allImportedProducts.Count >= 3)
                    {
                        Console.WriteLine("Seeding Cart Items...");
                        var prodForCart1 = allImportedProducts[0];
                        var prodForCart2 = allImportedProducts[1];
                        var prodForCart3 = allImportedProducts[2];

                        await context.CartItems.AddRangeAsync(
                            new CartItem { UserId = customerUser1.Id, ProductId = prodForCart1.Id, Quantity = 1, CreatedAt = fixedUtcDateBase.AddHours(-2), UpdatedAt = fixedUtcDateBase.AddHours(-2) },
                            new CartItem { UserId = customerUser1.Id, ProductId = prodForCart2.Id, Quantity = 2, CreatedAt = fixedUtcDateBase.AddHours(-1), UpdatedAt = fixedUtcDateBase.AddHours(-1) },
                            new CartItem { UserId = customerUser2.Id, ProductId = prodForCart3.Id, Quantity = 1, CreatedAt = fixedUtcDateBase.AddHours(-3), UpdatedAt = fixedUtcDateBase.AddHours(-3) }
                        );
                        await context.SaveChangesAsync();
                        Console.WriteLine("Cart Items seeded.");
                    }
                    else
                    {
                        Console.WriteLine("Skipping Cart Items seeding: Not enough products (need at least 3) available in DB.");
                        Console.WriteLine($"  Debug: Products count: {allImportedProducts.Count}");
                    }

                    // 5. Orders (Depends on Users and Products for total calculation)
                    if (!await context.Orders.AnyAsync() && allImportedProducts.Count >= 3)
                    {
                        Console.WriteLine("Seeding Orders...");
                        var p1Order = allImportedProducts[0];
                        var p2Order = allImportedProducts[1];
                        var p3Order = allImportedProducts[2];

                        decimal order1Total = (1 * p1Order.EffectivePrice) + (2 * p2Order.EffectivePrice);
                        decimal order2Total = (1 * p3Order.EffectivePrice);

                        await context.Orders.AddRangeAsync(
                            new Order
                            {
                                UserId = customerUser1.Id,
                                OrderDate = fixedUtcDateBase.AddDays(-10),
                                TotalAmount = order1Total,
                                Status = "Delivered",
                                ShippingAddress = "123 Main St, Anytown, Anystate 12345",
                                CustomerName = customerUser1.FullName,
                                CustomerEmail = customerUser1.Email,
                                CustomerPhone = "555-111-2222",
                                CreatedAt = fixedUtcDateBase.AddDays(-10),
                                UpdatedAt = fixedUtcDateBase.AddDays(-5)
                            },
                            new Order
                            {
                                UserId = customerUser2.Id,
                                OrderDate = fixedUtcDateBase.AddDays(-7),
                                TotalAmount = order2Total,
                                Status = "Pending",
                                ShippingAddress = "456 Oak Ave, Villageton, Stateland 67890",
                                CustomerName = customerUser2.FullName,
                                CustomerEmail = customerUser2.Email,
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
                        Console.WriteLine("Skipping Orders seeding: Not enough products (need at least 3) available in DB.");
                        Console.WriteLine($"  Debug: Products count: {allImportedProducts.Count}");
                    }
                    var savedOrdersList = await context.Orders.Where(o => o.UserId == customerUser1.Id || o.UserId == customerUser2.Id).ToListAsync();


                    // 6. OrderItems (Depends on Orders and Products)
                    if (!await context.OrderItems.AnyAsync() && savedOrdersList.Count >= 2 && allImportedProducts.Count >= 3)
                    {
                        Console.WriteLine("Seeding Order Items...");
                        var order1 = savedOrdersList.FirstOrDefault(o => o.UserId == customerUser1.Id);
                        var order2 = savedOrdersList.FirstOrDefault(o => o.UserId == customerUser2.Id);

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
                }

                Console.WriteLine("\nAll data seeding process completed successfully!");
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
        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Customer" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    Console.WriteLine($"  Creating role: {roleName}");
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

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
    }
}
