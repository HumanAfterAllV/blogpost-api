using BlogPost.Api.Models;

namespace BlogPost.Api.Interfaces;

public interface IPostRepository
{
    //Obtener todos los post
    Task<IEnumerable<Post>> GetAllAsync();

    // Obtener un post por su Id
    Task<Post?> GetByIdAsync(Guid id);

    // Crear un nuevo post
    Task CreateAsync(Post post);

    // Actualizar un post existente
    Task UpdateAsync(Post post);

    // Eliminar un post
    Task DeleteAsync(Post post);
}