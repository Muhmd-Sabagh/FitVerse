using FitVerse.Web.Models; // Ensure this namespace is correct for FitVerseContext
//using FitVerse.Web.Repositories.Implementations;
//using FitVerse.Web.Repositories.Interfaces;
using FitVerse.Web.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FitVerse.Web.Repositories;

using FitVerse.Web.MapperConfig;
using FitVerse.Web.Repositories.Interfaces;
using FitVerse.Web.Repositories.Implementations; // Required for AutoMapper
//using FitVerse.Web.Mappers; // Your AutoMapper profile namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Entity Framework Core with FitVerseContext
builder.Services.AddDbContext<FitVerseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Authentication (Cookie-based authentication)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Path to your login action
        options.LogoutPath = "/Account/Logout"; // Path to your logout action
        options.AccessDeniedPath = "/Account/AccessDenied"; // Path if user tries to access unauthorized resource
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie expiration
        options.SlidingExpiration = true; // Renew cookie if half the expire time has passed
    });

// Add Session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Make the session cookie essential
});


//// Register Repositories with Dependency Injection
builder.Services.AddScoped<UnitOfWork, UnitOfWork>();
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Register Generic Repository
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<CartItemRepository, CartItemRepository>();
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
//builder.Services.AddScoped<IBannerRepository, BannerRepository>(); // Register Banner Repository


//// Configure AutoMapper
//// Scans the assembly for profiles and adds them.
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Use session middleware (must be before UseAuthentication/UseAuthorization)

app.UseAuthentication(); // Use authentication middleware
app.UseAuthorization();  // Use authorization middleware

// Apply migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<FitVerseContext>(); // Use FitVerseContext
        context.Database.Migrate(); // Apply any pending migrations
        // SeedData is now called directly from OnModelCreating in FitVerseContext,
        // so explicit SeedData.Initialize is not needed here anymore unless you have
        // complex seeding logic requiring service resolution.
        // If you had SeedData.Initialize, you'd remove it and let EF Core handle it via HasData.
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred applying migrations or seeding the DB.");
    }
}


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
