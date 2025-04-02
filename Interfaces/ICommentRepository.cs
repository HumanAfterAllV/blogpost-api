using BlogPost.Api.Models;

namespace BlogPost.Api.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Comment>> GetByPostIdAsync(Guid postId);
        Task<bool> UpdateAsync(Comment comment);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> IsAuthorAsync(Guid commentId, Guid userId);
    }
}
