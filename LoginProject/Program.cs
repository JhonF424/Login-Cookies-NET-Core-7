using Microsoft.EntityFrameworkCore;
using LoginProject.Models;
using LoginProject.Services.Contract;
using LoginProject.Services.Implementation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LogindbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlString"));
});

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opts =>
{
    opts.LoginPath = "/Start/Login";
    opts.ExpireTimeSpan= TimeSpan.FromMinutes(30);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Start}/{action=Login}/{id?}");

app.Run();
