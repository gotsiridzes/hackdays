using Koggo.Api.Extensions;
using Koggo.Application.Configuration;
using Koggo.Domain.Models;
using Koggo.Infrastructure;
using Koggo.Infrastructure.Implementation;
using Koggo.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.AddSecurityDefinition("jwt", new OpenApiSecurityScheme() {}));
builder.Services.AddDbContext<KoggoDbContext>(
       options => options.UseSqlServer("Server=L_STURASHVILI;Database=Koggo;Trusted_Connection=True;MultipleActiveResultSets=true; TrustServerCertificate=True"));

builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IRepository<Reservation>, Repository<Reservation>>();
builder.Services.AddScoped<IRepository<Service>, Repository<Service>>();
builder.Services.AddScoped<IRepository<UserService>, Repository<UserService>>();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.OptionName));
builder.Services.AddJwtAuth(builder.Configuration.GetSection(JwtOptions.OptionName).Get<JwtOptions>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();