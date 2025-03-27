using AutoMapper;
using BlogPost.Api.DTOs;
using BlogPost.Api.Models;

namespace BlogPost.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeo entre Post y PostDto (bidireccional)
        CreateMap<Post, PostDto>().ReverseMap();

        // Mapeo desde CreatePostDto hacia Post
        CreateMap<CreatePostDto, Post>();

        // Mapeo desde UpdatePostDto hacia Post
        CreateMap<UpdatePostDto, Post>();

        // Mapeo entre Comment y CommentDto (bidireccional)
        CreateMap<Comment, CommentDto>().ReverseMap();

        // Mapeo desde CreateCommentDto hacia Comment
        CreateMap<CreateCommentDto, Comment>();
    }
}
