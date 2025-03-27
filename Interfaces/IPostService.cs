using BlogPost.Api.DTOs;

namespace BlogPost.Api.Interfaces
{
    public interface IPostService
    {
        // Obtener lista de posts (para mostrar en frontend)
        Task<IEnumerable<PostDto>> GetPostsAsync();

        // Obtener un post específico por Id
        Task<PostDto?> GetPostByIdAsync(Guid id);

        // Crear un nuevo post desde un DTO específico
        Task<PostDto> CreatePostAsync(CreatePostDto dto);

        // Actualizar un post por su Id con un DTO específico
        Task<bool> UpdatePostAsync(Guid id, UpdatePostDto dto);

        // Eliminar un post específico por Id
        Task<bool> DeletePostAsync(Guid id);
    }
}
