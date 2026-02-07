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
using Resend;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
var connStrng = Environment.GetEnvironmentVariable("CONNECTION_STR");
var port = Environment.GetEnvironmentVariable("PORT");
var resend = Environment.GetEnvironmentVariable("RESEND_KEY");

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

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
builder.Services.AddEndpointsApiExplorer();

//Servicios
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IVehiclesService, VehiclesService>();
builder.Services.AddScoped<IEmailService, EmailService>();


//Razor
builder.Services.AddRazorPages();

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
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

//HTTPS
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

//RESEND
builder.Services.AddOptions();
builder.Services.AddHttpClient<ResendClient>();
builder.Services.Configure<ResendClientOptions>(options =>
{
    options.ApiToken = resend!;
});
builder.Services.AddTransient<IResend, ResendClient>();

var app = builder.Build();
app.UseForwardedHeaders();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAngularApp");

app.UseSwagger();
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Mobi API v1");
    });

}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapRazorPages();
app.MapGet("/", () => Results.Redirect("/Index"));

app.Run();