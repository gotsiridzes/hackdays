using Koggo.Application.Configuration;
using Koggo.Application.Services.Implementation;
using Koggo.Application.Services.Interface;
using Koggo.Client.Extensions;
using Koggo.Domain.Models;
using Koggo.Infrastructure;
using Koggo.Infrastructure.Implementation;
using Koggo.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var configuration = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<KoggoDbContext>(
       options => options.UseSqlServer(configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IRepository<Reservation>, Repository<Reservation>>();
builder.Services.AddScoped<IRepository<Service>, Repository<Service>>();
builder.Services.AddScoped<IRepository<UserService>, Repository<UserService>>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddDataProtection();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.OptionName));
builder.Services.AddJwtAuth(builder.Configuration.GetSection(JwtOptions.OptionName).Get<JwtOptions>());

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();