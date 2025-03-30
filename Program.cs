using BlogPost.Api.Data;
using BlogPost.Api.Interfaces;
using BlogPost.Api.Data.Repositories;
using BlogPost.Api.Services;
using BlogPost.Api.Mappings;
using BlogPost.Api.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Añadir soporte para controladores
builder.Services.AddControllers();


// Configurar DBContext (en appsettings.json configuramos la conexión)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings!.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

builder.Services.AddHttpClient();

//Inyeccion de Dependencias
builder.Services.AddScoped<IPostRepository, PostRepository>();

// Configurar AutoMapper (lo configuramos luego)
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configuración de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware para Swagger en desarrollo (y producción, por ahora)
app.UseSwagger();
app.UseSwaggerUI();

// Middleware para manejo de excepciones (personalizado más adelante)
app.UseHttpsRedirection();


app.UseAuthorization();
app.UseAuthentication();

app.UseCors("AllowedFrontend");

app.MapControllers();

app.Run();
