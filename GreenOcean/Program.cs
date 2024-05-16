using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using GreenOcean.Business.Interfaces;
using GreenOcean.Business.Services;
using GreenOcean.Business.Settings;
using GreenOcean.Data;
using GreenOcean.Data.Interfaces;
using GreenOcean.Data.Repositories;
using GreenOcean.Services;
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


// Configurations
var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
var tokenSettings = configuration.GetSection("TokenSettings").Get<TokenSettings>();
var awsSettings = configuration.GetSection("AWSSettings").Get<AWSSettings>();
builder.Services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
builder.Services.Configure<BasicPhotoSettings>(configuration.GetSection("BasicPhoto"));
builder.Services.Configure<EmailPathSettings>(configuration.GetSection("EmailPathSettings"));
builder.Services.Configure<EmailSubjectSettings>(configuration.GetSection("EmailSubjectSettings"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register dependencies
builder.Services.AddScoped<IEmailService>(serviceProvider =>
{
    return new EmailService(emailSettings);
});

builder.Services.AddScoped<ITokenService>(serviceProvider => {
    return new TokenService(tokenSettings);
});
builder.Services.AddScoped<ISettingPassword, SettingPassword>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<ISensorDataVerification, SensorDataVerification>();
//builder.Services.AddHostedService<SensorDataHostedService>();

var credentials = new BasicAWSCredentials(awsSettings.AccesKey, awsSettings.SecretAccesKey);
var config = new AmazonDynamoDBConfig()
{
    RegionEndpoint = RegionEndpoint.USEast1
};
var client = new AmazonDynamoDBClient(credentials, config);
builder.Services.AddSingleton<IAmazonDynamoDB>(client);
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

// Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

// Add service dependencies
builder.Services.AddScoped<IRegisteredEquipmentService, RegisteredEquipmentService>();
builder.Services.AddScoped<IValidatingAccountService, ValidatingAccountService>();
builder.Services.AddScoped<IResetingPasswordService, ResetingPasswordService>();
builder.Services.AddScoped<IGreenhouseService, GreenhouseService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ICreatingUserService, UserService>();
builder.Services.AddScoped<IPlantService, PlantService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IProcessService, ProcessService>();
builder.Services.AddScoped<IDataRepository, DataRepository>();

// Add repository dependencies
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ICodeRepository, CodeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRegisteredEquipmentRepository, RegisteredEquipmentRepository>();
builder.Services.AddScoped<IGreenhouseRepository, GreenhouseRepository>();
builder.Services.AddScoped<IPlantRepository, PlantRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IProcessRepository, ProcessRepository>();
builder.Services.AddScoped<IDataService, DataService>();

// Add authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MemberPolicy", policy =>
    {
        policy.RequireRole("Member");
    });

    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

// Verify the token
app.UseAuthentication();

// Verify the permissions of the token
app.UseAuthorization();
app.MapControllers();

app.Run();