using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PsvManager.Infrastructure.Data.Contexts;
using PsvManager.Infrastructure.Data.Interfaces;
using PsvManager.Infrastructure.Data.Repos;
using PsvManagerAPI.Core.Interfaces;
using PsvManagerAPI.Core.Services;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "PsvManagerAPI", Version = "v1" });
    c.EnableAnnotations();
});

// Add logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddDbContext<PsvContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PsvContext"));
});

// Add driver repository to the container.
builder.Services.AddScoped<IDriverRepository, DriverRepository>();

// Add driver service to the container.
builder.Services.AddScoped<IDriverService, DriverService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PsvManagerAPI v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
