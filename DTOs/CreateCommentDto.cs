using System.ComponentModel.DataAnnotations;

namespace BlogPost.Api.DTOs;

public class CreateCommentDto
{
    [Required]
    public string Content { get; set; } = string.Empty;
}