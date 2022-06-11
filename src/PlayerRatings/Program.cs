using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using PlayerRatings.Localization;
using PlayerRatings.Models;
using PlayerRatings.Repositories;
using PlayerRatings.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
}).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews().AddViewLocalization();

var configuration = builder.Configuration;
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"] ?? "unknown";
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"] ?? "unknown";
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Account/Login");
});

builder.Services.AddSingleton<IStringLocalizerFactory, CustomStringLocalizerFactory>();
builder.Services.AddSingleton<IStringLocalizer, CustomStringLocalizer>();

// Add application services.
builder.Services.AddTransient<IEmailSender, AuthMessageSender>();
builder.Services.AddTransient<ISmsSender, AuthMessageSender>();
builder.Services.AddTransient<ILanguageData, CustomStringLocalizerFactory>();
builder.Services.AddTransient<IInvitesService, InvitesService>();

builder.Services.AddTransient<ILeaguesRepository, LeaguesRepository>();

var app = builder.Build();

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
app.MapRazorPages();

app.Run();
