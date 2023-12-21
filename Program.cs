using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThesisOct2023.Data;
using ThesisOct2023.Models;
using ThesisOct2023.Repositories;

var builder = WebApplication.CreateBuilder(args);

//CONNECTION
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//IDENTITY AUTH
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

//PLAIN
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

//SCOPED
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewQuestionRepository, ReviewQuestionRepository>();

builder.Services.ConfigureApplicationCookie(options =>
{
    //Location for your Custom Access Denied Page
    options.AccessDeniedPath = "/Account/AccessDenied";

    //Location for your Custom Login Page
    options.LoginPath = "/Account/Login";
});


var app = builder.Build();
app.UseStaticFiles();
app.UseSession();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseStatusCodePages();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseStatusCodePages();
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
app.Seed();
app.Run();
