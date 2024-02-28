using GreenOcean.Data;
using GreenOcean.Helpers;
using GreenOcean.Interfaces;
using GreenOcean.Services;
using GreenOcean.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();


// Email Configuration
var emailSettings = new EmailSettings();
configuration.GetSection("EmailSettings").Bind(emailSettings);

// Token Configuration
var tokenSettings = new TokenSettings();
configuration.GetSection("TokenSettings").Bind(tokenSettings);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register dependencies
builder.Services.AddScoped<ICreateUser>(serviceProvider =>
{
    return new EmailService(emailSettings);
});

builder.Services.AddScoped<ITokenService>(serviceProvider => {
    return new TokenService(tokenSettings);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.TokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200"));

// Veirfy the token
app.UseAuthentication();

// Verify the permissions of the token
app.UseAuthorization();
app.MapControllers();

app.Run();