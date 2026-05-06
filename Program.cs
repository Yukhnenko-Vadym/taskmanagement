using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using TaskManagementApi.Data;
using TaskManagementApi.Mappers;
using TaskManagementApi.Repositories.Implementations;
using TaskManagementApi.Repositories.Interfaces;
using TaskManagementApi.Services.Implementations;
using TaskManagementApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// ================= DATABASE =================
var connectionString = configuration.GetConnectionString("TMConnectionString");

services.AddDbContext<TMContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));

// ================= BASIC =================
services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
services.AddEndpointsApiExplorer();

// ================= SWAGGER =================
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskManagement API",
        Version = "v1",
        Description = "API for managing tasks, projects, and users"
    });

    // JWT AUTH IN SWAGGER
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter: Bearer {your JWT token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ================= AUTOMAPPER =================
services.AddAutoMapper(cfg => { },
    typeof(TaskItemProfile),
    typeof(ProjectProfile),
    typeof(UserProfile));

// ================= DEPENDENCY INJECTION =================
services.AddTransient<IProjectRepository, ProjectRepo>();
services.AddTransient<IProjectService, ProjectService>();

services.AddTransient<IUserRepository, UserRepo>();
services.AddTransient<IUserService, UserService>();

services.AddTransient<ITaskItemRepository, TaskItemRepo>();
services.AddTransient<ITaskItemService, TaskItemService>();

services.AddScoped<ITokenService, TokenService>();

// ================= JWT AUTH =================
var jwtSection = configuration.GetSection("Jwt");

var jwtKey = jwtSection["Key"];
var jwtIssuer = jwtSection["Issuer"];
var jwtAudience = jwtSection["Audience"];

if (string.IsNullOrWhiteSpace(jwtKey))
    throw new InvalidOperationException("JWT Key is not configured. Set Jwt:Key in configuration.");

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };
    });

services.AddAuthorization();

// ================= APP =================
var app = builder.Build();

// Swagger only in dev (clean setup)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagement API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();