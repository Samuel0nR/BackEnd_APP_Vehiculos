using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using api_dotNet_vehicles.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Env.Load();
var connStrng = Environment.GetEnvironmentVariable("CONNECTION_STR");
var port = Environment.GetEnvironmentVariable("PORT");

builder.Services.AddDbContext<DBContext>(option => option.UseMySql(connStrng, ServerVersion.AutoDetect(connStrng)));

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAngularApp", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowAngularApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();