using Microsoft.EntityFrameworkCore;
using LoginProject.Models;
using System.Data.SqlTypes;
using LoginProject.Services.Contract;
using LoginProject.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LogindbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlString"));
});

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
