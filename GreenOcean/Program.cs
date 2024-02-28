using GreenOcean.Data;
using GreenOcean.Helpers;
using GreenOcean.Interfaces;
using GreenOcean.Services;
using GreenOcean.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();


// Email Configuration
var emailSettings = new EmailSettings();
var tokenSettings = new TokenSettings();
configuration.GetSection("TokenSettings").Bind(tokenSettings);
configuration.GetSection("EmailSettings").Bind(emailSettings);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();