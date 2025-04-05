using Microsoft.AspNetCore.Mvc;
using BlogPost.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using BlogPost.Api.DTOs;
using System.Security.Claims;


namespace BlogPost.Api.Controllers
{
    [ApiController]
    [Route("api/posts/{postId}/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService) 
        {
            _commentService = commentService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Guid postId, [FromBody] CreateCommentDto dto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized(email);

            var comment = await _commentService.CreateCommentAsync(postId, email, dto);

            return Ok(comment);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid postId)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        [Authorize]
        [HttpPut("{commentId}")]
        public async Task<IActionResult> Update(Guid postId, Guid commentId, [FromBody] UpdateCommentDto dto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var update = await _commentService.UpdateCommentAsync(postId, email, dto.NewComment);
            return update ? Ok("Comentario actualizado") : Forbid();
        }

        [Authorize]
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete(Guid postId, Guid commentId)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (email == null || role == null) return Unauthorized();

            var deleted = await _commentService.DeleteCommentAsync(postId, email, role);
            return deleted ? Ok("Comentario eliminado") : Forbid();

        }
    }
}
