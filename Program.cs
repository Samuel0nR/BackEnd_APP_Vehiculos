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

builder.Services.AddRazorPages();

builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Vehicles API",
        Version = "v1"
    });
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAngularApp");
app.UseSwagger();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.MapRazorPages();
app.MapGet("/", () => Results.Redirect("/Index"));

app.Run();