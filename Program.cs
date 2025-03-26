using BlogPost.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// A�adir soporte para controladores
builder.Services.AddControllers();

// Configurar DBContext (en appsettings.json configuramos la conexi�n)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar AutoMapper (lo configuramos luego)
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Configuraci�n de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware para Swagger en desarrollo (y producci�n, por ahora)
app.UseSwagger();
app.UseSwaggerUI();

// Middleware para manejo de excepciones (personalizado m�s adelante)
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
