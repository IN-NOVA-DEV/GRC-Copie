//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorPages();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

//app.Run();

using grc_copie.Data;
using grc_copie.Service;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using grc_copie.Controllers;
using grc_copie.Controllers.Tools;

var result = "".Split(",");
bool EnLocal = false;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 524288000;
});
string AdminGroup = "e2a22365-defc-492d-a2b7-2d8f686d5695";
string UserGroup = "75e25ce6-0a87-413e-9cb4-954c2bc0cbcf";
string ModeratorGroup = "f2879207-c3cb-4d56-9fac-409badd5b2f7";
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", p =>
    {
        p.RequireClaim("groups", AdminGroup); //Admin
    });
    options.AddPolicy("Admin-Moderator", p =>
    {
        p.RequireClaim("groups", ModeratorGroup, AdminGroup); //Admin, Moderateur
    });
    options.AddPolicy("ModeratorOnly", p =>
    {
        p.RequireClaim("groups", ModeratorGroup); //Moderateur
    });
    options.AddPolicy("User", p =>
    {
        p.RequireClaim("groups", UserGroup); //User
    });
    options.AddPolicy("Admin-Moderator-User", p =>
    {
        p.RequireClaim("groups", ModeratorGroup, AdminGroup, UserGroup); //Admin, Moderateur, User
    });
});

builder.Services.AddDbContext<GRC_Context>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString((EnLocal) ? "LocalHost" : "DefaultConnection")
    ));


builder.Services.AddScoped<HomeService>();
builder.Services.AddScoped<Middleware>();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<JobNameActionFilter>();
});
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

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
