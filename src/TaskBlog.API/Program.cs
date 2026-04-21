using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskBlog.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
