using AutoMapper;
using BlogPost.Api.DTOs;
using BlogPost.Api.Interfaces;
using BlogPost.Api.Models;

namespace BlogPost.Api.Services
{
    public class CommentServide : ICommentService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CommentServide(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository, IMapper mapper)
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
    
    }
}
