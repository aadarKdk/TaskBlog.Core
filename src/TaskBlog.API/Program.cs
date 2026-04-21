using Microsoft.EntityFrameworkCore;
using TaskBlog.Application.Interfaces;
using TaskBlog.Application.Services;
using TaskBlog.Infrastructure.Data;
using TaskBlog.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

app.MapControllers();

app.Run();
