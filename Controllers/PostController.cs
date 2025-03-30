using BlogPost.Api.DTOs;
using BlogPost.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // Ruta base: api/posts
public class PostController : ControllerBase
{
    private readonly IPostService _postservice;

    // Inyectamos el servicio en el constructor
    public PostController(IPostService postservice)
    {
        _postservice = postservice;
    }

    // GET: api/posts
    // Obtiene todos los posts
    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        var posts = await _postservice.GetPostsAsync();
        return Ok(posts);
    }

    public async Task<IActionResult> GetPost(Guid id)
    {
        var post = await _postservice.GetPostByIdAsync(id);
        if (post is null)
        {
            return NotFound();
        }

        return Ok(post);
    }


    // POST: api/post
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto)
    {
        var createdPost = await _postservice.CreatePostAsync(dto);
        return CreatedAtAction(nameof(GetPost), new { id = createdPost.Id }, createdPost);
        //HTTP 201
    }

    // PUT: api/posts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(Guid id, [FromBody] UpdatePostDto dto)
    {
        var update = await _postservice.UpdatePostAsync(id, dto);
        if (!update)
            return NotFound();

        // HTTP 204 indicando exito sin contenido adicional
        return NoContent();
    }

    // DELETE: api/posts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var deleted = await _postservice.DeletePostAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}