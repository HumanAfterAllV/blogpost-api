using BlogPost.Api.DTOs;

namespace BlogPost.Api.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto> CreateCommentAsync(Guid postId, string userEmail, CreateCommentDto dto);
    }
}
