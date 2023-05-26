
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using Microsoft.AspNetCore.Identity;
using Supermarket_Managment_System.Services.AuthService;
using Supermarket_Managment_System.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<db_context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("Connection string 'dbContext' not found.")));
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddIdentity<users, IdentityRole>()
    .AddEntityFrameworkStores<db_context>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;

});


//builder.Services.AddIdentity<users, IdentityRole>().AddEntityFrameworkStores<db_context>();

builder.Services.AddSession();

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
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Casher}/{action=Index}/{id?}");

app.Run();
