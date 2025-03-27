using BlogPost.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Api.Data;

// Contexto principal de la base de datos usando Entity Framework Core
public class ApplicationDbContext : DbContext
{
    // Constructor que recibe opciones de configuración (ej. cadena de conexión)
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Tabla Posts en la base de datos
    public DbSet<Post> Posts { get; set; }

    // Tabla Comments en la base de datos
    public DbSet<Comment> Comments { get; set; }

    // Método para configurar relaciones entre tablas
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relación clara entre Post y Comments (Un Post tiene muchos Comments)
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade); // al borrar un post, borra sus comentarios
    }
}

