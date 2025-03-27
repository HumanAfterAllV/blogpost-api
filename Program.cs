using BlogPost.Api.Data;
using BlogPost.Api.Interfaces;
using BlogPost.Api.Data.Repositories;
using BlogPost.Api.Services;
using BlogPost.Api.Mappings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Añadir soporte para controladores
builder.Services.AddControllers();

// Configurar DBContext (en appsettings.json configuramos la conexión)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.MapControllers();

app.Run();
