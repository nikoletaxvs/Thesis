using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThesisOct2023.Data;
using ThesisOct2023.Models;
using ThesisOct2023.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultUI()
//    .AddDefaultTokenProviders();
// Configure Identity with a custom user class
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
var app = builder.Build();
app.UseStaticFiles();
app.UseSession();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute("catpage",
"{category}/Page{productPage:int}",
new { Controller = "Cook", action = "Food" });
app.MapControllerRoute("page", "Page{productPage:int}",
new { Controller = "Cook", action = "Food", productPage = 1 });
app.MapControllerRoute("category", "{category}",
new { Controller = "Cook", action = "Food", productPage = 1 });
app.MapControllerRoute("pagination",
"Food/Page{productPage}",
new
{
    Controller = "Cook",
    action = "Food",
    productPage = 1
});
app.MapRazorPages();
app.MapDefaultControllerRoute();
app.Run();
