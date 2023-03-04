using Microsoft.EntityFrameworkCore;
using Onboarding.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    var connectionString = builder.Configuration["ConnectionString"];
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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