using BlogPost.Api.DTOs;

namespace BlogPost.Api.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto> CreateCommentAsync(Guid postId, string userEmail, CreateCommentDto dto);
        Task<IEnumerable<CommentDto>> GetCommentsByPostIdAsync(Guid postId);
        Task<bool> UpdateCommentAsync(Guid commentId, string userEmail, string newComment);
        Task<bool> DeleteCommentAsync(Guid commentId, string userEmail, string role);

    }
}
    