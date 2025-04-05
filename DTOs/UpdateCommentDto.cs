using System.ComponentModel.DataAnnotations;

namespace BlogPost.Api.DTOs
{
    public class UpdateCommentDto
    {
        [Required]
        public string NewComment { get; set; } = string.Empty;
    }
}
