using BlogPost.Api.Interfaces;
using BlogPost.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Api.Data.Repositories;

public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _context;

    // Constructor con inyección de ApplicationDbContext
    public PostRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Obtener todos los posts, incluyendo comentarios
    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _context.Posts
            .Include(p => p.Comments)
            .ToListAsync();
    }

    // Obtener un post específico por su Id, incluyendo comentarios
    public async Task<Post?> GetByIdAsync(Guid id)
    {
        return await _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Crear un nuevo post
    public async Task CreateAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
    }

    // Actualizar un post existente
    public async Task UpdateAsync(Post post)
    {
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }

    // Eliminar un post
    public async Task DeleteAsync(Post post)
    {
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }
}
