using Microsoft.AspNetCore.Mvc;
using BlogPost.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using BlogPost.Api.DTOs;
using System.Security.Claims;


namespace BlogPost.Api.Controllers
{
    [ApiController]
    [Route("api/posts/{postId}/[controller]")]
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

   
    }
}
