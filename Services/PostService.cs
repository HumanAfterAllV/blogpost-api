using AutoMapper;
using BlogPost.Api.DTOs;
using BlogPost.Api.Interfaces;
using BlogPost.Api.Models;

namespace BlogPost.Api.Services; 

//Implementación clara de la logica de negocio relacionada con Posts
public class PostService : IPostService
{

    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;

    // Constructor que recive dependencias necesarias
    public PostService(IPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // Obtener todos los posts
    public async Task<IEnumerable<PostDto>> GetPostsAsync()
    {
        // Obtenemos posts desde la base de datos
        var posts = await _repository.GetAllAsync();

        // Mapeamos a DTOs para protección y claridad
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<PostDto?> GetPostByIdAsync(Guid id)
    {
        var post = await _repository.GetByIdAsync(id);

        // Si no existe, regresamos null
        if (post is null || id == Guid.Empty)
            return null;

        // Retornamos el post convertido en DTO
        return _mapper.Map<PostDto>(post);
    }

    // Crear un nuevo post
    public async Task<PostDto> CreatePostAsync(CreatePostDto dto)
    {
        // Validaciones
        if (string.IsNullOrWhiteSpace(dto.Title) || dto.Title.Length < 3)
            throw new ArgumentException("El titulo es obligatorio");
        if (string.IsNullOrWhiteSpace(dto.Description) || dto.Description.Length < 10)
            throw new ArgumentException("La descripcion es obligatoria");
        if (string.IsNullOrEmpty(dto.Content) || dto.Content.Length < 20)
            throw new ArgumentException("El contenido es obligatorio");

        // Mapeamos el DTO de entrada ala entidad interna
        var post = _mapper.Map<Post>(dto);

        await _repository.CreateAsync(post);

        // Retornamos el nuevo post convertido en DTO
        return _mapper.Map<PostDto>(post);
    }

    public async Task<bool> UpdatePostAsync(Guid id, UpdatePostDto dto)
    {
        var post = await _repository.GetByIdAsync(id);

        // Validamos si el post existe antes de actualizar
        if (post is null) return false;

        // Actualizamos las propiedades
        post.Title = dto.Title;
        post.Description = dto.Description;
        post.Content = dto.Content;
        post.UpdateAt = DateTime.UtcNow;

        await _repository.UpdateAsync(post);

        return true; // Indicamos que se actualizó con éxito
    }

    public async Task<bool> DeletePostAsync(Guid id)
    {
        var postExistente = await _repository.GetByIdAsync(id);

        // Validamos si el post existe antes de eliminar
        if (postExistente is null)
            return false;

        await _repository.DeleteAsync(postExistente);

        return true; // Eliminado correctamente
    }

}

