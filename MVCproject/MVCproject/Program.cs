using Microsoft.EntityFrameworkCore;
using MVCproject.Data;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------------
// Service Registration (Dependency Injection Container)
// -----------------------------------------------------------

// MVC Controllers + Razor Views
builder.Services.AddControllersWithViews();

// Entity Framework Core DbContext (SQL Server)
builder.Services.AddDbContext<MVCDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Session state management
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);   // Auto-expire inactive sessions
    options.Cookie.HttpOnly = true;                   // Prevent client-side access
    options.Cookie.IsEssential = true;                // Required for login/session flow
});

var app = builder.Build();

// -----------------------------------------------------------
// Middleware Pipeline Configuration
// -----------------------------------------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Enable HSTS for production
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// NOTE: No authentication middleware used here (manual login flow)
app.UseAuthorization();

// Enable session handling before MVC executes
app.UseSession();

// Default Route → Login page
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}"
);

app.Run();
