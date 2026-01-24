using System.Text;
using API_VehiclesAPP.Configurations;
using API_VehiclesAPP.Data;
using API_VehiclesAPP.Services;
using API_VehiclesAPP.Services.Interfaces;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
var connStrng = Environment.GetEnvironmentVariable("CONNECTION_STR");
var port = Environment.GetEnvironmentVariable("PORT");

//builder.Services.AddDbContext<DBContext>(option => option.UseMySql(connStrng, ServerVersion.AutoDetect(connStrng))); //MySQL
builder.Services.AddDbContext<DBContext>(option => option.UseSqlServer(connStrng)); //SQLServer

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

//Servicios
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IVehiclesService, VehiclesService>();


//Razor
builder.Services.AddRazorPages();

//Swagger
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My Mobi API",
        Version = "v1"
    });
});

//JWT
builder.Services.Configure<JwtConfig>(options =>
{
    options.Key = Environment.GetEnvironmentVariable("JWT_KEY")
        ?? throw new Exception("JWT_KEY no configurado");

    options.Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")
        ?? throw new Exception("JWT_ISSUER no configurado");

    options.Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
        ?? throw new Exception("JWT_AUDIENCE no configurado");
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
            ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    Environment.GetEnvironmentVariable("JWT_KEY")!
                    )
                ),

            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddAuthorization();


builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseForwardedHeaders();

app.UseCors("AllowAngularApp");
app.UseSwagger();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapRazorPages();
app.MapGet("/", () => Results.Redirect("/Index"));

app.Run();