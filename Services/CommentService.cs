using AutoMapper;
using BlogPost.Api.DTOs;
using BlogPost.Api.Interfaces;
using BlogPost.Api.Models;

namespace BlogPost.Api.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CommentService(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<CommentDto> CreateCommentAsync(Guid postId, string userEmail, CreateCommentDto dto)
        { 
            var user = await _userRepository.GetByEmailAsync(userEmail);

            if (user == null) throw new Exception("Usuario no encontrado");

            var post = await _postRepository.GetByIdAsync(postId);

            if (post == null) throw new Exception("Post no encontrado");

            var commnent = new Comment
            {
                Content = dto.Content,
                PostId = post.Id,
                UserId = user.Id,
            };

            await _commentRepository.CreateAsync(commnent);

            return _mapper.Map<CommentDto>(commnent);
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByPostIdAsync(Guid postId)
        {
            var comments = await _commentRepository.GetByPostIdAsync(postId);
            return _mapper.Map<IEnumerable<CommentDto>>(comments);
        }

        public async Task<bool> UpdateCommentAsync(Guid commentId, string userEmail, string newComment)
        {
            var user = await _userRepository.GetByEmailAsync(userEmail);
            if (user == null) return false;

            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null || comment.UserId != user.Id) return false;

            comment.Content = newComment;
            comment.UpdatedAt = DateTime.UtcNow;

            return await _commentRepository.UpdateAsync(comment);
        }

        public async Task<bool> DeleteCommentAsync(Guid commentId, string userEmail, string role)
        {
            var user = await _userRepository.GetByEmailAsync(userEmail);
            if (user == null) return false;

            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null) return false;

            if (comment.UserId != user.Id && role != "Admin") return false;

            return await _commentRepository.DeleteAsync(commentId);
        }
    
    }
}
