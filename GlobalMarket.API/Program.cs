using System.Globalization;
using GlobalMarket.Core.Repository;
using GlobalMarket.Core.Services;
using GlobalMarket.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("GlobalMarket.Core")));

builder.Services.AddScoped<IManufatureService, ManufactureService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IShopService, ShopService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseMiddleware<>
app.MapControllers();

app.Run();
