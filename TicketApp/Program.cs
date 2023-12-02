using Microsoft.EntityFrameworkCore;
using TicketApp.Models;
using TicketApp.Repository;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// MUST BE CALLED before AddControllersWithViews
builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddTransient<ITicketRepository, TicketRepository>();
// Add services to the container.
builder.Services.AddControllersWithViews();
//Add EF Core Dependency Injection
builder.Services.AddDbContext<TicketContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TicketContext")));
//Add Identity Middleware
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<TicketContext>()
    .AddDefaultTokenProviders();

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
app.UseSession();
app.UseRouting();

//for Identity
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
