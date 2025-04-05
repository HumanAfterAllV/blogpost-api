namespace BlogPost.Api.DTOs;

public class CommentDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }


    public Guid UserId { get; set; }
    public string UserEmail { get; set; } = string.Empty;

    public Guid PostId { get; set; }

}