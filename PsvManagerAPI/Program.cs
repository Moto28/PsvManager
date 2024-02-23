using Microsoft.EntityFrameworkCore;
using PsvManager.Infrastructure.Data.Contexts;
using PsvManager.Infrastructure.Data.Entities;
using PsvManager.Infrastructure.Data.Interfaces;
using PsvManager.Infrastructure.Data.Repos;
using PsvManagerAPI.Core.Interfaces;
using PsvManagerAPI.Core.Services;
using Microsoft.Extensions.Logging; // Add this line

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add logging
builder.Logging.AddConsole();

builder.Services.AddDbContext<PsvContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PsvContext"));
});

// Add driver repository to the container.
builder.Services.AddScoped<IDriverRepository,DriverRepository>();

// Add driver service to the container.
builder.Services.AddScoped<IDriverService, DriverService>();

// Add a configuration here that gets the connection string via user secrets.


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();    
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
