using BlogPost.Api.Interfaces;
using BlogPost.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Api.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> GetByIdAsync(Guid id)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetByPostIdAsync(Guid postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task<bool> DeleteAsync(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return false;

            _context.Comments.Remove(comment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsAuthorAsync(Guid commentId, Guid userId)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            return comment != null && comment.UserId == userId;

        }

    }
}
