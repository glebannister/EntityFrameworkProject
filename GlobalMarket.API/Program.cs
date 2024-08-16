using System.Text;
using GlobalMarket.API.Middleware;
using GlobalMarket.Core.Models;
using GlobalMarket.Core.Repository;
using GlobalMarket.Core.Services;
using GlobalMarket.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionStringApplicationConfiguratio = builder.Configuration
    .GetConnectionString(Environment.GetEnvironmentVariable("APP_CONFIGURATION"));

builder.Configuration.AddAzureAppConfiguration(connectionStringApplicationConfiguratio);

var connectionStringDataBase = builder.Configuration.GetValue<string>("AppDbConnectionString");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionStringDataBase,
    builder => builder.MigrationsAssembly("GlobalMarket.API"))
);

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

builder.Services.AddOptions<JwtSettings>()
    .Bind(builder.Configuration.GetSection("JwtSettings"))
    .ValidateOnStart()
    .ValidateDataAnnotations();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromSeconds(0),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

builder.Services.AddScoped<IHashPasswordService, HashPasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IManufatureService, ManufactureService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddSingleton<ExceptionHandlerMiddleware>();

builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GlobalMarket",
        Version = "v1"
    });

    var securityScheme = new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme",
        Reference = new()
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme,
        }
    };

    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                securityScheme,
                Array.Empty<string>()
            }
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers()
   .RequireAuthorization();

app.Run();
