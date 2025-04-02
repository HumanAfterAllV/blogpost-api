using System.ComponentModel.DataAnnotations;

namespace BlogPost.Api.Models;

public class Comment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Content { get; set; } = string.Empty;


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set;} = DateTime.UtcNow;

    // Relación con Post
    public Guid PostId { get; set; }
    public Post? Post { get; set; } = null!;


    //Relacion con User
    public Guid UserId { get; set; }
    public User? User { get; set; } = null!;

}