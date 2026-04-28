using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using TaskManagementApi.Data;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Mappers;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories.Implementations;
using TaskManagementApi.Repositories.Interfaces;
using TaskManagementApi.Services.Implementations;
using TaskManagementApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("TMConnectionString");

services.AddDbContext<TMContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));

services.AddControllers();
services.AddEndpointsApiExplorer();

services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskManagement API",
        Version = "v1",
        Description = "API for managing tasks, projects, and users"
    });
});

services.AddSwaggerGen();
// services.AddAutoMapper(cfg => { }, typeof(TaskItemProfile));
// services.AddAutoMapper(cfg => { }, typeof(ProjectProfile));
// services.AddAutoMapper(cfg => { }, typeof(UserProfile));
services.AddAutoMapper(cfg => { },
    typeof(TaskItemProfile), typeof(ProjectProfile), typeof(UserProfile));

services.AddTransient<IProjectRepository, ProjectRepo>();
services.AddTransient<IProjectService, ProjectService>();

services.AddTransient<IUserRepository, UserRepo>();
services.AddTransient<IUserService, UserService>();

services.AddTransient<ITaskItemRepository, TaskItemRepo>();
services.AddTransient<ITaskItemService, TaskItemService>();

var app = builder.Build();

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
app.UseAuthorization();
app.MapControllers();

app.Run();




